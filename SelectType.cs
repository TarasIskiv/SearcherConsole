using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearcherConsole
{
    internal abstract class SelectType
    {
        protected string searchedValue;

        public abstract string getResult(string pathToSource);
    }
}
