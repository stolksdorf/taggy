using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace taggy
{
    public class HelpCmd : CommandTemplate
    {
        public override string Name()
        {
            return "help";
        }
        public override string Info()
        {
            return "Displays all available commands or a command's manpage";
        }
        
        public override string[] Aliases()
        {
            return new string[]{ "man","h", "?" };
        }

        public override string[] Synopsis(){
            return new string[]{ 
                "help [] '' : Display's a list of all commands and a brief description", 
                "help [] 'command' : Display's commands manpage."
            };
        }

        public override string Description()
        {
            return "help without any parameters will display each command available, it's alaises and a brief description. " +
            "When given a parameter of a command name it will display its manpage which will cover each of it's aliases, " +
            "and a more in-depth description.";
        }

        public override void Command(string options, string parameter)
        {
            if (parameter == "")
            {
                Console.WriteLine("List of Commands:");
                foreach (CommandTemplate cmd in Global.Commands)
                {
                    Console.Write("  ");
                    Console.WriteLine("{0} ({1}) : {2}", cmd.Name(), String.Join(",", cmd.Aliases()), cmd.Info());
                }
            }
            else
            {
                bool found = false;
                foreach (CommandTemplate cmd in Global.Commands)
                {
                    if (cmd.Name() == parameter || cmd.Aliases().Contains(parameter))
                    {
                        Console.WriteLine("NAME");
                        Console.WriteLine("    {0} - {1}", cmd.Name(), cmd.Info());
                        Console.WriteLine("\nSYNOPSIS");
                        foreach (string syn in cmd.Synopsis())
                            Console.WriteLine("    {0}", syn);
                        Console.WriteLine("\nDESCRIPTION");
                        Console.WriteLine("{0}", cmd.Description());
                        found = true;
                    }
                }
                if (!found)
                    Console.WriteLine("Command not recognized");
            }
        }
    }
}
