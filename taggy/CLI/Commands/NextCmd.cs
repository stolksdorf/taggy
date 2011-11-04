using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace taggy
{
    public class NextCmd : CommandTemplate
    {
        public override string Name()
        {
            return "next";
        }
        public override string Info()
        {
            return "Increments the current index and displays the tag info of it";
        }

        public override string[] Aliases()
        {
            return new string[] { "n" };
        }

        public override string[] Synopsis()
        {
            return new string[]{ 
                "next [] '' : Increments the current index and displays the tag info", 
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
            int temp = Global.CurrentIndex + 1;
            InfoCmd tempCmd = new InfoCmd();
            tempCmd.Command("", temp.ToString());
        }
    }
}