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
                    _XmlDocument.Load(this.LocalPath);
                }
                return _XmlDocument;
            }
        }

        private List<XmlNodeHandler> structurehandlers = new List<XmlNodeHandler>();

        public XbrlTaxonomyDocument() 
        {

        }

        public XbrlTaxonomyDocument(string filename)
        {
            _FileName = filename;
        }

        public XbrlTaxonomyDocument(XbrlTaxonomy taxonomy, string filepath)
        {
            this.Taxonomy = taxonomy;
            LoadTaxonomyDocument(filepath, null);
        }

        public XbrlTaxonomyDocument(string filepath, XbrlTaxonomyDocument parent)
        {
            this.Taxonomy = parent.Taxonomy;
            LoadTaxonomyDocument(filepath, parent);
         
        }

        public void LoadTaxonomyDocument(string filepath, XbrlTaxonomyDocument parent) 
        {
            MarkupPath = filepath;
            var sourcepath = filepath;
            if (parent != null)
            {
                if (Utilities.Strings.IsRelativePath(MarkupPath))
                {
                    sourcepath = Utilities.Strings.ResolveRelativePath(parent.SourceFolder, MarkupPath);
                }
            }

            var localpath = Utilities.Strings.GetLocalPath(Taxonomy.LocalFolder, sourcepath);

            SetLocalPath(localpath);
            SetSourcePath(sourcepath);
            try
            {
                Utilities.Strings.CopyToLocal(sourcepath, localpath, false);
                LoadDocument();

            }
            catch (Exception ex) 
            {
                Console.WriteLine(String.Format("Can't get source file {0}. Error: {1}", sourcepath, ex));
            }

            structurehandlers.Add(new XmlNodeHandler(Tags.Links, LoadLink));

            structurehandlers.Add(new XmlNodeHandler(Tags.Imports, LoadImport));
        }

        public override void LoadReferences()
        {
            if (XmlDocument != null)
            {
                var tags = XmlDocument.GetElementsByTagName("*").Cast<XmlNode>();
                _TagNames = tags.Select(i => i.Name).Distinct().ToArray();
                _TargetNamespace = this.Taxonomy.GetTargetNamespace(XmlDocument);
                foreach (var handler in structurehandlers)
                {
                    var subtags = tags.Where(i => i.Name.ToLower().In(handler.XmlTagNames));
                    foreach (var subtag in subtags)
                    {
                        handler.Handle(subtag, this);
                    }

                }
            }
        }

        public bool LoadLink(XmlNode node, XbrlTaxonomyDocument taxonomydocument)
        {
            var result = true;
            var path = Xml.Attr(node, Attributes.XlinkHref);
            LoadTaxonomyDocument(path);

            return result;
        }

        public bool LoadImport(XmlNode node, XbrlTaxonomyDocument taxonomydocument)
        {
            var result = true;
            var path = Xml.Attr(node, Attributes.SchemaLocation);
            LoadTaxonomyDocument(path);
            return result;

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
                var localpath = Utilities.Strings.GetLocalPath(Taxonomy.LocalFolder, sourcepath);
                //Console.WriteLine(localpath);

                var taxonomydocument = Taxonomy.FindDocument(localpath);
                if (taxonomydocument == null)
                {
                    //Console.WriteLine(path);
                    taxonomydocument = new XbrlTaxonomyDocument(path, this);
                    Taxonomy.AddTaxonomyDocument(taxonomydocument);
                    taxonomydocument.LoadReferences();
                }
                //var reference = References.FirstOrDefault(i => i.SourcePath == path);
                var reference = References.FirstOrDefault(i => i.LocalPath == localpath);
                if (reference == null)
                {
                    References.Add(taxonomydocument);
                    ReferencedFiles.Add(taxonomydocument.LocalPath);

                }
            }
        }

        public override void LoadDocument()
        {
            XmlDocument.LoadXml(System.IO.File.ReadAllText(_LocalPath));

        }

    }
}
