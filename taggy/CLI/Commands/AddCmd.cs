using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace taggy
{
    public class AddCmd : CommandTemplate
    {
        public override string Name()
        {
            return "add";
        }
        public override string Info()
        {
            return "Add's a directory for taggy to scan for music";
        }

        public override string[] Aliases()
        {
            return new string[] { "a", "+" };
        }

        public override string[] Synopsis()
        {
            return new string[]{ 
                "add [] 'path' : Add's path to the directories taggy scans"
            };
        }

        public override string Description()
        {
            return "add will add a directory to the list that taggy scnas. This list is saved in a settings file. " +
            "After you add a new directory, or remove an old one, you'll need to run the scan command to update taggy.";
        }

        public override void Command(string options, string parameter)
        {
            DirectoryInfo info = new DirectoryInfo(parameter);

            if(info.Exists){
                Global.MusicDirectories.Add(parameter);
                Global.Settings.updateItems("dir", Global.MusicDirectories);
                Console.WriteLine("{0} was added!", parameter);
            }else
                Console.WriteLine("Invalid directory");
        }
    }
}
