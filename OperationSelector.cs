using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearcherConsole
{
    internal class OperationSelector
    {
        private string _flag;
        private string _pathToSource;
        private string _searchedValue;
        public OperationSelector(string[] args)
        {
            _flag = args[0];
            _pathToSource = args[1];
            _searchedValue = args[2];
        }

        public string selectOperation()
        {

            if (_flag.Equals(Flag.FILE_FLAG))
            {
                 return new FileSelected(_searchedValue, _pathToSource).search();
            }

            if (_flag.Equals(Flag.DIRECTORY_FLAG))
            {
                return new FolderSelected(_searchedValue, _pathToSource).search();
            }

            if (_flag.Equals(Flag.URL_PARSED_FLAG))
            {
                return new URLSelected(_searchedValue, _pathToSource, true).search();
            }

            if(_flag.Equals(Flag.URL_FLAG))
            {
                return new URLSelected(_searchedValue, _pathToSource, false).search();
            }

            return Message.BAD_INPUT;
        }
    }
}
