using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace taggy
{
    public class PlayCmd : CommandTemplate
    {
        public override string Name()
        {
            return "play";
        }
        public override string Info()
        {
            return "Plays the given song";
        }

        public override string[] Aliases()
        {
            return new string[] { "p" };
        }

        public override string[] Synopsis()
        {
            return new string[]{ 
                "play [] '' : Plays the song of the current index", 
                "play [] 'index' : Plays the song at the given index"
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
                Console.WriteLine("Playing {0}.", Global.Queue[index].FileName);
                Global.Queue[index].Play();
            }
        }
    }
}
