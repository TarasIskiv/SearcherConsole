using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SearcherConsole
{
    public class Program
    {

        #region Functions

        public static void selector(ref string OutputValue, string line)
        {
            if (line.Equals("help"))
            {
                OutputValue = helpSelected();
            }
            else
            {
                if (line.StartsWith("-f"))
                {
                    OutputValue = fileSelected(line);
                }
                else if (line.StartsWith("-d"))
                {
                    OutputValue = directorySelected(line);
                }
                else if (line.StartsWith("-u"))
                {
                    OutputValue = uriSelected(line);
                }
                else
                {
                    OutputValue = "Bad input";
                }
            }
        }
        #region File Selected
        private static string fileSelected(string line)
        {
            string result = string.Empty;
            try
            {
                
                line = line.Substring(3).Trim();
                var elements = line.Split(new string[] { " - " }, StringSplitOptions.None).ToList();
                if(elements.Count == 1) { return "Bad Input"; }

                if (!elements.ToString().EndsWith(@"\"))
                {
                    string FILENAME = getFileName(line, elements);
                    string path = Path.GetFullPath(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    string filepath;
                    do
                    {
                        path = Path.GetDirectoryName(path);
                        filepath = String.Format("{0}{1}{2}", path, Path.DirectorySeparatorChar, FILENAME);
                        if (filepath.StartsWith(@"\\"))
                        {
                            return "File not found";
                        }
                    } while (!File.Exists(filepath));
                    Console.WriteLine(filepath);

                    using (var reader = new StreamReader(filepath))
                    {
                        var content = new List<string>();
                        while (!reader.EndOfStream)
                        {
                            content.Add(reader.ReadLine());
                        }

                        FileData.filePath = filepath;
                        FileData.data = content;
                        result = searcher(elements[1]);
                    }
                }
                else
                {
                    result = "Bad Input";
                }
            }
            catch (Exception ex)
            {
                return "Bad Input";
            }
            return result;
        }


        private static string getFileName(string line, List<string> elements)
        {


            string FileName = @elements[0];
            var temp_list = FileName.Split((char)92);

            string FILENAME = temp_list[temp_list.Length - 2] + ((char)92) + temp_list[temp_list.Length - 1];
            return FILENAME;
        }


        private static string searcher(string searchedValue)
        {
            if (searchedValue.StartsWith(((char)34).ToString()) && searchedValue.EndsWith(((char)34).ToString()))
            {
                searchedValue = searchedValue.Substring(1);
                searchedValue = searchedValue.Substring(0, searchedValue.Length - 1);
            }
            var content = FileData.data.ToList();
            int lineNumber = 1;
            var listResults = new List<string>();
            listResults.Add("Searched data : " + searchedValue);
            foreach (var line in content)
            {
                if (line.Contains(searchedValue))
                {
                    listResults.Add("Line Number : " + lineNumber.ToString() + "\nLine: " + line.ToString() + "\n");
                }
                lineNumber++;
            }
            string res = "Bad Input Data";
            if (listResults.Count == 1)
            {
                res = "No matches found";
            }
            else
            {
                res = string.Join("\n", listResults.ToArray());
            }
            return res;
        }

        #endregion

        #region Folder Selected
        private static string directorySelected(string line)
        {
            try
            {
                string result = string.Empty;
                line = line.Substring(3).Trim();
                var elements = line.Split(new string[] { " - " }, StringSplitOptions.None).ToList();
                if(elements.Count == 1) { return "Bad Input"; }
                if (!getFiles(elements[0]))
                {
                    return "Bad Input";
                }
                return getContent(elements[1]);
            }
            catch (Exception ex)
            {
                return "Bad Input";
            }

        }

        private static string getContent(string searchedValue)
        {
            var temp_list = new List<string>();
            foreach (var item in FolderData.allFiles)
            {
                if (item.ToString().EndsWith(".txt") || item.ToString().EndsWith(".docx"))
                {
                    temp_list.Add(item);
                }
            }

            if (temp_list.Count == 0)
            {
                return "Folder don't exist .txt or .docx files";
            }
            for (int i = 0; i < temp_list.Count; ++i)
            {
                var temp_list_Lines = new List<string>();
                using (StreamReader stream = new StreamReader(@temp_list[i]))
                {
                    while (!stream.EndOfStream)
                        temp_list_Lines.Add(stream.ReadLine());
                }
                int lineNumber = 1;
                foreach (var line in temp_list_Lines)
                {
                    if (line.Contains(searchedValue))
                    {
                        string toSet = "File : " + temp_list[i];
                        if (!FolderData.results.Contains(toSet))
                        {
                            FolderData.results.Add("File : " + temp_list[i]);
                        }
                        FolderData.results.Add("Line number : " + lineNumber.ToString() + "\nLine : \n" + line);

                    }
                    lineNumber++;
                }
            }

            if (FolderData.results.Count == 0)
            {
                return "No matches found";
            }


            return string.Join("\n", FolderData.results.ToArray());
        }
        private static bool getFiles(string path)
        {
            try
            {
                var list = Directory.GetFiles(path);
                FolderData.allFiles = list.ToList();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        #endregion
        #region URI Selected
        private static string uriSelected(string line)
        {
            line = line.Substring(3).Trim();
            var elements = line.Split(new string[] { " - " }, StringSplitOptions.None).ToList();
            if (elements.Count == 1) { return "Bad Input"; }
            if (writeContent(elements[0]).ToString().Equals("Bad URI"))
            {
                return "Bad URI";
            }

            string result = URISearhcer(elements[1]);
            return result;
        }

        private static string writeContent(string uri)
        {
            var html = new List<string>();
            try
            {
                WebRequest request = WebRequest.Create(uri);
                WebResponse response = request.GetResponse();
                Stream data = response.GetResponseStream();
                using (StreamReader sr = new StreamReader(data))
                {
                    while (!sr.EndOfStream)
                        html.Add(sr.ReadLine() + "\n");
                }
                URIData.data = html;
                URIData.uri = uri;
            }
            catch (Exception ex)
            {
                return "Bad URI";
            }
            return "Good URI";
        }


        private static string URISearhcer(string searchedValue)
        {
            var content = URIData.data.ToList();
            int lineNumber = 1;
            var listResults = new List<string>();
            listResults.Add("Searched data : " + searchedValue);
            foreach (var line in content)
            {
                if (line.Contains(searchedValue))
                {
                    listResults.Add("Line Number : " + lineNumber + "\n" + "Line : " + line.ToString() + "\n");
                }
                lineNumber++;
            }

            if (listResults.Count == 1)
            {
                return "No matches found";
            }
            string result = string.Join("\n", listResults.ToArray());
            return result;
        }
        #endregion
        private static string helpSelected()
        {
            string resultLine = "Commands : \nhelp - show possible commands\n" +
                " -f <filepath> - <word> or <\"sentence\">\n" +
                " -d <directorypath> - <word> or <\"sentence\">\n" +
                " -u <url> - <word> or <\"sentence\">\n" +
                " exit - close application\n" +
                " Do not use brackets \"<,>\"";
            return resultLine;
        }
        #endregion
        static void Main(string[] args)
        {
            string command = string.Empty;
            string startLine = "console line > ";
            while (true)
            {
                string res = string.Empty;
                Console.Write(startLine);
                command = Console.ReadLine();
                if (command.Equals("exit"))
                {
                    break;
                }
                else if (command.Equals("clear"))
                {
                    Console.Clear();

                } 
                else
                {
                    selector(ref res, command);
                    Console.WriteLine(res);

                }
            }
            
        }
    }
}
