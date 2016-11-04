using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Web
{
    public class JsonNetResult : ActionResult
    {
        public Encoding ContentEncoding { get; set; }
        public string ContentType { get; set; }
        public object Data { get; set; }

        public JsonSerializerSettings SerializerSettings { get; set; }
        public Formatting Formatting { get; set; }

        public JsonNetResult()
        {
            SerializerSettings = new JsonSerializerSettings();
            SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;

            //Newtonsoft.Json.Serialization.DefaultContractResolver dcr = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            //dcr.DefaultMembersSearchFlags = System.Reflection.BindingFlags.NonPublic;
            //jss.ContractResolver = dcr;
            SerializerSettings.ContractResolver = new Utilities.PublicContractResolver();
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            HttpResponseBase response = context.HttpContext.Response;

            response.ContentType = !string.IsNullOrEmpty(ContentType)
              ? ContentType
              : "application/json";

            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;

            if (Data != null)
            {
                JsonTextWriter writer = new JsonTextWriter(response.Output) { Formatting = Formatting };

                JsonSerializer serializer = JsonSerializer.Create(SerializerSettings);
                serializer.Error+=serializer_Error;
                serializer.Serialize(writer, Data);

                writer.Flush();
             
            }
        }

        private void serializer_Error(object sender, Newtonsoft.Json.Serialization.ErrorEventArgs e)
        {
            throw new NotImplementedException();
        }
    }

}