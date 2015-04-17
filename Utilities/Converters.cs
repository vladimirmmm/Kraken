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
    public class Converters
    {
        class PublicContractResolver : DefaultContractResolver
        {

            protected override List<MemberInfo> GetSerializableMembers(Type objectType)
            {
                var flags = BindingFlags.Instance | BindingFlags.Public;
                //MemberInfo[] fields = objectType.GetProperties(flags);
                //return fields
                //    .Concat(objectType.GetProperties(flags).Where(propInfo => propInfo.CanWrite))
                //    .ToList();
                return objectType.GetProperties(flags).Cast<MemberInfo>().ToList();
            }

            protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
            {

                IList<JsonProperty> props = base.CreateProperties(type, memberSerialization);
                return props.Where(p => p.Writable).ToList();

            }
        }


        public static String ToJson(object obj) 
        {
            var settings = new JsonSerializerSettings{  ReferenceLoopHandling = ReferenceLoopHandling.Ignore};
            //Newtonsoft.Json.Serialization.DefaultContractResolver dcr = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            //dcr.DefaultMembersSearchFlags = System.Reflection.BindingFlags.NonPublic;
            //jss.ContractResolver = dcr;
            settings.ContractResolver = new PublicContractResolver();
     
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj, Formatting.Indented, settings
                );
        }

        public static T JsonTo<T>(string jsontext)
        {
            if (String.IsNullOrEmpty(jsontext)) { return default(T); }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsontext);
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

        public static String DateTimeToString(DateTime dt, string format)
        {
            CultureInfo ci = new CultureInfo("en-US");
            return String.Format(ci, format, dt);
        }

        public static DateTime StringToDateTime(String dt, string format)
        {
            DateTime result = DateTime.Now.AddYears(-200);

            CultureInfo ci = new CultureInfo("en-US");
            var dtfi = ci.DateTimeFormat;
            dtfi.ShortDatePattern = format;
            dtfi.FullDateTimePattern = format;

            if (!DateTime.TryParse(dt, dtfi, DateTimeStyles.None, out result))
            {
            }
            return result;
        }

    }
}
