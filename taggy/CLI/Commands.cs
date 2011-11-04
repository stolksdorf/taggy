using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace taggy
{
    public static class Commands
    {
        public static int CurrentIndex = 0;
        public static Action<string,string> LastCommand;

/*
        public static void help(string options, string parameter)
        {
            Console.WriteLine("List of Commands:");
            foreach (CommandTemplate cmd in Global.Commands)
            {
                Console.Write("  ");
                Console.WriteLine("{0} ({1}) : {2}",cmd.Name, cmd.Shortcut , cmd.Info);
            }
        }

        public static void search(string options, string parameter)
        {
            bool quiet = options.Contains('q');
            bool emptyGenre = options.Contains('g');
            bool emptyArtist = options.Contains('a');
            bool emptyTitle = options.Contains('t') ;
            bool displayList = options.Contains('d');

            bool noSearch = parameter == "";
            bool noOptions = (!emptyGenre && !emptyArtist && !emptyTitle);

            parameter = parameter.ToLower();

            int total = 0;
            List<Song> tempList = new List<Song>();
            Global.Queue.Clear();

            if (displayList) tempList = Global.Queue;
            else tempList = Global.Songs;

            for (int i = 0; i < tempList.Count; i++)
            {
                bool searchCheck = false;
                bool optionsCheck = false;
               
                if (tempList[i].Artist == null)
                {
                    if (emptyArtist) optionsCheck = true;
                }
                else if (tempList[i].Artist.ToLower().Contains(parameter) || parameter == "")
                    searchCheck = true;

                if (tempList[i].Title == null)
                {
                    if (emptyTitle) optionsCheck = true;
                }
                else if (tempList[i].Title.ToLower().Contains(parameter) || parameter == "")
                    searchCheck = true;

                if (tempList[i].Genre == null)
                {
                    if (emptyGenre) optionsCheck = true;
                }
                else if (tempList[i].Genre.ToLower().Contains(parameter) || parameter == "")
                    searchCheck = true;

                if (tempList[i].FileName.ToLower().Contains(parameter) || parameter == "")
                    searchCheck = true;


                if ((noSearch || searchCheck) && (noOptions || optionsCheck))
                {
                    if (!quiet) Console.WriteLine("{0}: {1}", total, tempList[i].FileName);
                    Global.Queue.Add(tempList[i]);
                    total++;
                }

            }
            Console.WriteLine();
            Console.WriteLine("{0} songs in queue.", total);
        }

        public static void info(string options, string parameter)
        {
            int result;
            if (int.TryParse(parameter, out result) || parameter == "")
            {
                if(parameter == "") result = CurrentIndex;

                if (result < Global.Queue.Count && result >= 0)
                {
                    CurrentIndex = result;
                    LastCommand = Commands.info;

                    Song temp = Global.Queue[result];
                    Console.WriteLine("Title: " + temp.Title);
                    Console.WriteLine("Artist: " + temp.Artist);
                    Console.WriteLine("Genre: " + temp.Genre);
                    Console.WriteLine("Path: " + temp.Path);
                }else
                    Console.WriteLine("Could not parse parameter: " + parameter);
            }
            else
                Console.WriteLine("Could not parse parameter: " + parameter);

        }

        public static void clear(string options, string parameter)
        {
            Console.Clear();
        }

        public static void play(string options, string parameter)
        {
            int result;
            if (int.TryParse(parameter, out result) || parameter == "")
            {
                if (parameter == "") result = CurrentIndex;
                CurrentIndex = result;
                //LastCommand = Commands.play;
                Console.WriteLine("Playing {0}.", Global.Queue[result].FileName);
                Global.Queue[result].Play();
            }
            else
                Console.WriteLine("Could not parse parameter: " + parameter);
        }

        public static void delete(string options, string parameter)
        {
            int result;
            if (int.TryParse(parameter, out result) || parameter == "")
            {
                if (parameter == "") result = CurrentIndex;
                CurrentIndex = result;

                Console.Write("Are you sure you want to delete " + Global.Queue[result].Path + " (y/n)? ");
                string ans = Console.ReadLine();
                if (ans.ToLower() == "y" || ans.ToLower() == "yes")
                {
                    Global.Songs.Remove(Global.Queue[result]);
                    Global.Queue.RemoveAt(result);
                    File.Delete(Global.Queue[result].Path);
                    Console.WriteLine("Deleted.");
                }
            }
            else
                Console.WriteLine("Could not parse parameter: " + parameter);
        }


        public static void edit(string options, string parameter)
        {
            bool noOptions = options == "";
            int index;
            string input;

            int result;
            if (int.TryParse(parameter, out result))
                CurrentIndex = result;
            index = CurrentIndex;

            if (noOptions || options.Contains('t'))
            {
                Console.Write("Title: ");
                input = Console.ReadLine();
                if (input != "")
                    Global.Queue[index].Title = input;
                else
                {
                    Console.CursorTop = Console.CursorTop - 1;
                    Console.CursorLeft = 7;
                    Console.WriteLine(Global.Queue[index].Title);
                }
            }

            if (noOptions || options.Contains('a'))
            {
                Console.Write("Artist: ");
                input = Console.ReadLine();
                if (input != "")
                    Global.Queue[index].Artist = input;
                else
                {
                    Console.CursorTop = Console.CursorTop - 1;
                    Console.CursorLeft = 8;
                    Console.WriteLine(Global.Queue[index].Artist);
                }
            }

            if (noOptions || options.Contains('g'))
            {
                Console.Write("Genre: ");
                input = Console.ReadLine();
                if (input != "")
                    Global.Queue[index].Genre = input;
                else
                {
                    Console.CursorTop = Console.CursorTop - 1;
                    Console.CursorLeft = 7;
                    Console.WriteLine(Global.Queue[index].Genre);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Song Updated!");
        }

        public static void next(string options, string parameter)
        {
            CurrentIndex++;
            if (CurrentIndex >= Global.Queue.Count) CurrentIndex = 0;
            if (CurrentIndex < 0) CurrentIndex = 0;
            //LastCommand("",CurrentIndex.ToString());
            info("", CurrentIndex.ToString());
        }



        public static void rename(string options, string parameter)
        {
            //if(options.Contains("a"))
            int total = 0;

            Console.WriteLine("Renaming all songs in the queue");

            foreach (Song song in Global.Queue)
            {
                string newName = song.Artist + " - " + song.Title + ".mp3";
                if (newName != song.FileName && song.Title != "" && song.Artist != "")
                {
                    string newPath = song.Path.Replace(song.FileName, newName);
                    Console.WriteLine("{0} -> {1}", song.FileName, newName);
                    System.IO.File.Move(song.Path, newPath);
                    song.Path = newPath;
                    song.FileName = newName;
                    total++;
                }
            }
            Console.WriteLine();
            Console.WriteLine("{0} Songs renamed.", total);
        }

        /*
        public static void search(string options, string parameter)
        {
            bool quiet = false;

            Global.Queue.Clear();
            int total = 0;
            if (parameter != "")
            {
                foreach (Song song in Global.Songs)
                {
                    bool check = false;

                    if (song.Artist != null)
                        if (song.Artist.ToLower().Contains(parameter))
                            check = true;
                    if (song.Genre != null)
                        if (song.Genre.ToLower().Contains(parameter))
                            check = true;
                    if (song.Title != null)
                        if (song.Title.ToLower().Contains(parameter))
                            check = true;
                    if (song.Path != null)
                        if (song.Path.ToLower().Contains(parameter))
                            check = true;

                    if (check)
                    {
                        Global.Queue.Add(song);
                        if (!quiet) Console.WriteLine("{0}: {1}", total, song.FileName);
                        total++;
                    }
                }
                Console.WriteLine();
                Console.WriteLine("{0} songs added to queue.", total);
            }
        }

        /*
        public static void temp(string options, string parameter)
        {

        } 
         * 
         * */



    }
}
