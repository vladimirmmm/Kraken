using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using System.Data;
using System.IO;
using XBRLProcessor;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var taxonomyentry = @"C:\My\Tasks\!Tools\Taxonomies\XBRl taxonomy 2.2\XBRL Taxonomy and Supporting Documents.2.2\Taxonomy\2.2.0.0\www.eba.europa.eu\eu\fr\xbrl\crr\fws\corep\its-2013-02\2014-07-31\mod\corep_ind.xsd";
            var engine = new XbrlEngine();
            engine.LoadTaxonomy(taxonomyentry);
            //xsd.ReadXml(taxonomyentry, XmlReadMode.IgnoreSchema);

            var x = 0;
        }
    }
}
