using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace taggy
{
    public class RenameCmd : CommandTemplate
    {
        public override string Name()
        {
            return "rename";
        }
        public override string Info()
        {
            return "Renames songs based on their tag information";
        }

        public override string[] Aliases()
        {
            return new string[] { "r" };
        }

        public override string[] Synopsis()
        {
            return new string[]{ 
                "rename [] '' : Renames the current song using the recipe", 
                "rename [-e] '' : Edits the recipe for renaming songs",
                "rename [-v] '' : Views the current reciepe for renaming songs",
                "rename [-a] '' : Renames all the songs in the queue using the recipe", 
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
            if(options.Contains("v")){
                Console.WriteLine(Global.Settings.getItem("rename"));
            }
            else if (options.Contains("e"))
            {
                Console.Write("New renaming recipe: ");
                string input = Console.ReadLine();
                if (input != "")
                {
                    Global.Settings.updateItem("rename", input);
                    Console.WriteLine("Renaming recipe updated to " + input);
                }
                else
                {
                    Console.CursorTop = Console.CursorTop - 1;
                    Console.CursorLeft = 21;
                    Console.WriteLine(Global.Settings.getItem("rename"));
                }
            }
            else
            {
                string recipe = Global.Settings.getItem("rename");
                if (recipe == "")
                    Console.WriteLine("Renaming recipe not set. Use 'rename -e' to set it");
                else
                {
                    List<Song> tempQueue = new List<Song>();
                    int total = 0;
                    if (options.Contains("a"))
                    {
                        tempQueue = Global.Queue;
                        Console.WriteLine("Renaming all songs in the queue");
                    }
                    else 
                    {
                        tempQueue.Add(Global.Queue[Global.CurrentIndex]);
                        Console.WriteLine("Renaming current song");
                    }
                    
                    for(int t1 = 0; t1<tempQueue.Count() ; t1++)
                    {
                        Song song = tempQueue[t1];
                        string newName = recipe .Replace("%artist%", song.Artist)
                                                .Replace("%title%", song.Title)
                                                .Replace("%genre%", song.Genre) + ".mp3";
                        if (newName != song.FileName && song.Title != "" && song.Artist != "")
                        {
                            string newPath = song.Path.Replace(song.FileName, newName);
                            Console.WriteLine("{0} -> {1}", song.FileName, newName);
                            Global.MoveSong(t1, newPath);
                            /*
                            if (System.IO.File.Exists(newPath)) //If the renamed song already exists
                            {
                                System.IO.File.Delete(song.Path);
                                Global.Songs.Remove(song);
                                Global.Queue.Remove(song);
                            }
                            else
                            {
                                System.IO.File.Move(song.Path, newPath);
                                song.Path = newPath;
                                song.FileName = newName;
                            } */
                            total++;
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine("{0} Songs renamed.", total);
                }
            }
        }
    }
}

