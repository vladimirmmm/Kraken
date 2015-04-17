using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace XBRLProcessor.Enums
{
    //C:\My\Tasks\!Tools\Taxonomies\XBRl taxonomy 2.2\XBRL Taxonomy and Supporting Documents.2.2\Taxonomy\2.2.0.0\www.xbrl.org\2005\xbrldt-2005.xsd
    public class ArcRoleType
    {
        private static List<KeyValue<string, string>> keys = new List<KeyValue<string, string>>();
        public static String hypercube_dimension = Add("hypercube_dimension", "http://xbrl.org/int/dim/arcrole/hypercube-dimension");
        public static String dimension_domain = Add("dimension_domain", "http://xbrl.org/int/dim/arcrole/dimension-domain");
        public static String domain_member = Add("domain_member", "http://xbrl.org/int/dim/arcrole/domain-member");
        public static String all = Add("all", "http://xbrl.org/int/dim/arcrole/all");
        public static String notAll = Add("notAll", "http://xbrl.org/int/dim/arcrole/notAll");
        public static String dimension_default = Add("dimension_default", "http://xbrl.org/int/dim/arcrole/dimension-default");

        public static string Add(string key, string value) 
        {
            keys.Add(new KeyValue<string, string>(key, value.ToLower()));
            return value;
        }

        public static string GetKeyByValue(string value)
        {
            string lvalue=value.ToLower();
            return keys.FirstOrDefault(i=>i.Value==lvalue).Key;
        }
    }
}
