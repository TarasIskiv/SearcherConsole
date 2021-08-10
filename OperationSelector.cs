using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearcherConsole
{
    internal class OperationSelector
    {
        private string flag;
        private string pathToSource;
        private string searchedValue;
        public OperationSelector() { }

        private void setParamaters(string[] args)
        {
            flag = args[0];
            pathToSource = args[1];
            searchedValue = args[2];
        }
        public string selectOperation(string[] args)
        {
            setParamaters(args);

            if (flag.Equals(Flag.FILE_FLAG))
            {
                FileSelected file = new FileSelected(searchedValue);
                return file.getResult(pathToSource);
            }

            if (flag.Equals(Flag.DIRECTORY_FLAG))
            {
                FolderSelected folder = new FolderSelected(searchedValue);
                return folder.getResult(pathToSource);
            }

            if (flag.Equals(Flag.URL_PARSED_FLAG))
            {
                URLSelected url = new URLSelected(searchedValue, true);
                return url.getResult(pathToSource);
            }

            if(flag.Equals(Flag.URL_FLAG))
            {
                URLSelected url = new URLSelected(searchedValue, false);
                return url.getResult(pathToSource);
            }

            return Message.BAD_INPUT;
        }
    }
}
