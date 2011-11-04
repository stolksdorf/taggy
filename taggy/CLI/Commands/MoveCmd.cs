using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace taggy
{
    public class MoveCmd : CommandTemplate
    {
        public override string Name()
        {
            return "move";
        }
        public override string Info()
        {
            return "Moves a song to one of the stored directories";
        }

        public override string[] Aliases()
        {
            return new string[] {"m"};
        }

        public override string[] Synopsis()
        {
            return new string[]{ 
                "move [] '' : Moves the sogn at the current index",
                "move [] 'index' : Moves the song at indeex in the queue"
            };
        }

        public override string Description()
        {
            return "add will add a directory to the list that taggy scnas. This list is saved in a settings file. " +
            "After you add a new directory, or remove an old one, you'll need to run the scan command to update taggy.";
        }

        public override void Command(string options, string parameter)
        {
           //Check the parameter 
            // print current path to song
            // list stored directories
            //prompt for index
            //check index
            //move song
            int index;
            if (Global.ValidateIndex(parameter, out index))
            {
                Song tempSong = Global.Queue[index];
                Console.WriteLine("Current path: {0}", tempSong.Path);
                for (int i = 0; i < Global.MusicDirectories.Count; i++)
                    Console.WriteLine("{0}: {1}", i, Global.MusicDirectories[i]);
                Console.WriteLine("");
                Console.Write("Directory to move to? ");
                string input = Console.ReadLine();

                int result;
                if (int.TryParse(input, out result))
                {
                    if (result >= 0 && result < Global.MusicDirectories.Count)
                    {
                        Console.WriteLine("{0} was moved to {1}", tempSong.FileName, Global.MusicDirectories[result]);
                        string newPath = Global.MusicDirectories[result] + "\\" + tempSong.FileName;

                        Global.MoveSong(index, newPath);
                        /*if (System.IO.File.Exists(newPath)) //If the renamed song already exists
                        {
                            System.IO.File.Delete(tempSong.Path);
                            Global.Songs.Remove(tempSong);
                            Global.Queue.Remove(tempSong);
                        }
                        else
                        {
                            System.IO.File.Move(tempSong.Path, newPath);
                            tempSong.Path = newPath;
                        } */
                    }
                    else
                        Console.WriteLine("Invalid selection");
                }
                else
                    Console.WriteLine("Invalid selection");
            }
        }
    }
}

