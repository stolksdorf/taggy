using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace taggy
{
    public class DeleteCmd : CommandTemplate
    {
        public override string Name()
        {
            return "delete";
        }
        public override string Info()
        {
            return "Deletes the given song";
        }

        public override string[] Aliases()
        {
            return new string[] { "del","d" };
        }

        public override string[] Synopsis()
        {
            return new string[]{ 
                "delete [] '' : Deletes the song of the current index", 
                "delete [] 'index' : Deletes the song at the given index"
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
                Console.Write("Are you sure you want to delete " + Global.Queue[index].Path + " (y/n)? ");
                string ans = Console.ReadLine();
                if (ans.ToLower() == "y" || ans.ToLower() == "yes")
                {
                    Global.Songs.Remove(Global.Queue[index]);
                    Global.Queue.RemoveAt(index);
                    File.Delete(Global.Queue[index].Path);
                    Console.WriteLine("Deleted.");
                }
            }
        }
    }
}