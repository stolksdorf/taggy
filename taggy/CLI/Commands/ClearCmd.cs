using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace taggy
{
    public class ClearCmd : CommandTemplate
    {
        public override string Name()
        {
            return "clear";
        }
        public override string Info()
        {
            return "Clears the screen";
        }

        public override string[] Aliases()
        {
            return new string[] { "cls","c" };
        }

        public override string[] Synopsis()
        {
            return new string[]{ 
                "clear [] '' : Clears the screen"
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
            Console.Clear();
        }
    }
}
