using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Web
    {
        public static string readfromWeb(string path)
        {
            string lcUrl = null;
            lcUrl = path;

            // *** Establish the request
            System.Net.HttpWebRequest loHttp = (HttpWebRequest)System.Net.HttpWebRequest.Create(lcUrl);

            // *** Set properties
            loHttp.Timeout = 9000;

            loHttp.UserAgent = "Code Sample Web Client";

            // *** Retrieve request info headers
            System.Net.HttpWebResponse loWebResponse = (HttpWebResponse)loHttp.GetResponse();

            Encoding enc = Encoding.GetEncoding(65001);

            //Dim enc As Encoding = Encoding.GetEncoding(65001)
            //Dim enc As Encoding = Encoding.GetEncoding(1252)
            //// Windows default Code Page

            System.IO.StreamReader loResponseStream = new System.IO.StreamReader(loWebResponse.GetResponseStream(), enc);

            string lcHtml = loResponseStream.ReadToEnd();
            loWebResponse.Close();
            loResponseStream.Close();
            return lcHtml;
        }
    }
}
