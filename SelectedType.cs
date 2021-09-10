using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearcherConsole
{
    internal abstract class SelectedType
    {
        protected string searchedValue;

        protected string pathToSource;
        public abstract string search();
    }
}
