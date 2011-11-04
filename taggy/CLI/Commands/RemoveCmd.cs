using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace taggy
{
    public class RemoveCmd : CommandTemplate
    {
        public override string Name()
        {
            return "remove";
        }
        public override string Info()
        {
            return "Removes a directory from taggy";
        }

        public override string[] Aliases()
        {
            return new string[] {"-"};
        }

        public override string[] Synopsis()
        {
            return new string[]{ 
                "remove [] : Removes a directory from scanning"
            };
        }

        public override string Description()
        {
            return "remove will display a list of all stored directories with indexes. The user then selects which one to remove. "+
            "After you add a new directory, or remove an old one, you'll need to run the scan command to update taggy.";
        }

        public override void Command(string options, string parameter)
        {
            for(int i=0; i<Global.MusicDirectories.Count; i++)
                Console.WriteLine("{0}: {1}", i, Global.MusicDirectories[i]);
            Console.WriteLine("");
            Console.Write("Directory to remove? ");
            string input = Console.ReadLine();

            int result;
            if (int.TryParse(input, out result))
            {
                if (result >= 0 && result < Global.MusicDirectories.Count)
                {
                    Console.WriteLine("{0} was removed.", Global.MusicDirectories[result]);
                    Global.MusicDirectories.RemoveAt(result);
                    Global.Settings.updateItems("dir", Global.MusicDirectories);
                }
                else
                    Console.WriteLine("Invalid selection");
            }else 
                Console.WriteLine("Invalid selection");
        }
    }
}
