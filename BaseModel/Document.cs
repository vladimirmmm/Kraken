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
     
        protected string _SourceFolder = "";
        public string SourceFolder { get { return _SourceFolder; } }
        


        private List<String> _ReferencedFiles = new List<String>();
        public List<String> ReferencedFiles
        {
            get { return _ReferencedFiles; }
            set { _ReferencedFiles = value; }
        }

        protected string _LocalRelPath = "";
        public string LocalRelPath
        {
            get { return _LocalRelPath; }
            set { _LocalRelPath = value; }
        }
        public string LocalPath
        {
            get 
            {
                //return String.Format("{0}{1}", GetEngineLocalFolder(), LocalRelPath);
                return Utilities.Strings.ResolveRelativePath( GetEngineLocalFolder(), LocalRelPath); 
            }
    
        }
        public string _LocalFolder = "";
    
        public string LocalFolder
        {
         
            //return Utilities.Strings.GetFolder(LocalPath);
            get 
            {
                if (String.IsNullOrEmpty(_LocalFolder)) 
                {
                    _LocalFolder = Utilities.Strings.GetFolder(LocalPath);
                }
                return _LocalFolder; 
            }

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

        public virtual string GetEngineLocalFolder() {
            return "";
        }

        public virtual void SetContent(string value) { }

        public virtual string GetContent() { return ""; }

        public virtual void LoadReferences() { }

        public virtual void LoadDocument() 
        { 

        }


    }
}
