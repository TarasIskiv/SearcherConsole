using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearcherConsole
{
    internal class FolderSelected
    {
        internal List<string> allFiles;
        internal List<string> results = new List<string>();
        internal string searchedValue;
        public FolderSelected(string searchedValue) 
        {
            this.searchedValue = searchedValue;
        }

        public string getResult(string pathToSource)
        {
            try
            {
                string result = string.Empty;
                if (!getFiles(@pathToSource))
                {
                    return Message.BAD_INPUT;
                }
                return getContent(searchedValue);
            }
            catch (Exception ex)
            {
                return Message.BAD_INPUT;
            }

        }

        private string getContent(string searchedValue)
        {
            var files = new List<string>();
            foreach (var item in this.allFiles)
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
                        if (!this.results.Contains(toSet))
                        {
                            this.results.Add("File : " + files[i]);
                        }
                        this.results.Add("Line number : " + lineNumber.ToString() + "\nLine : " + line);

                    }
                    lineNumber++;
                }
            }

            if (this.results.Count == 0)
            {
                return "No matches found";
            }

            string tempLine = "Searched Value : " + searchedValue + "\n";
            return tempLine + string.Join("\n", this.results.ToArray());
        }
        private bool getFiles(string path)
        {
            try
            {
                var list = Directory.GetFiles(path);
                this.allFiles = list.ToList();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
