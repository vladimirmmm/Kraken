using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBRLProcessor.Literals
{
    public class Tags
    {
        public static string[] Links = { "link:linkbaseRef", "link:roleRef", "arcroleRef", "link:loc", "link:arcroleRef" };
        public static string[] Imports = { "xs:import" };
        public static string[] Labels = { "label:label", "xbrlle:label", "eg:label", "fn:label", "link:label", "xbrli:label", "xfi:label", "xbrldi:label", "xbrldt:label", "xl:label", "xlink:label", "xs:label", "xsi:label", "gen:label", "variable :label", "iso4217:label" };
        public static string[] TableContainers = { "xs:appinfo" };

        public const string UsedOn = "link:usedOn";
        public const string TableLayoutContainer = "gen:link";
        public const string TableDefinitionContainer = "link:definitionlink";
        public const string LinkBaseRef = "link:linkbaseRef";
        public const string LinkBase = "link:linkbase";

               
    }
    public class Roles 
    {
        public static string[] LabelCodeRoles = { "http://www.eurofiling.info/xbrl/role/rc-code", "http://www.eba.europa.eu/xbrl/role/dpm-db-id" };
        public static string[] LabelTextRoles = { "http://www.xbrl.org/2008/role/label", "http://www.xbrl.org/2003/role/label" };

    }
    public class Literal
    {
        public const string RenderFileSuffix = "-rend.xml";
        public const string DefinitionFileSuffix = "-def.xml";
        public const string LabelPrefix = "label_";
    }

    public class Attributes
    {
        public const string XlinkHref = "xlink:href";
        public static string SchemaLocation = "schemaLocation";
        public const string LabelID = "xlink:label";
        public const string LabelRole = "xlink:role";
        public const string Language = "xml:lang";
    }
}
 