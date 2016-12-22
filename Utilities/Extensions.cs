using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class ObjectExtensions
    {
        public static bool In<T>(this T obj, params T[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (Object.Equals(obj, values[i]))
                    return true;
            }
            return false;
        }

        public static bool In<T>(this T obj, params String[] values)
        {
            var stringval = String.Format("{0}", obj);
            if (obj == null) { obj = (T)new Object(); }
            for (int i = 0; i < values.Length; i++)
            {
                //if (String.Equals(obj, values[i]))
                //    return true;
                if (obj.Equals(values[i]))
                    return true;
            }
            return false;
        }
        public static bool In<T>(this T obj, List<String> values)
        {
            var stringval = String.Format("{0}", obj);
            if (obj == null) { obj = (T)new Object(); }
            for (int i = 0; i < values.Count; i++)
            {
                //if (String.Equals(obj, values[i]))
                //    return true;
                if (obj.Equals(values[i]))
                    return true;
            }
            return false;
        }
        public static bool IsAllNull<T>(this List<T> obj)
        {
            return IsAllNull(obj.ToArray());
        }
        public static bool IsAllNull<T>(this T[] obj)
        {
            for (int i = 0; i < obj.Length; i++)
            {
                if  (obj[i]!=null)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsAllTrue(this bool[] obj)
        {
            for (int i = 0; i < obj.Length; i++)
            {
                if (!obj[i])
                {
                    return false;
                }
            }
            return true;
        }
    }

    public static class StringExtensions
    {

        public static string TextBetween(this String text, string begintag, string endtag)
        {
            return Strings.TextBetween(text, begintag, endtag);
        }

        public static string[] Split(this string text, string Splitter)
        {
            return text.Split(new string[] { Splitter }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string GetBefore(this string text, string Splitter)
        {
            string result = "";
            if (text.IndexOf(Splitter) > 0)
            {
                result = text.Remove(text.IndexOf(Splitter)).Trim();
            }
            return result;
        }
        public static string GetAfter(this string text, string Splitter)
        {
            string result = "";
            if (text.IndexOf(Splitter) > 0)
            {
                result = text.Substring(text.IndexOf(Splitter) + 1).Trim();
            }
            return result;
        }





        public static string NormalizeFolder(string folder)
        {
            if (folder == null) { folder = ""; }
            if (folder[0] != '~') { folder = "~" + folder; }
            if (folder[folder.Length - 1] != '/')
            {
                folder = folder + "/";
            }

            return folder;
        }
    }
    public static class DateTimeExtensions
    {
 
    }
}
