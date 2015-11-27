using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalModel
{
    public enum SimpleType
    {
        Numeric,
        String,
        Date,
        Boolean,
    }
    public class Setting 
    {
        public SimpleType Type { get; set; }
        public String Name { get; set; }
        public String Value { get; set; }

        public Setting() 
        {

        }
        public Setting(string Name)
        {
            this.Name = Name;
            //this.Type=
        }
        
        public Setting(string Name,SimpleType Type)
        {
            this.Name = Name;
            this.Type = Type;
        }

        public Setting(string Name, SimpleType Type,Object Value)
        {
            this.Name=Name;
            this.Type = Type;
            this.Value = String.Format("{0}", Value);
        }
    }
    public class Settings : Dictionary<string, Setting>
    {
        private static string FileName = "Settings.json";

        public string GetJsonObj() 
        {
            var dict = new Dictionary<string, string>();
            foreach (var item in this) 
            {
                dict.Add(item.Key, item.Value.Value);
            }
            return Utilities.Converters.ToJson(dict);
        }

        private static Settings _Current = null;
        public static Settings Current 
        {
            get
            {
                if (_Current == null)
                {
                    _Current = new Settings();
                }
                return _Current;
            }

    
        }

        public void Save(Settings settings)
        {
            var dir = AppDomain.CurrentDomain.BaseDirectory;
            var filepath = dir + "\\" + FileName;
            if (settings==null)
            {
                settings = this;
            }
            System.IO.File.WriteAllText(filepath, Utilities.Converters.ToJson(settings));
        }

        public void Load(String json)
        {
            if (string.IsNullOrEmpty(json))
            {
                var dir = AppDomain.CurrentDomain.BaseDirectory;
                var filepath = dir + "\\" + FileName;
                if (System.IO.File.Exists(filepath))
                {
                    json = System.IO.File.ReadAllText(filepath);
                }
            }
            if (!string.IsNullOrEmpty(json))
            {
                _Current = Utilities.Converters.JsonTo<Settings>(json);
                
            }
        }

        public Boolean ValidateOnInstanceLoaded 
        {
            get { return Boolean.Parse(GetValue("ValidateOnInstanceLoaded", SimpleType.Boolean, false)); }
            set {  SetValue("ValidateOnInstanceLoaded",value); }
        }
        public Boolean ReloadTaxonomyOnInstanceLoaded
        {
            get { return Boolean.Parse(GetValue("ReloadTaxonomyOnInstanceLoaded", SimpleType.Boolean, false)); }
            set {  SetValue("ReloadTaxonomyOnInstanceLoaded",value); }
        }
        public Boolean ReloadFullTaxonomyButStructure
        {
            get { return Boolean.Parse(GetValue("ReloadFullTaxonomyButStructure", SimpleType.Boolean, false)); }
            set {  SetValue("ReloadFullTaxonomyButStructure", value); }
        }
        public Boolean ReloadFullTaxonomy
        {
            get { return Boolean.Parse(GetValue("ReloadFullTaxonomy", SimpleType.Boolean, false)); }
            set {  SetValue("ReloadFullTaxonomy",value); }
        }
        public Boolean ReDownloadFiles
        {
            get { return Boolean.Parse(GetValue("ReDownloadFiles", SimpleType.Boolean, false)); }
            set { SetValue("ReDownloadFiles", value); }
        }
        public Boolean CheckValidationCells
        {
            get { return Boolean.Parse(GetValue("CheckValidationCells", SimpleType.Boolean, false)); }
            set { SetValue("CheckValidationCells", value); }
        }
        public int DebugLevel
        {
            get { return int.Parse(GetValue("DebugLevel", SimpleType.Numeric, 0)); }
            set { SetValue("DebugLevel",value); }
        }
        public int AppMode
        {
            get { return int.Parse(GetValue("AppMode", SimpleType.Numeric, 0)); }
            set { SetValue("AppMode", value); }
        }

        private string GetValue(string key,SimpleType type,object DefaultValue) 
        {
            Setting setting = null;
            if (!this.ContainsKey(key))
            {
                setting = new Setting(key, type, DefaultValue);
                this.Add(key, setting);
            }
            else 
            {
                setting = this[key];
            }
            return setting.Value;
        }

        private void SetValue(string key, object Value)
        {
            Setting setting = null;
            if (!this.ContainsKey(key))
            {
                setting = new Setting(key, SimpleType.String, Value);
                this.Add(key, setting);

            }
            setting = this[key];
            setting.Value = String.Format("{0}", Value);
        }
    }

    public enum AppMode 
    { 
        Development=1,
        Free=2,
        Full = 3,
        FullwithAPI = 2,
    }
}
