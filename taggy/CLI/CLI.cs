using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace taggy
{
    public class CLI
    {
        private string consoleName;
        private List<CommandTemplate> Commands = new List<CommandTemplate>();

        public CLI(string console_name, List<CommandTemplate> commands)
        {
            consoleName = console_name;
            Commands = commands;
        }

        public void RunConsole()
        {
            string input = "";
            bool exitShell = false;
            while (!exitShell)
            {
                Console.Write("${0}> ", consoleName);

                input = Console.ReadLine();
                string command = ExtractCommand(input);
                string parameter = ExtractParameter(input);
                string options = ExtractOptions(input);

                bool cmdValid = false;
                foreach (CommandTemplate cmd in Commands)
                {
                    if (command == "exit" || command == "quit") exitShell = true;
                    else if (cmd.Name() == command || cmd.Aliases().Contains(command))
                    {
                        cmdValid = true;
                        cmd.Command(options, parameter);
                        break;
                    }
                }

                //Unrecognized Command
                if (!cmdValid && !exitShell)
                    Console.WriteLine("Command not recognized: {0}", command);

                Console.WriteLine("");
            }
        }


        private string ExtractCommand(string input)
        {
            return input.TrimStart(' ').Split(' ')[0]; 
        }

        private string ExtractOptions(string input)
        {
            string result ="";
            foreach (string part in input.TrimStart(' ').Split(' '))
            {
                if(part.StartsWith("-"))
                    result = part;
            }
            return result;
        }

        private string ExtractParameter(string input)
        {
            string[] parts = input.TrimStart(' ').Split(' ');
            string removed = parts[0];
            if (parts.Length > 1)
            {
                if (parts[1].StartsWith("-"))
                    removed += " " + parts[1];
                if(removed.Length < input.Length)
                    return input.Substring(removed.Length + 1);
            }
            return "";
        }

    }
}
