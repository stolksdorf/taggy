using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace taggy
{
    public class EditCmd : CommandTemplate
    {
        public override string Name()
        {
            return "edit";
        }
        public override string Info()
        {
            return "Edits the tag info of a given song";
        }

        public override string[] Aliases()
        {
            return new string[] { "e" };
        }

        public override string[] Synopsis()
        {
            return new string[]{ 
                "info [] '' : Displys the tag info of the song of the current index", 
                "info [] 'index' : Displays the tag info at the given index"
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
            bool noOptions = options == "";
            string input;

            int index;
            if (Global.ValidateIndex(parameter, out index))
            {
                if (noOptions || options.Contains('t'))
                {
                    Console.Write("Title: ");
                    input = Console.ReadLine();
                    if (input != "")
                        Global.Queue[index].Title = input;
                    else
                    {
                        Console.CursorTop = Console.CursorTop - 1;
                        Console.CursorLeft = 7;
                        Console.WriteLine(Global.Queue[index].Title);
                    }
                }

                if (noOptions || options.Contains('a'))
                {
                    Console.Write("Artist: ");
                    input = Console.ReadLine();
                    if (input != "")
                        Global.Queue[index].Artist = input;
                    else
                    {
                        Console.CursorTop = Console.CursorTop - 1;
                        Console.CursorLeft = 8;
                        Console.WriteLine(Global.Queue[index].Artist);
                    }
                }

                if (noOptions || options.Contains('g'))
                {
                    Console.Write("Genre: ");
                    input = Console.ReadLine();
                    if (input != "")
                        Global.Queue[index].Genre = input;
                    else
                    {
                        Console.CursorTop = Console.CursorTop - 1;
                        Console.CursorLeft = 7;
                        Console.WriteLine(Global.Queue[index].Genre);
                    }
                }

                Console.WriteLine();
                Console.WriteLine("Song Updated!");
            }
        }
    }
}

