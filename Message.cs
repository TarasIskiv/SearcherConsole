using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearcherConsole
{
    internal class Message
    {
        public Message() { }

        public static readonly string HELP_COMMAND = "Commands : \nhelp - show possible commands\n" +
                "-f <filePath> <word> or <\"sentence\">\n" +
                "-d <directoryPath> <word> or <\"sentence\">\n" +
                "-u <url> <word> or <\"sentence\">\n" +
                "-up <url> <word> or <\"sentence\">\n" +
                "Use -up to show parsed web page content\n" +
                "Do not use brackets \"<,>\"\n";

        public static readonly string BAD_INPUT = "Bad Input!\n" +
            "You need to enter :\n" +
            "flag (-f, -d, -u)\n" +
            "path\n" +
            "searched text\n" +
            "Enter \"help\" to see possible commands";
        
    }
}
