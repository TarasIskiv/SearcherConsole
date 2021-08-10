using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearcherConsole
{
    internal class OperationSelector
    {
        public OperationSelector() { }

        public static string operationSelector(string[] args)
        {

            string flag = args[0];
            string pathToSource = args[1];
            string searchedValue = args[2];
            if (flag.Equals(Flag.FILE_FLAG))
            {
                FileSelected file = new FileSelected(searchedValue);
                return file.fileSelected(pathToSource);
            }
            else if (flag.Equals(Flag.DIRECTORY_FLAG))
            {
                FolderSelected folder = new FolderSelected(searchedValue);
                return folder.directorySelected(pathToSource);
            }
            else if (flag.Equals(Flag.URL_FLAG) || flag.Equals(Flag.URL_PARSED_FLAG))
            {
                if (flag.Equals(Flag.URL_PARSED_FLAG))
                {
                    URLSelected url = new URLSelected(searchedValue, true);
                    return url.urlSelected(pathToSource);
                }
                else
                {
                    URLSelected url = new URLSelected(searchedValue, false);
                    return url.urlSelected(pathToSource);
                }

            }
            return Message.BAD_INPUT;
        }
    }
}
