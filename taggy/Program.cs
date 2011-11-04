using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TagLib;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;

namespace taggy
{
    class Program
    {
        static void Main(string[] args)
        {
            //Add all the commands
            Global.Commands.Add(new HelpCmd());
            Global.Commands.Add(new ClearCmd());
            Global.Commands.Add(new AddCmd());
            Global.Commands.Add(new RemoveCmd());
            Global.Commands.Add(new SearchCmd());
            Global.Commands.Add(new ScanCmd());
            Global.Commands.Add(new InfoCmd());
            Global.Commands.Add(new NextCmd());
            Global.Commands.Add(new DeleteCmd());
            Global.Commands.Add(new EditCmd());
            Global.Commands.Add(new MoveCmd());
            Global.Commands.Add(new RenameCmd());
            Global.Commands.Add(new PlayCmd());

            //load settings
            Global.Settings = new SettingsClass("settings.txt");

            string dirs = Global.Settings.getItem("dir");
            if (dirs == "")
            {
                Console.WriteLine("No directories loaded. You should add some using the 'add' cmd.");
                Console.WriteLine("");
            }
            else
            {
                Global.MusicDirectories.AddRange(dirs.Split(','));
                ScanCmd tempScan = new ScanCmd();
                tempScan.Command("", "");
            }

            CLI taggyCLI = new CLI("taggy", Global.Commands);
            taggyCLI.RunConsole();
        }

        //Loads up all of your music into the library
        public static void LoadLibraries()
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
