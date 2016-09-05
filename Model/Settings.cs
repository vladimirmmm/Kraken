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
    //public class Setting 
    //{
    //    public SimpleType Type { get; set; }
    //    public String Name { get; set; }
    //    public String Value { get; set; }

    //    public Setting() 
    //    {

    //    }
    //    public Setting(string Name)
    //    {
    //        this.Name = Name;
    //        //this.Type=
    //    }
        
    //    public Setting(string Name,SimpleType Type)
    //    {
    //        this.Name = Name;
    //        this.Type = Type;
    //    }

    //    public Setting(string Name, SimpleType Type,Object Value)
    //    {
    //        this.Name=Name;
    //        this.Type = Type;
    //        this.Value = String.Format("{0}", Value);
    //    }
    //}
    public class Settings : Dictionary<string, string>
    {
        private static string FileName = "Settings.json";

        public string GetJsonObj() 
        {
            var dict = new Dictionary<string, string>();
            foreach (var item in this) 
            {
                dict.Add(item.Key, item.Value);
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
                var settings = Utilities.Converters.JsonTo<Settings>(json);
                var keys = _Current.Keys.ToList();
                foreach (var key in keys) 
                {
                    if (settings.ContainsKey(key)) 
                    {
                        _Current[key] = settings[key];
                    }
                }
                Save(null);
                
            }
        }
        public Settings()
        {
            AddSetting("ValidateOnInstanceLoaded",true);
            AddSetting("ReloadTaxonomyOnInstanceLoaded", false);
            AddSetting("ReloadFullTaxonomyButStructure", false);
            AddSetting("ReloadFullTaxonomy", false);
            AddSetting("ReDownloadFiles", false);
            AddSetting("LoadValidationRules", true);
            AddSetting("CheckValidationCells", false);
            AddSetting("DebugLevel", 0);
            AddSetting("AppMode", 0);
        }

        public void AddSetting(string name, object value) 
        {
            var valuetype = value == null ? typeof(string) : value.GetType();
         
            var strvalue = String.Format("{0}", value);

            if (valuetype == typeof(Boolean)) 
            {
            }
            if (valuetype == typeof(int))
            {
            }
            if (valuetype == typeof(DateTime))
            {
                strvalue = Utilities.Converters.DateTimeToString((DateTime)value, Utilities.Converters.DateTimeFormat);
            }
            this.Add(name, strvalue);
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
        public Boolean LoadValidationRules
        {
            get { return Boolean.Parse(GetValue("LoadValidationRules", SimpleType.Boolean, false)); }
            set { SetValue("LoadValidationRules", value); }
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

        public string GetValue(string key,SimpleType type,object DefaultValue) 
        {
            string value = String.Format("{0}", DefaultValue);
            if (!this.ContainsKey(key))
            {
                this.Add(key, value);
            }
            else 
            {
                value = this[key];
            }
            if (type == SimpleType.Boolean) 
            {
                value = value.Trim().ToLower() == "true" || value.Trim().ToLower() == "1" ? "True" : "False";
            }
            return value;
        }

        public void SetValue(string key, object Value)
        {
            string value = String.Format("{0}", Value);

            if (!this.ContainsKey(key))
            {

                this.Add(key, value);

            }
            this[key] = value;
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
