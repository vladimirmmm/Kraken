using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BaseModel
{
    public class DocumentCollection
    {

        
        public Document EntryDocument { get; set; }

        private List<Document> _Documents = new List<Document>();
        public List<Document> Documents
        {
            get { return _Documents; }
            set { _Documents = value; }
        }

        public virtual void LoadAllReferences() { }

        public virtual void LoadAll() { }
    }
}
