using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace taggy
{
    public abstract class CommandTemplate
    {
        public abstract string Name();
        public abstract string Info();
        public abstract string[] Synopsis();
        public abstract string Description();
        public abstract string[] Aliases();
        public abstract void Command(string options, string parameter);

        /*
        public CommandTemplate(string name, Action<string,string> cmd, string info)
        {
            Name = name;
            Info = info;
            Command = cmd;
        }

        public CommandTemplate(string name, Action<string,string> cmd, string info, string shortcut)
        {
            Name = name;
            Info = info;
            Command = cmd;
            Shortcut = shortcut;
        } */
    }
}
