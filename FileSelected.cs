using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearcherConsole
{
    internal class FileSelected : ISelectedType
    {
        internal string filePath;
        internal List<string> data;
        internal string searchedValue;

        public FileSelected(string searchedValue)
        {
            this.searchedValue = searchedValue;
            data = new List<string>();
            filePath = string.Empty;
        }


        public string getResult(string pathToSource)
        {
            string result = string.Empty;
            const int MAX_NUMB_ITERATION = 50;
            int currentIteration = 0;
            try
            {
                string FILENAME = getFileName(pathToSource);
                if (!FILENAME.EndsWith(".txt")) return Message.BAD_INPUT;
                string path = Path.GetFullPath(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string temporaryFilePath;
                do
                {
                    path = Path.GetDirectoryName(path);
                    temporaryFilePath = String.Format("{0}{1}{2}", path, Path.DirectorySeparatorChar, FILENAME);
                    if (temporaryFilePath.StartsWith(@"\\") || currentIteration >= MAX_NUMB_ITERATION)
                    {
                        return "File not found";
                    }
                    currentIteration++;
                } while (!File.Exists(temporaryFilePath));
                Console.WriteLine(temporaryFilePath);

                using (var reader = new StreamReader(temporaryFilePath))
                {
                    var content = new List<string>();
                    while (!reader.EndOfStream)
                    {
                        content.Add(reader.ReadLine());
                    }
                    filePath = temporaryFilePath;
                    data = content;
                    result = searcher(searchedValue);
                }
            }
            catch (Exception ex)
            {
                return Message.BAD_INPUT;
            }
            return result;
        }


        private string getFileName(string pathToSource)
        {
            string FileName = @pathToSource;
            var temp_list = FileName.Split((char)92);

            string FILENAME = temp_list[temp_list.Length - 2] + ((char)92) + temp_list[temp_list.Length - 1];
            return FILENAME;
        }


        private string searcher(string searchedValue)
        {
            var content = data.ToList();
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
    }
}
