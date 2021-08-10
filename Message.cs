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
        
        public static readonly string FOLDER_WITHOUT_TXT_FILES = "Folder don't exist .txt or .docx files";

        public static readonly string NO_MATCHES = "No matches found";

        public static readonly string FILE_NOT_FOUND = "File not found";

    }
}
