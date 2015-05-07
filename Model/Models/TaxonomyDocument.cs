using BaseModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Utilities;

namespace LogicalModel
{
    public class TaxonomyDocument:Document
    {
        [JsonIgnore]
        public Taxonomy Taxonomy { get; set; }

        private List<TaxonomyDocument> _References = new List<TaxonomyDocument>();
        [JsonIgnore]
        public List<TaxonomyDocument> References 
        {
            get { return _References; }
        }

        public string MarkupPath = "";


        public void SetLocalPath(string value) 
        {
            _LocalPath = value;
            _FileName = Utilities.Strings.GetFileName(_LocalPath);
            _LocalFolder = Utilities.Strings.GetFolder(_LocalPath);
        }

        public void SetSourcePath(string value)
        {
            _SourcePath = value;
            _SourceFolder = Utilities.Strings.GetFolder(_SourcePath);
        }

        public void LoadAll()//Engine engine)
        {
            LoadReferences();
   
            //Engine.TraverseNodes(XmlDocument.ChildNodes, Handler);
        }

        

        public override string ToString()
        {
            return String.Format("TaxonomyDocument: {0} References {1} ", FileName , this.References.Count);
        }

    }
}
