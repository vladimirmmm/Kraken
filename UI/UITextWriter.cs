using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class UITextWriter : TextWriter
    {
        private Action<string> action;
        private StringBuilder sb = new StringBuilder();
        public UITextWriter(Action<string> action)
        {
            this.action = action;
        }

        public override void Write(char value)
        {
            action(value.ToString());

        }

        public override void Write(string value)
        {
            action(value);

        }
        public override void WriteLine(string value) 
        {
            Write(value + "\r\n");
        }

        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }

        public override void Flush()
        {
            var str = sb.ToString();
            sb.Clear();
            action(str);
        }

        //public override void Close()
        //{
        //    foreach (var writer in writers)
        //        writer.Close();
        //}

    }
}
