using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace XBRLProcessor
{
    public class SchemaValidation
    {
        private List<String> messages = null;
        public void ValidateInstance(XmlDocument instancedoc, Dictionary<string, string> Schemas, List<String> messages) 
        {
            this.messages = messages;

            //foreach (var item in Schemas) 
            //{
            //    ValidateInstanceBySchema(instancedoc, item.Key, messages);
            //}
            var readersettings = new XmlReaderSettings(); ;

            var nodereader = new XmlNodeReader(instancedoc);


            readersettings.Async = false;
            readersettings.DtdProcessing = DtdProcessing.Parse;
            readersettings.ValidationType = ValidationType.Schema;
            readersettings.ConformanceLevel = ConformanceLevel.Auto;
            readersettings.ValidationFlags =
                XmlSchemaValidationFlags.ProcessIdentityConstraints
                | XmlSchemaValidationFlags.ProcessInlineSchema
                //|XmlSchemaValidationFlags.ReportValidationWarnings 
                | XmlSchemaValidationFlags.ProcessSchemaLocation
                ;
            readersettings.ValidationEventHandler += new ValidationEventHandler(SettingsValidationEventHandler);
            try
            {
                foreach(var item in Schemas){
                    if (!readersettings.Schemas.Contains(item.Value))
                    {
                        readersettings.Schemas.Add(item.Value, item.Key);
                    }
                }
                XmlReader xmlReader = XmlReader.Create(nodereader, readersettings);

                while (xmlReader.Read()) { }
            }
            catch (Exception ex)
            {

            }
            this.messages = null;
        }

        public void ValidateInstanceBySchema(XmlDocument instancedoc, string schemapath, List<string> messages)
        {
            var readersettings = new XmlReaderSettings(); ;

            var nodereader = new XmlNodeReader(instancedoc);


            readersettings.Async = false;
            readersettings.DtdProcessing = DtdProcessing.Parse;
            readersettings.ValidationType = ValidationType.Schema;
            readersettings.ConformanceLevel = ConformanceLevel.Auto;
            readersettings.ValidationFlags =
                XmlSchemaValidationFlags.ProcessIdentityConstraints
                |XmlSchemaValidationFlags.ProcessInlineSchema 
                //|XmlSchemaValidationFlags.ReportValidationWarnings 
                | XmlSchemaValidationFlags.ProcessSchemaLocation
                ;
            readersettings.ValidationEventHandler += new ValidationEventHandler(SettingsValidationEventHandler);

            try
            {
                readersettings.Schemas.Add(null, schemapath);

                XmlReader xmlReader = XmlReader.Create(nodereader, readersettings);

                while (xmlReader.Read()) { }
            }
            catch (Exception ex) 
            {
                
            }
        }
        static void Main()
        {
            //var instancepath = @"C:\My\Tasks\00406446\EBA_COREP_Standard_SOLO_rev4_DPM_2.4_CIB_30_09_2016.xbrl";
            var instancepath = @"C:\Program Files (x86)\FRS Apps\ILA 430\ILA\logs\Copy_of_ALMM_2.4.1_ILA_4.3.0._2016.09.30..xbrl";
            ////var t_location = "http://www.eba.europa.eu/eu/fr/xbrl/crr/fws/corep/its-2015-04/2015-08-31/mod/corep_ind.xsd";
            var xmldoc = new XmlDocument();
            xmldoc.Load(instancepath);

            var nss = Utilities.Xml.GetTaxonomyNamespaceManager(xmldoc);
            var z = nss.GetNamespacesInScope(XmlNamespaceScope.Local);
            var zz = nss.GetNamespacesInScope(XmlNamespaceScope.All);
         
           
            //var schemans="http://www.xbrl.org/2003/instance";
            //var fsns = "http://www.eurofiling.info/xbrl/ext/filing-indicators";
            //var fspath=@"C:\Users\vladimir.balacescu\AppData\Roaming\XbrlStudio\Taxonomies\www.eurofiling.info\eu\fr\xbrl\ext\filing-indicators.xsd";
            //var schpath =@"C:\Users\vladimir.balacescu\AppData\Roaming\XbrlStudio\Taxonomies\www.xbrl.org\2003\xbrl-instance-2003-12-31.xsd";

            ////var t_location2 = @"C:\Users\vladimir.balacescu\AppData\Roaming\XbrlStudio\Taxonomies\www.eba.europa.eu\eu\fr\xbrl\crr\fws\corep\its-2015-04\2015-08-31\mod\corep_ind.xsd";
            ////var t_ns = "http://www.eba.europa.eu/xbrl/crr/fws/corep/its-2015-04/2015-08-31/mod/COREP_Ind";


            var t_ns_path = @"C:\Users\vladimir.balacescu\AppData\Roaming\XbrlStudio\Taxonomies\www.eba.europa.eu\eu\fr\xbrl\crr\fws\corep\its-2015-04\2016-01-31\mod\corep_alm_ind.xsd";
            var t_ns = "http://www.eba.europa.eu/xbrl/crr/fws/corep/its-2015-04/2016-01-31/mod/COREP_ALM_Ind";

            var met_ns = "http://www.eba.europa.eu/xbrl/crr/dict/met";
            var met_ns_path = @"C:\Users\vladimir.balacescu\AppData\Roaming\XbrlStudio\Taxonomies\www.eba.europa.eu\eu\fr\xbrl\crr\dict\met\met.xsd";
            var ct_ns = "http://xbrl.org/2006/xbrldi";
            var ct_ns_path = @"C:\Users\vladimir.balacescu\AppData\Roaming\XbrlStudio\Taxonomies\www.xbrl.org\2006\xbrldi-2006.xsd";
            var i_ns = "http://www.xbrl.org/2003/instance";
            var i_ns_path = @"C:\Users\vladimir.balacescu\AppData\Roaming\XbrlStudio\Taxonomies\www.xbrl.org\2003\xbrl-instance-2003-12-31.xsd";
            
            XmlReaderSettings booksSettings = new XmlReaderSettings();
          
            //booksSettings.Schemas.Add(schemans, schpath);
            //booksSettings.Schemas.Add(fsns, fspath);

            //booksSettings.Schemas.Add(t_ns, t_ns_path);
            //booksSettings.Schemas.Add(met_ns, met_ns_path);
            //booksSettings.Schemas.Add(ct_ns, ct_ns_path);

            //booksSettings.Schemas.Add(i_ns, i_ns_path);
            booksSettings.Async = false;
            booksSettings.DtdProcessing = DtdProcessing.Parse;
            booksSettings.ValidationType = ValidationType.Schema;
            booksSettings.ConformanceLevel = ConformanceLevel.Auto;
            booksSettings.ValidationFlags = 
                XmlSchemaValidationFlags.ProcessIdentityConstraints |
                XmlSchemaValidationFlags.ProcessInlineSchema |
                XmlSchemaValidationFlags.ReportValidationWarnings | 
                XmlSchemaValidationFlags.ProcessSchemaLocation;
            foreach (var item in z)
            {
                booksSettings.Schemas.Add(null, item.Value);

            }
            booksSettings.ValidationEventHandler += new ValidationEventHandler(booksSettingsValidationEventHandler);

            XmlReader books = XmlReader.Create(instancepath, booksSettings);
            while (books.Read()) { }
         
        }
        public static void test2() 
        {
            var path = @"C:\My\XBRL\XBRL Files\XBRL Files\Support\Reg_400_Corep_20160930_30_09_2016.xbrl";
            var xmldoc = new XmlDocument();
            xmldoc.Load(path);

            xmldoc.Validate(booksSettingsValidationEventHandler);
        }
        static void booksSettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                Console.Write("WARNING: ");
                Console.WriteLine(e.Message);
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                Console.Write("ERROR: ");
                Console.WriteLine(e.Message);
            }
        }
        protected void SettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
               // messages.Add(String.Format("Warining: {0}", e.Message));

            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                if (e.Message != "The 'localpath' attribute is not allowed.")
                {

                    messages.Add(String.Format("Error: {0}", e.Message));
                }

            }
        }

    }
}
