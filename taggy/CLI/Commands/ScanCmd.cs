using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace taggy
{
    public class ScanCmd : CommandTemplate
    {
        public override string Name()
        {
            return "scan";
        }
        public override string Info()
        {
            return "Scans the stored directories for music";
        }

        public override string[] Aliases()
        {
            return new string[] {};
        }

        public override string[] Synopsis()
        {
            return new string[]{ 
                "scan [] '' : Scans the stored directories"
            };
        }

        public override string Description()
        {
            return "add will add a directory to the list that taggy scnas. This list is saved in a settings file. " +
            "After you add a new directory, or remove an old one, you'll need to run the scan command to update taggy.";
        }

        public override void Command(string options, string parameter)
        {
            int emptyGenre = 0;
            int emptyTitle = 0;
            int emptyArtist = 0;
            int totalSongs = 0;

            Global.Songs.Clear();
            Console.WriteLine("Searching Music Directorires...");
            foreach (string dir in Global.MusicDirectories)
            {
                string[] filePaths = Directory.GetFiles(dir);
                Console.Write("Scanning {0}... ", dir);
                int count = 0;
                foreach (string fileName in filePaths)
                {
                    if (fileName.EndsWith(".mp3"))
                    {
                        count++;
                        TagLib.File f = TagLib.File.Create(fileName);
                        Global.Songs.Add(new Song(f, fileName));

                        if (f.Tag.FirstGenre == null) emptyGenre++;
                        if (f.Tag.Title == null) emptyTitle++;
                        if (f.Tag.FirstArtist == null) emptyArtist++;
                    }
                }
                Console.Write("{0} songs found.\n", count);
                totalSongs += count;
            }
            Console.WriteLine("{0} Songs found.", totalSongs);
            Console.WriteLine();
            Console.WriteLine("{0} songs found with no title", emptyTitle);
            Console.WriteLine("{0} songs found with no artist", emptyArtist);
            Console.WriteLine("{0} songs found with no genre", emptyGenre);
            Console.WriteLine();
        }
    }
}
