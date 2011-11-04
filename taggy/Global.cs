using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace taggy
{
    public static class Global
    {
        //public static string[] MusicDirectories = { @"C:\Dropbox\root\Music", @"E:\Shared\Music Storage" };
        
        public static List<string> MusicDirectories = new List<string>();
        public static List<Song> Queue = new List<Song>();
        public static List<Song> Songs = new List<Song>();
        public static List<CommandTemplate> Commands = new List<CommandTemplate>();

        public static Song CurrentSong; //possibly remove
        public static int CurrentIndex;


        public static SettingsClass Settings;


        public static bool ValidateIndex(string input, out int result)
        {
            int temp;
            if(Queue.Count == 0){
                Console.WriteLine("No songs in queue");
                result = 0;
                return false;
            }
            else if(input ==""){
                result = CurrentIndex;
                return true;
            }
            else if (int.TryParse(input, out temp))
            {
                result = temp;
                if (temp < 0 || temp >= Queue.Count) 
                    result = 0;
                return true;
            }
            else
                Console.WriteLine("Parameter not recognized: " + input);
            result = 0;
            return false;
        }

        public static void MoveSong(int indexInQueue, string newPath)
        {
           // string newPath = Global.MusicDirectories[result] + "\\" + tempSong.FileName;

            //check to see if the file already exists 
            //if so, find the song object by searching for the new path
            //delete the old item from the library, and over write the song object in the queue

            //otherwise, just move the song and update the path

            if (System.IO.File.Exists(newPath)) //If the renamed song already exists
            {
                foreach (Song song in Songs)
                {
                    if (song.Path == newPath)
                    {
                        Songs.Remove(Queue[indexInQueue]);
                        System.IO.File.Delete(Queue[indexInQueue].Path);
                        Queue[indexInQueue] = song;
                        break;
                    }
                }
            }
            else
            {
                System.IO.File.Move(Queue[indexInQueue].Path, newPath);
                Queue[indexInQueue].Path = newPath;
            }
        }



        //For the next command
        

        
        /*

        public static List<Song> Search(string query, bool quiet)
        {
            List<Song> result = new List<Song>();



            return result;
        }

        //Searches for songs with missing tags and returns them
        public static List<Song> Search(bool noTitle, bool noArtist, bool noGenre, bool quiet)
        {
            List<Song> result = new List<Song>();



            return result;
        }



        public static CommandTemplate[] Cmds = {
            new CommandTemplate("exit", null, "Exits taggy","exit"),
            new CommandTemplate("help", Commands.help, "Prints out a list of available commands", "h"),
            new CommandTemplate("clear", Commands.clear, "Clears the screen", "cls"),
            //new CommandTemplate("ls", Commands.ls, "-[qtags] Returns a list of songs."),
            new CommandTemplate("next", Commands.next, "Jumps to the next song in the list", "n"),
            new CommandTemplate("play", Commands.play, "Plays the song", "p"),
            new CommandTemplate("info", Commands.info, "Display the info about a given song", "i"),
            new CommandTemplate("search", Commands.search, "Searches for songs", "s"),
            new CommandTemplate("edit", Commands.edit, "Edits tags on a song", "e"),
            new CommandTemplate("rename", Commands.rename, "Renames the file based on the mp3tags", "r"),
            new CommandTemplate("delete", Commands.delete, "Deletes the song", "d")

            //move
         * //add
         * /remove
            //clone
            //man pages
            //




        };
        */
        


    }
}
