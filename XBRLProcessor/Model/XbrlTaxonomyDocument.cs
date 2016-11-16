using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Utilities;
using XBRLProcessor.Helpers;
using XBRLProcessor.Literals;

namespace XBRLProcessor.Models
{
    public class XbrlTaxonomyDocument : LogicalModel.TaxonomyDocument
    {
        [JsonIgnore]
        public new XbrlTaxonomy Taxonomy
        {
            get { return (XbrlTaxonomy)base.Taxonomy; }
            set { base.Taxonomy=value; }
        }

        private List<String> _LoadedSourcePathes = new List<String>();

        private List<XbrlTaxonomyDocument> _References = new List<XbrlTaxonomyDocument>();
        public new List<XbrlTaxonomyDocument> References
        {
            get
            {
                return _References;
            }
        }

        public override string GetContent()
        {
            return Utilities.Xml.XmlToString(XmlDocument);
        }

        private string[] _TagNames = new string[] { };
        public string[] TagNames { get { return _TagNames; } set { _TagNames = value; } }

        private string _TargetNamespacePrefix = "";
        public string TargetNamespacePrefix { get { return _TargetNamespacePrefix; } set { _TargetNamespacePrefix = value; } }
        
     
        private string _TargetNamespace = "";
        public string TargetNamespace { get { return _TargetNamespace; } set { _TargetNamespace = value; } }
    

        private XmlDocument _XmlDocument = null;
        public XmlDocument XmlDocument
        {
            get
            {
                if (_XmlDocument == null && System.IO.File.Exists(this.LocalPath))
                {
                    _XmlDocument = new XmlDocument();

                    //var textReader = System.IO.File.OpenText(this.LocalPath);
                    //XmlTextReader xmlReader = new XmlTextReader(textReader);
                 
                    //var xmlreadersettings = new XmlReaderSettings();
                    //xmlreadersettings.ValidationType = ValidationType.None;
                    //xmlreadersettings.DtdProcessing = DtdProcessing.Parse;
                    //xmlreadersettings.XmlResolver = null;
                    //XmlReader xmlReader = XmlReader.Create(this.LocalPath, xmlreadersettings);
                    ////extract and flatten data from the xml doc
                    //XmlDocument xmlDoc = new XmlDocument();
                    //xmlDoc.XmlResolver = null;
                    //xmlDoc.Load(xmlReader);


                    _XmlDocument.Load(this.LocalPath);
                    _XmlDocument.DocumentElement.SetAttribute("localpath", this.LocalPath);
                    var localpath = _XmlDocument.DocumentElement.GetAttribute("localpath");
                    if (String.IsNullOrEmpty(localpath))
                    {

                    }

                }
                return _XmlDocument;
            }
        }

        //public void test()
        //{
        //    var xmlreadersettings = new XmlReaderSettings();
        //    xmlreadersettings.ValidationType = ValidationType.None;
        //    xmlreadersettings.DtdProcessing = DtdProcessing.Parse;
        //    xmlreadersettings.ConformanceLevel = ConformanceLevel.Fragment;
        //    xmlreadersettings.XmlResolver = null;
        //    XmlReader xmlReader = XmlReader.Create(@"C:\Users\vladimir.balacescu\AppData\Roaming\XbrlStudio\Taxonomies\www.xbrl.org\in\bk\2009\in-gaap-roles-2009-06-30.xsd", xmlreadersettings);
        //    //extract and flatten data from the xml doc
        //    XmlDocument xmlDoc = new XmlDocument();
        //    xmlDoc.XmlResolver = null;
        //    xmlDoc.Load(xmlReader);
  
        //}

        private List<XmlNodeHandler> structurehandlers = new List<XmlNodeHandler>();
        private Dictionary<string, bool> _RelativeReferencedFiles = new Dictionary<string, bool>();
        public List<String> RelativeReferencedFiles = new List<String>();

        public XbrlTaxonomyDocument() 
        {
            AddHandlers();
        }

        public XbrlTaxonomyDocument(string filename)
        {
            _FileName = filename;
            AddHandlers();
        }

        public XbrlTaxonomyDocument(XbrlTaxonomy taxonomy, string filepath)
        {
            this.Taxonomy = taxonomy;
            AddHandlers();
            LoadTaxonomyDocument(filepath, null);
        }

        public XbrlTaxonomyDocument(string filepath, XbrlTaxonomyDocument parent)
        {
            this.Taxonomy = parent.Taxonomy;
            AddHandlers();
            LoadTaxonomyDocument(filepath, parent);
         
        }

        private void AddHandlers() 
        {
            structurehandlers.Add(new XmlNodeHandler(Tags.Links, LoadLink));
            structurehandlers.Add(new XmlNodeHandler(Tags.Imports, LoadImport));
        }

