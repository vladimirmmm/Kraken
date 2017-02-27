using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class PublicContractResolver : DefaultContractResolver
    {

        protected override List<MemberInfo> GetSerializableMembers(Type objectType)
        {
            var flags = BindingFlags.Instance | BindingFlags.Public;
            var members = objectType.GetProperties(flags).Cast<MemberInfo>().ToList();
            return members;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {

            IList<JsonProperty> props = base.CreateProperties(type, memberSerialization);
            props = props.Where(p => p.Writable && p.PropertyName != "Parent").ToList();
            return props;

        }
    }
    
    public class Converters
    {
        private static CultureInfo dci = new CultureInfo("en-US");
        public static void test() 
        {
            var x = FastParse("-23");
            var y= FastParse("123423");
        }
        public static int FastParse(string str) 
        {
            int value = 0;
            var startix = str[0]==45?1:0;
            for (int i = startix; i < str.Length; i++)
            {
                if (str[i] == ' ') { break; }
                value = value * 10 + (str[i] - '0');

            }
            if (startix == 1) { value = value * -1; }
            return value;
        }

        public static String ToJson(object obj) 
        {
            var settings = new JsonSerializerSettings(){  
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore 
                
            };
            //Newtonsoft.Json.Serialization.DefaultContractResolver dcr = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            //dcr.DefaultMembersSearchFlags = System.Reflection.BindingFlags.NonPublic;
            //jss.ContractResolver = dcr;
            settings.ContractResolver = new PublicContractResolver();
     
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj, Formatting.Indented, settings
                );
        }

        public static String ToJsonMin(object obj)
        {
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Formatting=Formatting.None
            };
            //Newtonsoft.Json.Serialization.DefaultContractResolver dcr = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            //dcr.DefaultMembersSearchFlags = System.Reflection.BindingFlags.NonPublic;
            //jss.ContractResolver = dcr;
            settings.ContractResolver = new PublicContractResolver();

            return Newtonsoft.Json.JsonConvert.SerializeObject(obj, Formatting.Indented, settings
                );
        }

        public static String ToJson<T>(object obj)
        {
            var settings = new JsonSerializerSettings() { 
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore 
            };
            //Newtonsoft.Json.Serialization.DefaultContractResolver dcr = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            //dcr.DefaultMembersSearchFlags = System.Reflection.BindingFlags.NonPublic;
            //jss.ContractResolver = dcr;
            settings.ContractResolver = new PublicContractResolver();

            return Newtonsoft.Json.JsonConvert.SerializeObject(obj, typeof(T), Formatting.Indented, settings
                );
        }

        public static T JsonTo<T>(string jsontext)
        {
            if (String.IsNullOrEmpty(jsontext)) { return default(T); }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsontext);
        }

        public static void JsonToObject<T>(string jsontext, T target)
        {
            if (String.IsNullOrEmpty(jsontext)) { }
            Newtonsoft.Json.JsonConvert.PopulateObject(jsontext,target);
        }

        public static string GetSizeReadable(long i)
        {
            string sign = (i < 0 ? "-" : "");
            double readable = (i < 0 ? -i : i);
            string suffix;
            if (i >= 0x1000000000000000) // Exabyte
            {
                suffix = "EB";
                readable = (double)(i >> 50);
            }
            else if (i >= 0x4000000000000) // Petabyte
            {
                suffix = "PB";
                readable = (double)(i >> 40);
            }
            else if (i >= 0x10000000000) // Terabyte
            {
                suffix = "TB";
                readable = (double)(i >> 30);
            }
            else if (i >= 0x40000000) // Gigabyte
            {
                suffix = "GB";
                readable = (double)(i >> 20);
            }
            else if (i >= 0x100000) // Megabyte
            {
                suffix = "MB";
                readable = (double)(i >> 10);
            }
            else if (i >= 0x400) // Kilobyte
            {
                suffix = "KB";
                readable = (double)i;
            }
            else
            {
                return i.ToString(sign + "0 B"); // Byte
            }
            readable = readable / 1024;

            return sign + readable.ToString("0.## ") + suffix;
        }


        public static string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        public static string DateFormat = "yyyy-MM-dd";
        public static String DateTimeToString(DateTime? dt, string format)
        {
            if (!dt.HasValue) { return ""; }
            return DateTimeToString(dt.Value, format);
        }
        public static String DateTimeToString(DateTime dt, string format)
        {
            CultureInfo ci = new CultureInfo("en-US");
            return String.Format("{0:" + format + "}", dt);
        }
        public static String DateTimeToString(DateTime dt)
        {
            CultureInfo ci = new CultureInfo("en-US");
            return String.Format("{0:" + DateTimeFormat + "}", dt);
        }
        public static DateTime StringToDateTime(String dt, string format)
        {
            DateTime result = DateTime.Now.AddYears(-200);
            if (!DateTime.TryParseExact(dt, format, dci, DateTimeStyles.None, out result))
            {
            }
            else 
            {

            }
            return result;
        }
        public static bool IsDate(String dt, string format)
        {
            DateTime result = DateTime.Now.AddYears(-200);
            return DateTime.TryParseExact(dt, format, dci, DateTimeStyles.None, out result);
           
        }
        //public static DateTime StringToDateTime(String dt, string format)
        //{
        //    DateTime result = DateTime.Now.AddYears(-200);

            
        //    var dtfi = ci.DateTimeFormat;
        //    dtfi.ShortDatePattern = format;
        //    dtfi.FullDateTimePattern = format;

        //    if (!DateTime.TryParse(dt, dtfi, DateTimeStyles.None, out result))
        //    {
        //    }
        //    return result;
        //}


        public static bool IsInteger(string p)
        {
            return !String.IsNullOrEmpty(p) && p.All(char.IsDigit) ;
        }
    }
}
