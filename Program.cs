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
        //#region Constants

        //public const string FILE_FLAG = "-f";
        //public const string DIRECTORY_FLAG = "-d";
        //public const string URL_FLAG = "-u";
        //public const string URL_PARSED_FLAG = "-up";


        //public const string HELP_COMMAND = "Commands : \nhelp - show possible commands\n" +
        //        "-f <filePath> <word> or <\"sentence\">\n" +
        //        "-d <directoryPath> <word> or <\"sentence\">\n" +
        //        "-u <url> <word> or <\"sentence\">\n" +
        //        "-up <url> <word> or <\"sentence\">\n" +
        //        "Use -up to show parsed web page content\n" +
        //        "Do not use brackets \"<,>\"\n";
                



        //public const string BAD_INPUT_MESSAGE = "Bad Input!\n" +
        //    "You need to enter :\n" +
        //    "flag (-f, -d, -u)\n" +
        //    "path\n" +
        //    "searched text\n" +
        //    "Enter \"help\" to see possible commands";

        //#endregion
        //#region Functions

        //public static string operationSelector(string[] args)
        //{

        //    string flag = args[0];
        //    string pathToSource = args[1];
        //    string searchedValue = args[2];
        //    if (flag.Equals(FILE_FLAG))
        //    {
        //        return fileSelected(pathToSource, searchedValue);
        //    }
        //    else if (flag.Equals(Flag.DIRECTORY_FLAG))
        //    {
        //        return directorySelected(pathToSource, searchedValue);
        //    }else if (flag.Equals(URL_FLAG) || flag.Equals(URL_PARSED_FLAG))
        //    {
        //        if (flag.Equals(URL_PARSED_FLAG))
        //        {
        //            return urlSelected(pathToSource, searchedValue, true);
        //        }
        //        else
        //        {
        //            return urlSelected(pathToSource, searchedValue, false);
        //        } 

        //    }
        //    return BAD_INPUT_MESSAGE;
        //}
        //#region File Selected
        //private static string fileSelected(string pathToSource,string searchedValue)
        //{
        //    string result = string.Empty;
        //    const int MAX_NUMB_ITERATION = 50;
        //    int currentIteration = 0;
        //    try
        //    {
        //        string FILENAME = getFileName(pathToSource);
        //        if (!FILENAME.EndsWith(".txt")) return BAD_INPUT_MESSAGE;
        //        string path = Path.GetFullPath(System.Reflection.Assembly.GetExecutingAssembly().Location);
        //        string filepath;
        //        do
        //        {
        //            path = Path.GetDirectoryName(path);
        //            filepath = String.Format("{0}{1}{2}", path, Path.DirectorySeparatorChar, FILENAME);
        //            if (filepath.StartsWith(@"\\") || currentIteration >= MAX_NUMB_ITERATION)
        //            {
        //                return "File not found";
        //            }
        //            currentIteration++;
        //        } while (!File.Exists(filepath));
        //        Console.WriteLine(filepath);

        //        using (var reader = new StreamReader(filepath))
        //        {
        //            var content = new List<string>();
        //            while (!reader.EndOfStream)
        //            {
        //                content.Add(reader.ReadLine());
        //            }
        //            FileSelected.filePath = filepath;
        //            FileSelected.data = content;
        //            result = searcher(searchedValue);
        //         }                
        //    }
        //    catch (Exception ex)
        //    {
        //        return BAD_INPUT_MESSAGE;
        //    }
        //    return result;
        //}


        //private static string getFileName(string pathToSource)
        //{ 
        //    string FileName = @pathToSource;
        //    var temp_list = FileName.Split((char)92);

        //    string FILENAME = temp_list[temp_list.Length - 2] + ((char)92) + temp_list[temp_list.Length - 1];
        //    return FILENAME;
        //}


        //private static string searcher(string searchedValue)
        //{
        //    var content = FileSelected.data.ToList();
        //    int lineNumber = 1;
        //    var outputList = new List<string>();
        //    outputList.Add("Searched data : " + searchedValue);
        //    foreach (var line in content)
        //    {
        //        if (line.Contains(searchedValue))
        //        {
        //            outputList.Add("Line Number : " + lineNumber.ToString() + "\nLine: " + line.ToString());
        //        }
        //        lineNumber++;
        //    }
        //    if (outputList.Count == 1)
        //    {
        //        return "No matches found";
        //    }
        //    return string.Join("\n", outputList.ToArray()); 
        //}

        //#endregion

        //#region Folder Selected
        //private static string directorySelected(string pathToSource, string searchedValue)
        //{
        //    try
        //    {
        //        string result = string.Empty;
        //        if (!getFiles(@pathToSource))
        //        {
        //            return BAD_INPUT_MESSAGE;
        //        }
        //        return getContent(searchedValue);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BAD_INPUT_MESSAGE;
        //    }

        //}

        //private static string getContent(string searchedValue)
        //{
        //    var files = new List<string>();
        //    foreach (var item in FolderSelected.allFiles)
        //    {
        //        if (item.ToString().EndsWith(".txt") || item.ToString().EndsWith(".docx"))
        //        {
        //            files.Add(item);
        //        }
        //    }

        //    if (files.Count == 0)
        //    {
        //        return "Folder don't exist .txt or .docx files";
        //    }

        //    for (int i = 0; i < files.Count; ++i)
        //    {
        //        var temp_list_Lines = new List<string>();

        //        using (StreamReader stream = new StreamReader(@files[i]))
        //        {
        //            while (!stream.EndOfStream)
        //                temp_list_Lines.Add(stream.ReadLine());
        //        }

        //        int lineNumber = 1;

        //        foreach (var line in temp_list_Lines)
        //        {
        //            if (line.Contains(searchedValue))
        //            {
        //                string toSet = "File : " + files[i];
        //                if (!FolderSelected.results.Contains(toSet))
        //                {
        //                    FolderSelected.results.Add("File : " + files[i]);
        //                }
        //                FolderSelected.results.Add("Line number : " + lineNumber.ToString() + "\nLine : \n" + line);

        //            }
        //            lineNumber++;
        //        }
        //    }

        //    if (FolderSelected.results.Count == 0)
        //    {
        //        return "No matches found";
        //    }

        //    string tempLine = "Searched Value : " + searchedValue + "\n";
        //    return tempLine + string.Join("\n", FolderSelected.results.ToArray());
        //}
        //private static bool getFiles(string path)
        //{
        //    try
        //    {
        //        var list = Directory.GetFiles(path);
        //        FolderSelected.allFiles = list.ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        //#endregion

        //#region URI Selected
        //private static string urlSelected(string pathToSource,string searchedValue, bool isParsed)
        //{
        //    if (writeContent(pathToSource).ToString().Equals("Bad URI"))
        //    {
        //        return "Bad URI";
        //    }
        //    if (isParsed)
        //    {
        //        htmlParser();
        //    }

        //    return URLSearhcer(searchedValue);
        //}

        //private static string writeContent(string uri)
        //{
        //    var html = new List<string>();
        //    try
        //    {
        //        WebRequest request = WebRequest.Create(uri);
        //        WebResponse response = request.GetResponse();
        //        Stream data = response.GetResponseStream();
        //        using (StreamReader sr = new StreamReader(data))
        //        {
        //            while (!sr.EndOfStream)
        //                html.Add(sr.ReadLine() + "\n");
        //        }
        //        URLSelected.data = html;
        //        URLSelected.uri = uri;
        //    }
        //    catch (Exception ex)
        //    {
        //        return "Bad URI";
        //    }
        //    return "Good URI";
        //}


        //private static string URLSearhcer(string searchedValue)
        //{
        //    var content = URLSelected.data.ToList();
        //    int lineNumber = 1;
        //    var listResults = new List<string>();
        //    listResults.Add("Searched data : " + searchedValue);
        //    foreach (var line in content)
        //    {
        //        if (line.Contains(searchedValue))
        //        {
        //            listResults.Add("Line Number : " + lineNumber + "\n" + "Line : " + line.ToString());
        //        }
        //        lineNumber++;
        //    }

        //    if (listResults.Count == 1)
        //    {
        //        return "No matches found";
        //    }
        //    string result = string.Join("\n", listResults.ToArray());
        //    return result;
        //}

        //private static void htmlParser()
        //{
        //    var htmlLinesList = URLSelected.data;
        //    var urlParsedData = new List<string>();
        //    const char startSymbol = '>';
        //    const char endSymbol = '<';
        //    foreach (var line in htmlLinesList)
        //    {
        //        char[] arrS = line.ToCharArray();

        //        int startIndex = -1;
        //        int endIndex = -1;
        //        int distance = -1;

        //        int cutIndexFrom = 0;
        //        int cutIndexTo = 0;
        //        for (int i = 0; i < arrS.Length; ++i)
        //        {
        //            if (arrS[i] == startSymbol)
        //            {
        //                startIndex = i;
        //            }

        //            if (arrS[i] == endSymbol)
        //            {
        //                endIndex = i;
        //                if (distance < (endIndex - startIndex))
        //                {
        //                    distance = endIndex - startIndex;
        //                    cutIndexFrom = startIndex;
        //                    cutIndexTo = endIndex;
        //                }
        //            }
        //        }
        //        if ((cutIndexTo - cutIndexFrom - 1) > 0)
        //        {
        //            string newLine = line.Substring(cutIndexFrom + 1, (cutIndexTo - cutIndexFrom - 1));
        //            urlParsedData.Add(newLine.Trim());
        //        }
        //    }

        //    URLSelected.data = urlParsedData;
        //}

        //#endregion
        
        //#endregion
        static void Main(string[] args)
        {

            if (args.Length == 0)
            {
                Console.WriteLine("Empty Input");
            }
            else if (args.Length == 1 && args[0].ToString().Trim().Equals("help"))
            {
                Console.WriteLine(Message.HELP_COMMAND);
            }
            else if (args.Length == 3)
            {
                OperationSelector selector = new OperationSelector();
                Console.WriteLine(selector.selectOperation(args));
            }
            else
            {
                Console.WriteLine(Message.BAD_INPUT);
            }

        }
    }
}
