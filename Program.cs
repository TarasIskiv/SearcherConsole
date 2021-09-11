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
                Console.WriteLine(new OperationSelector(args).selectOperation());
            }
            else
            {
                Console.WriteLine(Message.BAD_INPUT);
            }

        }
    }
}
