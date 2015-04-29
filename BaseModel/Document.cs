using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModel
{
    public class Document
    {
        public DocumentCollection Container { get; set; }

        protected string _FileName = "";
        public string FileName { get { return _FileName; } set { _FileName = value; } }

        protected string _LocalFolder = "";
        public string LocalFolder { get { return _LocalFolder; } set { _LocalFolder = value; } }
        
        protected string _SourceFolder = "";
        public string SourceFolder { get { return _SourceFolder; } }

        public List<String> _ReferencedFiles = new List<String>();
        public List<String> ReferencedFiles
        {
            get { return _ReferencedFiles; }
            set { _ReferencedFiles = value; }
        }

        protected string _LocalPath = "";
        public string LocalPath
        {
            get { return _LocalPath; }
            set {_LocalPath = value; }
        }

        protected string _SourcePath = "";
        public string SourcePath
        {
            get { return _SourcePath; }
            set { _SourcePath = value;}
        }

        public String Content 
        {
            get { return GetContent(); }
            //set { SetContent(value); }
        }

        public virtual void SetContent(string value) { }

        public virtual string GetContent() { return ""; }

        public virtual void LoadReferences() { }

        public virtual void LoadDocument() 
        { 

        }


    }
}
