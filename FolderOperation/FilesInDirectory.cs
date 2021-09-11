using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearcherConsole.FolderOperation
{
    public class FilesInDirectory
    {
        private string _fullPathToDirectory;

        private List<string> _allFilesNames;

        public List<string> AllFilesNames
        {
            get => _allFilesNames; 
            set => _allFilesNames = value; 
        }

        public FilesInDirectory(string pathToDirectory)
        {
            _fullPathToDirectory = pathToDirectory;
        }


        public bool isFilesInDirectory()
        {
            try
            {
                AllFilesNames = Directory.GetFiles(_fullPathToDirectory).ToList();
                if(AllFilesNames.Count == 0)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public List<string> getTxtFilesFromDirectory()
        {
            const string fileEndsWith = ".txt";
            List<string> txtFiles = new List<string>();
            foreach (var item in AllFilesNames)
            {
                if (item.ToString().EndsWith(fileEndsWith))
                {
                    txtFiles.Add(item);
                }
            }
            return txtFiles;
        }
    }
}
