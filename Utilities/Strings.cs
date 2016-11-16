﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Utilities
{
    public partial class KeyWord
    {
        public String Name;
        public int Density;
        public KeyWord(String pName, int pDensity)
        {
            this.Name = pName;
            this.Density = pDensity;
        }
    }
    
    public class Strings
    {
        public static void testc()
        {
            var str="sdgsdg,dfgs,sdg,";
            var result = Strings.FactSplit(str, ',', 3);
        }
        public static List<string> FactSplit(string input,char splitter,int minlength=1)
        {
            var Result = new List<string>(20);
            //foreach (var ch in input)
            char ch;
            int lastix = 0;
            for (int i = 0; i < input.Length; i++)
            {
                //ch = input[i];
                if (input[i] == splitter)
                {
                    var word = input.Substring(lastix, i - lastix);
                    if (!String.IsNullOrEmpty(word)) 
                    {
                        Result.Add(word);
                        lastix = i + 1;
                        i = i + minlength-1;
                    }
                 
                }
           
            }
            //if (Result.Count == 0) 
            //{
            //    Result.Add(input);
            //}
            return Result;
        }

        public static string GetFolder(string FilePath)
        {
            if (!String.IsNullOrEmpty(FilePath))
            {
                if (FilePath.Contains("\\") && !FilePath.EndsWith("\\"))
                {
                    return FilePath.Remove(FilePath.LastIndexOf("\\") + 1);
                }
                if (FilePath.Contains("/") && !FilePath.EndsWith("/"))
                {
                    return FilePath.Remove(FilePath.LastIndexOf("/") + 1);
                }
            }
            return FilePath;
        }
        public static string GetFolderName(string FilePath)
        {
            var folder = GetFolder(FilePath);
            var foldername = folder.Trim('\\').Trim('/');
            if (foldername.Contains("\\"))
            {
                foldername = foldername.Substring(foldername.LastIndexOf("\\") + 1);
            }
            if (foldername.Contains("/"))
            {
                foldername = foldername.Substring(foldername.LastIndexOf("/") + 1);
            }
            return foldername;
        }
        public static string GetFileName(string FilePath)
        {
            var filename = "";
            if (!String.IsNullOrEmpty(FilePath))
            {
                if (FilePath.Contains("\\"))
                {
                    filename = FilePath.Substring(FilePath.LastIndexOf("\\") + 1);
                }
                if (FilePath.Contains("/"))
                {
                    filename = FilePath.Substring(FilePath.LastIndexOf("/") + 1);
                }
                var dot_ix = filename.LastIndexOf(".");
                var hash_ix = filename.LastIndexOf("#");
                if (hash_ix > dot_ix) 
                {
                    filename = filename.Remove(hash_ix);
                }
            }
            return filename;
        }
        public static string GetFileNameWithoutExtension(string FilePath)
        {
            var filename = "";
            if (!String.IsNullOrEmpty(FilePath))
            {
                if (FilePath.Contains("\\"))
                {
                    filename = FilePath.Substring(FilePath.LastIndexOf("\\") + 1);
                }
                if (FilePath.Contains("/"))
                {
                    filename = FilePath.Substring(FilePath.LastIndexOf("/") + 1);
                }
                var dot_ix = filename.LastIndexOf(".");
                filename = filename.Remove(dot_ix);
            }
            return filename;
        }
        public static string GetRelativePath(string referencePath, string absolutePath)
        {
            var lowerref = referencePath.ToLower();
            var lowerabs = absolutePath.ToLower();
            var ix = lowerabs.IndexOf(lowerref);
            if (ix == 0) 
            {
                var relpath = absolutePath.Substring(lowerref.Length);
                relpath = /*"..\\" +*/ relpath;
                return relpath;
            }
            return absolutePath;
        }
        public static string ResolveRelativePath(string referencePath, string relativePath,string localrootpath)
        {
            if (relativePath.StartsWith("http://") || relativePath.StartsWith("https://"))
            {
                return GetLocalPath(localrootpath, relativePath);
            }
            else 
            {
                return ResolveRelativePath(referencePath, relativePath);
            }
        }
        public static string ResolveRelativePath(string referencePath, string relativePath)
        {
            if (relativePath.StartsWith("./")) { 
                relativePath = relativePath.Substring(2); 
            }
            var hashix = relativePath.IndexOf("#");
            if (hashix > -1)
            {
                relativePath = relativePath.Remove(hashix);
            }
         

            Uri uri = new Uri(Path.Combine(referencePath, relativePath));
            var path = "";
            if (uri.Scheme != "http")
            {
                path = Path.GetFullPath(uri.AbsolutePath);
                path = HttpUtility.UrlDecode(path);

            }
            else
            {
                path = uri.AbsoluteUri;
            }
            return path;
        }
        public static bool IsRelativePath(string FilePath) 
        {
            if (!IsWebPath(FilePath) && !System.IO.Path.IsPathRooted(FilePath)) 
            {
                return true;
            }
            return false;
            //if ((!FilePath.Contains("\\") && !FilePath.Contains("/"))
            //    || FilePath.StartsWith("..") || FilePath.StartsWith("."))
            //{
            //    return true;
            //}
            //return false;
        }

        public static bool IsWebPath(string FilePath)
        {
            if (FilePath.StartsWith("www.") || FilePath.StartsWith("http://"))
            {
                return true;
            }
            return false;
        }

        public static String WebToLocalPath(string localrootfolder, string sourcepath) 
        {
            sourcepath = sourcepath.Replace("http://", "").Replace("/", "\\");
            sourcepath = localrootfolder + sourcepath;
            return sourcepath;
        }

        public static String LocalToLocalPath(string localrootfolder, string sourcepath) 
        {
            var w3index = sourcepath.IndexOf("www.");
            if (w3index > -1)
            {
                sourcepath = sourcepath.Substring(w3index);
            }
            if (!sourcepath.StartsWith(localrootfolder.ToLower()))
            {
                sourcepath = localrootfolder + sourcepath;
            }
            return sourcepath;
        }

        public static String GetLocalPath(string localrootfolder, string sourcepath) 
        {
            if (IsWebPath(sourcepath))
            {
                return WebToLocalPath(localrootfolder, sourcepath);
            }
            else 
            {
                return LocalToLocalPath(localrootfolder, sourcepath);

            }
        }

        public static void CopyToLocal(string sourcepath,string localpath, bool overwrite=false)
        {
            if (!System.IO.File.Exists(localpath) || overwrite)
            {
                var folder = GetFolder(localpath);
                if (!System.IO.Directory.Exists(folder))
                {
                    System.IO.Directory.CreateDirectory(folder);
                }
                if (IsWebPath(sourcepath))
                {

                    var content = Utilities.Web.readfromWeb(sourcepath);
                    Utilities.FS.WriteAllText(localpath, content);

                }
                else
                {
                    Utilities.FS.Copy(sourcepath, localpath);

                }
            }
        }

        public static List<KeyValue> ReadKeyValues(string file) 
        {
            List<KeyValue> result = new List<KeyValue>();
            string content = System.IO.File.ReadAllText(file);
            string[] lines = content.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines) 
            {
                if (line.IndexOf(":") > -1) 
                {
                    string key = line.Remove(line.IndexOf(":")).Trim();
                    string value = line.Substring(line.IndexOf(":")+1).Trim();
                    result.Add(new KeyValue(key, value));
                }
            }
            return result;
        }

        public static string[] SplitSimple(string text, string separator) 
        {
            return text.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string[] SplitPreserve(string text, string[] separators)
        {
            var result = new List<string>();
            for (int i = 0; i < separators.Length; i++) 
            {
                var separator = separators[i];
                var s_ix=0;
                var ix = text.IndexOf(separator,s_ix);
                while (ix > -1) 
                {
                    var item = text.Substring(s_ix, ix);
                    var sepitem = text.Substring(ix, separator.Length);
                    result.Add(item);
                    result.Add(sepitem);
                    s_ix = ix + separator.Length;
                    ix = text.IndexOf(separator, s_ix);
                }
            }
            foreach (var item in result) 
            {

            }
            return result.ToArray();
        }

        public static string TextBetween(String text, string begintag, string endtag, int startindex=0)
        {
            if (!String.IsNullOrEmpty(text))
            {
                int i1 = text.IndexOf(begintag, startindex, StringComparison.Ordinal);
                if (i1 > -1)
                {
                    i1 = i1 + begintag.Length;
                    int i2 = text.IndexOf(endtag, i1, StringComparison.Ordinal);
                    if (i2 > -1)
                    {
                        return text.Substring(i1, i2 - i1);
                    }
                }
            }
            return "";
        }

        public static List<string> TextsBetween(string Text, string BeginTag, string EndTag)
        {
            List<string> StringList = new List<string>();
            string cs = "";
            var six = 0;
            while (Text.IndexOf(BeginTag, six, StringComparison.Ordinal) > -1 & Text.IndexOf(EndTag, six, StringComparison.Ordinal) > -1)
            {
                cs = TextBetween(Text, BeginTag, EndTag, six);
                //Text = RemoveString(Text, BeginTag + cs + EndTag);
                StringList.Add(cs);
                six = Text.IndexOf(BeginTag, six, StringComparison.Ordinal) + BeginTag.Length;
            }
            return StringList;
        }
       
        public static string RemoveString(string Text, string StringToRemove)
        {
            int i1 = Text.IndexOf(StringToRemove);
            if (i1 > -1)
            {
                Text = Text.Remove(i1, StringToRemove.Length);
            }
            return Text;
        }

        public static String Format(string format, object value)
        {
            if (value == null)
            {
                return "";
            }
            else
            {
                return String.Format(format, value);
            }
        }

        public static int GetLevensteinDistance(string firstString, string secondString)
        {
            if (firstString == null)
                throw new ArgumentNullException("firstString");
            if (secondString == null)
                throw new ArgumentNullException("secondString");

            if (firstString == secondString)
                return 0;

            int[,] matrix = new int[firstString.Length + 1, secondString.Length + 1];

            for (int i = 0; i <= firstString.Length; i++)
                matrix[i, 0] = i; // deletion
            for (int j = 0; j <= secondString.Length; j++)
                matrix[0, j] = j; // insertion

            for (int i = 0; i < firstString.Length; i++)
                for (int j = 0; j < secondString.Length; j++)
                    if (firstString[i] == secondString[j])
                        matrix[i + 1, j + 1] = matrix[i, j];
                    else
                    {
                        matrix[i + 1, j + 1] = Math.Min(matrix[i, j + 1] + 1, matrix[i + 1, j] + 1); //deletion or insertion
                        matrix[i + 1, j + 1] = Math.Min(matrix[i + 1, j + 1], matrix[i, j] + 1); //substitution
                    }
            return matrix[firstString.Length, secondString.Length];
        }

        public static double GetSimilarity(string firstString, string secondString)
        {
            if (firstString == null)
                throw new ArgumentNullException("firstString");
            if (secondString == null)
                throw new ArgumentNullException("secondString");
            firstString = Strings.ConvertToEnglishChars(firstString.Trim().Replace(" ", ""));
            secondString = Strings.ConvertToEnglishChars(secondString.Trim().Replace(" ", ""));
            if (firstString == secondString)
                return 1;

            int longestLenght = Math.Max(firstString.Length, secondString.Length);
            int distance = GetLevensteinDistance(firstString, secondString);
            double percent = distance / (double)longestLenght;
            return 1 - percent;
        }
        public static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        public static bool IsDigitsOnly(string str,params char[] except)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                {
                    if (!except.Contains(c))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static string ArrayToString(string[] arr, string delimiter=", ")
        {
            string rs = "";
            for (int i = 0; i < arr.Length; i++)
            {
                rs += delimiter + arr[i];
            }
            if (rs.StartsWith(delimiter))
            {
                rs = rs.Substring(delimiter.Length);
            }
            return rs;
        }
        public static string ArrayToString<T>(T[] arr, string delimiter = ", ")
        {
            string rs = "";
            for (int i = 0; i < arr.Length; i++)
            {
                rs += delimiter + arr[i];
            }
            if (rs.StartsWith(delimiter))
            {
                rs = rs.Substring(delimiter.Length);
            }
            return rs;
        }

        public static string ArrayToString(decimal[] arr)
        {
            string rs = "";
            var delimiter = ", ";
            for (int i = 0; i < arr.Length; i++)
            {
                rs += delimiter + arr[i].ToString();
            }
            if (rs.StartsWith(delimiter))
            {
                rs = rs.Substring(delimiter.Length);
            }
            return rs;
        }
        public static string ArrayToString(int[] arr)
        {
            string rs = "";
            var delimiter = ", ";
            for (int i = 0; i < arr.Length; i++)
            {
                rs += delimiter + arr[i].ToString();
            }
            if (rs.StartsWith(delimiter))
            {
                rs = rs.Substring(delimiter.Length);
            }
            return rs;
        }

        public static string RemoveHTMLTags(string html)
        {
            string result = "";
            if (!string.IsNullOrEmpty(html))
            {
                result = Regex.Replace(html, "<.*?>", string.Empty);
            }
            return result;

        }

        public static string RemoveHTMLComments(string html)
        {
            string result = "";
            if (!string.IsNullOrEmpty(html))
            {
                result = Regex.Replace(html, "<!--.*?-->", String.Empty, RegexOptions.Singleline);
            } 
            return result;
        }

        public static string RemoveHTMLTags(string html, string tags)
        {

            //@"</?(?i:script|embed|object|frameset|frame|iframe|meta|link|style)(.|\n)*?>"
            //script|embed|object|frameset|frame|iframe|meta|link|style
            html = Regex.Replace(html, @"</?(?i:" + tags + ")(.|\n)*?>", "");

            return html;
        }

        public static String OnlyOneSpace(String text)
        {
            while (text.Contains("  "))
            {
                text = text.Replace("  ", " ");
            }
            return text.Trim();
        }

        public static String AlfaNumericOnly(String text)
        {
            return Regex.Replace(text, "[^a-zA-Z0-9 - @]", "");
        }

        public static int ContainsCount(string pattern, string text)
        {
            int result = 0;
            pattern = pattern.ToLower();
            text = text.ToLower();
            if (!string.IsNullOrEmpty(pattern))
            {
                while (text.Contains(pattern))
                {
                    result = result + 1;
                    text = text.Remove(text.IndexOf(pattern), pattern.Length);
                }
            }
            return result;

        }

        public static List<List<string>> GetPhrases(string text, int Wordcount)
        {
            string result = "";
            string[] words = text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            List<string> wordlist = new List<string>();
            List<List<string>> wordSlist = new List<List<string>>();
            int i = 0;
            int j = 0;
            string NotKeyWords = "";
            for (i = 1; i <= Wordcount; i++)
            {
                wordlist = new List<string>();
                int counter = 0;
                string word = "";
                for (j = 0; j <= words.Length - 1; j++)
                {
                    counter = counter + 1;
                    word += " " + words[j];
                    if (counter == i)
                    {
                        word = word.Trim().ToLower();
                        if (wordlist.Contains(word) == false & word.Length > (2 * i) + i & !NotKeyWords.Contains("," + word + ","))
                        {
                            wordlist.Add(word);

                        }
                        counter = counter - 1;
                        if (word.Contains(" "))
                        {
                            word = word.Substring(word.IndexOf(" "));
                        }
                        else
                        {
                            word = "";
                        }

                    }
                    else
                    {
                    }
                }
                wordSlist.Add(wordlist);

            }
            return wordSlist;
        }

        public static String ConvertToEnglishChars(String text)
        {
            text = text == null ? "" : text;
            char[] mixchars = @"áéíóöőúüűșşţțâăîëêèçåäãöôõóőòćĉĝěęėĕēéāġģğċčŝśšúűũūŭůżźžķĵï".ToCharArray();
            char[] engchars = @"aeiooouuussttaaieeecaaaooooooccgeeeeeeagggccsssuuuuuuzzzkji".ToCharArray();
            for (int i = 0; i < mixchars.Length; i++)
            {
                string strChar = mixchars.GetValue(i).ToString();
                string strEngChar = engchars.GetValue(i).ToString();
                if (text.Contains(strChar))
                {
                    text = text.Replace(strChar, strEngChar);
                }
                if (text.Contains(strChar.ToUpper()))
                {
                    text = text.Replace(strChar.ToUpper(), strEngChar.ToUpper());
                }
            }
            return text;
        }

        public static string GetForURL(string text)
        {
            text = ConvertToEnglishChars(text);
            text = text.Replace("-", " ");
            text = Regex.Replace(text, "[^a-zA-Z0-9 - @ -]", "");
            text = OnlyOneSpace(text);
            text = text.Replace(" ", "-");

            return text;
        }

        public static string GetKeyWords(string Title, string text)
        {
            Title = ConvertToEnglishChars(Title);
            text = ConvertToEnglishChars(text);

            Title = AlfaNumericOnly(Title);
            Title = Title.Replace(" at ", ",").Replace(" @ ", ",");
            string result = "";
            if (Title.Length < 80)
            {
                result = Title;
            }
            else
            {
                text = Title + ", " + Title + ", " + text;
            }
            NameValueCollection keywords = new NameValueCollection();
            text = Strings.RemoveHTMLTags(text);
            text = OnlyOneSpace(text);
            text = AlfaNumericOnly(text);
            string[] words = text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            int totalwordcount = words.Length;
            List<List<string>> phrases = new List<List<string>>();
            phrases = GetPhrases(text, 3);
            List<KeyWord> keywordlist = new List<KeyWord>();

            int counter = 0;
            int counter2 = 0;
            foreach (List<string> phrasetype in phrases)
            {
                counter2 = counter2 + 1;
                foreach (string phrase in phrasetype)
                {
                    if (ContainsCount(" " + phrase + " ", " " + text + " ") > 1)
                    {
                        //result += phrase + "," ' ":" + ContainsCount(" " + phrase + " ", " " + text + " ").ToString + ", "
                        KeyWord kw = new KeyWord(phrase.Trim(), ContainsCount(" " + phrase + " ", " " + text + " ") + phrases.Count - counter);

                        //GetGoogleCount(phrase)

                        keywordlist.Add(kw);
                    }
                }
                counter = counter + 1;
                //result += "<br/> <<<<< " + counter.ToString + " word Phrases<br/>"

            }
            keywordlist.OrderByDescending(k => k.Density);
            int limit = keywordlist.Count;
            if (limit > 8)
            {
                limit = 8;
            }

            for (int i = 0; i < limit; i++)
            {
                result += "," + keywordlist[i].Name;
            }
            if (result.Length > 300)
            {
                result = result.Remove(result.LastIndexOf(",", 300, 300));
            }
            return result;
        }

        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        public static string TrimTo(string name, int p)
        {
            if (name.Length > p + 3) 
            {
                name = name.Remove(p);
                name = name + "...";
            }
            return name;
        }

        public static string RemoveFrom(string name, int p)
        {
            if (name.Length > p + 3)
            {
                name = name.Remove(p);
                name = name + "...";
            }
            return name;
        }

        public static string HtmlEncode(string text)
        {
            if (text == null)
                return null;

            StringBuilder sb = new StringBuilder(text.Length);

            int len = text.Length;
            for (int i = 0; i < len; i++)
            {
                switch (text[i])
                {

                    case '<':
                        sb.Append("&lt;");
                        break;
                    case '>':
                        sb.Append("&gt;");
                        break;
                    case '"':
                        sb.Append("&quot;");
                        break;
                    case '&':
                        sb.Append("&amp;");
                        break;
                    default:
                        if (text[i] > 159)
                        {
                            // decimal numeric entity
                            sb.Append("&#");
                            sb.Append(((int)text[i]).ToString(CultureInfo.InvariantCulture));
                            sb.Append(";");
                        }
                        else
                            sb.Append(text[i]);
                        break;
                }
            }
            return sb.ToString();
        }


        public static string EnumerableToString<T>(List<T> items, string delimiter = ", ")
        {
            var rs = new StringBuilder();
            for (int i = 0; i < items.Count; i++)
            {
                if (i > 0) 
                {
                    rs.Append(delimiter);
                }
                rs.Append(items[i]);
            }
       
            return rs.ToString();
        }
    }
}
