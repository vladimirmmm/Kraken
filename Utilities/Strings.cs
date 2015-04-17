using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        public static string GetFolder(string FilePath)
        {
            if (!String.IsNullOrEmpty(FilePath))
            {
                if (FilePath.Contains("\\"))
                {
                    return FilePath.Remove(FilePath.LastIndexOf("\\") + 1);
                }
                if (FilePath.Contains("/"))
                {
                    return FilePath.Remove(FilePath.LastIndexOf("/") + 1);
                }
            }
            return "";
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
        public static string ResolveRelativePath(string referencePath, string relativePath)
        {
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
            if ((!FilePath.Contains("\\") && !FilePath.Contains("/")) 
                || FilePath.StartsWith(".."))
            {
                return true;
            }
            return false;
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
            sourcepath = sourcepath.Substring(sourcepath.IndexOf("www."));
            sourcepath = localrootfolder + sourcepath;
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
                    System.IO.File.WriteAllText(localpath, content);
                }
                else
                {
                    System.IO.File.Copy(sourcepath, localpath);

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

        public static string TextBetween(String text, string begintag, string endtag)
        {
            if (!String.IsNullOrEmpty(text))
            {
                int i1 = text.IndexOf(begintag, StringComparison.Ordinal);
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
            while (Text.Contains(BeginTag) & Text.Contains(EndTag))
            {
                cs = TextBetween(Text, BeginTag, EndTag);
                Text = RemoveString(Text, BeginTag + cs + EndTag);
                StringList.Add(cs);
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

        public static string ArrayToString(string[] arr)
        {
            string rs = "";
            for (int i = 0; i < arr.Length; i++)
            {
                rs += "," + arr[i];
            }
            if (rs.StartsWith(","))
            {
                rs = rs.Substring(1);
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
    }
}