        public void LoadTaxonomyDocument(string path)
        {
            //remove hash

            if (path.Contains("#"))
            {
                path = path.Remove(path.IndexOf("#"));
            }

            var loaded = _LoadedSourcePathes.FirstOrDefault(i => i == path);
            if (loaded == null)
            {
                _LoadedSourcePathes.Add(path);


                var fullpath = path;
                //var taxonomydocument = Engine.CurrentEngine.TaxonomyDocuments.FirstOrDefault(i => i.SourcePath == path);


                var sourcepath = path;
                if (Utilities.Strings.IsRelativePath(sourcepath))
                {
                    sourcepath = Utilities.Strings.ResolveRelativePath(SourceFolder, path);
                }
                var localpath = Utilities.Strings.GetLocalPath(XbrlEngine.LocalFolder, sourcepath);
                var localrelpath = Utilities.Strings.GetRelativePath(LogicalModel.TaxonomyEngine.LocalFolder, localpath);
                //Logger.WriteLine(localpath);

                var taxonomydocument = Taxonomy.FindDocument(localrelpath);
                if (taxonomydocument == null)
                {
                    //Logger.WriteLine(path);
                    taxonomydocument = new XbrlTaxonomyDocument(path, this);
                    Taxonomy.AddTaxonomyDocument(taxonomydocument);
                    taxonomydocument.LoadReferences();
                }
                //var reference = References.FirstOrDefault(i => i.SourcePath == path);
                var reference = References.FirstOrDefault(i => i.LocalRelPath == localrelpath);
                if (reference == null)
                {
                    References.Add(taxonomydocument);
                    ReferencedFiles.Add(taxonomydocument.LocalRelPath);

                }
            }
        }


        public bool LoadTaxonomyDocument(string filepath, XbrlTaxonomyDocument parent) 
        {
            if (filepath.Contains("header-rend.xml")) 
            {

            }
            var result = true;
            MarkupPath = filepath;
            var sourcepath = filepath;
            if (parent != null)
            {
                if (Utilities.Strings.IsRelativePath(MarkupPath))
                {
                    sourcepath = Utilities.Strings.ResolveRelativePath(parent.SourceFolder, MarkupPath);
                }
            }

            var localpath = Utilities.Strings.GetLocalPath(XbrlEngine.LocalFolder, sourcepath);

            SetLocalPath(localpath);
            SetSourcePath(sourcepath);
             //moved to FileManager.cs
            try
            {
                Utilities.Strings.CopyToLocal(sourcepath, localpath, false);
                LoadDocument();

            }
            catch (Exception ex) 
            {
                Logger.WriteLine(String.Format("Can't get source file {0}. Error: {1}", sourcepath, ex));
                result = false;
            }
          

            return result;
        }

        public override void LoadReferences()
        {
            if (XmlDocument != null)
            {
                var tags = XmlDocument.GetElementsByTagName("*").Cast<XmlNode>();
                _TagNames = tags.Select(i => i.Name).Distinct().ToArray();
                this.Taxonomy.SetTargetNamespace(this);
                foreach (var handler in structurehandlers)
                {
                    var subtags = tags.Where(i => i.Name.ToLower().In(handler.XmlTagNames));
                    foreach (var subtag in subtags)
                    {
                        handler.Handle(subtag, this);
                    }

                }
                var fm = new LogicalModel.Helpers.FileManager();
                var redownload = LogicalModel.Settings.Current.ReDownloadFiles;
                //redownload = true;
                RelativeReferencedFiles = _RelativeReferencedFiles.Select(i => i.Key).ToList();
                //var localfiles = RelativeReferencedFiles.AsParallel().Select(i => fm.GetFile(this, i, redownload)).ToList();
                var localfiles = Utilities.Threading.ExecuteParalell(RelativeReferencedFiles, i => fm.GetFile(this, i, redownload)).ToList();
                localfiles = localfiles.Where(i => !String.IsNullOrEmpty(i)).ToList();
                foreach (var file in localfiles)
                {
                    LogicalModel.Helpers.FileManager.AddDocument(file);
                }
                foreach (var file in RelativeReferencedFiles)
                {
                    LoadTaxonomyDocument(file);
                }
                RelativeReferencedFiles.Clear();
                _RelativeReferencedFiles.Clear();

            }
        }
        private void AddRelativeReferencedFiles(string path)
        {
            if (path.Contains("#"))
            {
                path = path.Remove(path.IndexOf("#"));
            }
            if (!_RelativeReferencedFiles.ContainsKey(path)) 
            {
                _RelativeReferencedFiles.Add(path, true);
            }
        }
        //TODO
        public bool LoadLink(XmlNode node, XbrlTaxonomyDocument taxonomydocument)
        {
            var result = true;
            var path = Xml.Attr(node, Attributes.XlinkHref);
            AddRelativeReferencedFiles(path);

            return result;
        }

        public bool LoadImport(XmlNode node, XbrlTaxonomyDocument taxonomydocument)
        {
            var result = true;
            var path = Xml.Attr(node, Attributes.SchemaLocation);
            AddRelativeReferencedFiles(path);

            return result;

        }

       
        public override void LoadDocument()
        {
            XmlDocument.LoadXml(System.IO.File.ReadAllText(LocalPath));
            XmlDocument.DocumentElement.SetAttribute("localpath", LocalPath);
        }

        public override void ClearDocument()
        {
            base.ClearDocument();
            Utilities.Xml.ClearDocument(this.XmlDocument);
            this._XmlDocument = null;

        }

       

    }
}
