﻿using System;
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
        #region Constants

        public const string FILE_FLAG = "-f";
        public const string DIRECTORY_FLAG = "-d";
        public const string URL_FLAG = "-u";


        public const string HELP_COMMAND = "Commands : \nhelp - show possible commands\n" +
                "-f <filePath> <word> or <\"sentence\">\n" +
                "-d <directoryPath> <word> or <\"sentence\">\n" +
                "-u <url> <word> or <\"sentence\">\n" +
                "Do not use brackets \"<,>\"";



        public const string BAD_INPUT_MESSAGE = "Bad Input!\n" +
            "You need to enter :\n" +
            "flag (-f, -d, -u)\n" +
            "path\n" +
            "searched text\n" +
            "Enter \"help\" to see possible commands";

        #endregion
        #region Functions

        public static string operationSelector(string[] args)
        {

            string flag = args[0];
            string pathToSource = args[1];
            string searchedValue = args[2];
            if (flag.Equals(FILE_FLAG))
            {
                return fileSelected(pathToSource, searchedValue);
            }
            else if (flag.Equals(DIRECTORY_FLAG))
            {
                return directorySelected(pathToSource, searchedValue);
            }else if (flag.Equals(URL_FLAG))
            {
                return urlSelected(pathToSource, searchedValue);
            }
            return BAD_INPUT_MESSAGE;
        }
        #region File Selected
        private static string fileSelected(string pathToSource,string searchedValue)
        {
            string result = string.Empty;
            const int MAX_NUMB_ITERATION = 50;
            int currentIteration = 0;
            try
            {
                string FILENAME = getFileName(pathToSource);
                if (!FILENAME.EndsWith(".txt")) return BAD_INPUT_MESSAGE;
                string path = Path.GetFullPath(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string filepath;
                do
                {
                    path = Path.GetDirectoryName(path);
                    filepath = String.Format("{0}{1}{2}", path, Path.DirectorySeparatorChar, FILENAME);
                    if (filepath.StartsWith(@"\\") || currentIteration >= MAX_NUMB_ITERATION)
                    {
                        return "File not found";
                    }
                    currentIteration++;
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
                    result = searcher(searchedValue);
                 }                
            }
            catch (Exception ex)
            {
                return BAD_INPUT_MESSAGE;
            }
            return result;
        }


        private static string getFileName(string pathToSource)
        { 
            string FileName = @pathToSource;
            var temp_list = FileName.Split((char)92);

            string FILENAME = temp_list[temp_list.Length - 2] + ((char)92) + temp_list[temp_list.Length - 1];
            return FILENAME;
        }


        private static string searcher(string searchedValue)
        {
            var content = FileData.data.ToList();
            int lineNumber = 1;
            var outputList = new List<string>();
            outputList.Add("Searched data : " + searchedValue);
            foreach (var line in content)
            {
                if (line.Contains(searchedValue))
                {
                    outputList.Add("Line Number : " + lineNumber.ToString() + "\nLine: " + line.ToString());
                }
                lineNumber++;
            }
            if (outputList.Count == 1)
            {
                return "No matches found";
            }
            return string.Join("\n", outputList.ToArray()); 
        }

        #endregion

        #region Folder Selected
        private static string directorySelected(string pathToSource, string searchedValue)
        {
            try
            {
                string result = string.Empty;
                if (!getFiles(@pathToSource))
                {
                    return BAD_INPUT_MESSAGE;
                }
                return getContent(searchedValue);
            }
            catch (Exception ex)
            {
                return BAD_INPUT_MESSAGE;
            }

        }

        private static string getContent(string searchedValue)
        {
            var files = new List<string>();
            foreach (var item in FolderData.allFiles)
            {
                if (item.ToString().EndsWith(".txt") || item.ToString().EndsWith(".docx"))
                {
                    files.Add(item);
                }
            }

            if (files.Count == 0)
            {
                return "Folder don't exist .txt or .docx files";
            }

            for (int i = 0; i < files.Count; ++i)
            {
                var temp_list_Lines = new List<string>();

                using (StreamReader stream = new StreamReader(@files[i]))
                {
                    while (!stream.EndOfStream)
                        temp_list_Lines.Add(stream.ReadLine());
                }

                int lineNumber = 1;

                foreach (var line in temp_list_Lines)
                {
                    if (line.Contains(searchedValue))
                    {
                        string toSet = "File : " + files[i];
                        if (!FolderData.results.Contains(toSet))
                        {
                            FolderData.results.Add("File : " + files[i]);
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

            string tempLine = "Searched Value : " + searchedValue + "\n";
            return tempLine + string.Join("\n", FolderData.results.ToArray());
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
        private static string urlSelected(string pathToSource,string searchedValue)
        {
            if (writeContent(pathToSource).ToString().Equals("Bad URI"))
            {
                return "Bad URI";
            }

            string result = URISearhcer(searchedValue);
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
                    listResults.Add("Line Number : " + lineNumber + "\n" + "Line : " + line.ToString());
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
        
        #endregion
        static void Main(string[] args)
        {

            if (args.Length == 0)
            {
                Console.WriteLine("Empty Input");
            }
            else if (args.Length == 1 && args[0].ToString().Trim().Equals("help"))
            {
                Console.WriteLine(HELP_COMMAND);
            }
            else if (args.Length == 3)
            {
                Console.WriteLine(operationSelector(args));
            }
            else
            {
                Console.WriteLine(BAD_INPUT_MESSAGE);
            }

        }
    }
}
