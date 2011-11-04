using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace taggy
{
    public class InfoCmd : CommandTemplate
    {
        public override string Name()
        {
            return "info";
        }
        public override string Info()
        {
            return "Displays the tag information about a given song";
        }

        public override string[] Aliases()
        {
            return new string[] { "i" };
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
            int index;
            if (Global.ValidateIndex(parameter, out index))
            {
                Global.CurrentIndex = index;
                Song temp = Global.Queue[index];
                Console.WriteLine("Title: " + temp.Title);
                Console.WriteLine("Artist: " + temp.Artist);
                Console.WriteLine("Genre: " + temp.Genre);
                Console.WriteLine("Path: " + temp.Path);
                Console.WriteLine("Index: " + index);
            }
        }
    }
}
