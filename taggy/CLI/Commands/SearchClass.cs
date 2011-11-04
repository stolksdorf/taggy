using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace taggy
{
    public class SearchCmd : CommandTemplate
    {
        public override string Name()
        {
            return "search";
        }
        public override string Info()
        {
            return "Searches for songs and adds them to the queue";
        }

        public override string[] Aliases()
        {
            return new string[] { "s" };
        }


        public override string[] Synopsis()
        {
            return new string[]{ 
                "search [] '' : Loads all songs into queue", 
                "search [] 'term' : Loads all songs that contain 'term' in title, artist, genre, or path", 
                "search [-q] '' : Quietly adds the songs to the queue",
                "search [-t] '' : Loads all songs that don't have a title", 
                "search [-a] '' : Loads all songs that don't have a artist", 
                "search [-g] '' : Loads all songs that don't have a genre", 
                "search [-d] '' : Runs the search on the songs in the queue instead of the library", 
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
            bool quiet = options.Contains('q');
            bool emptyGenre = options.Contains('g');
            bool emptyArtist = options.Contains('a');
            bool emptyTitle = options.Contains('t');
            bool displayList = options.Contains('d');

            bool noSearch = parameter == "";
            bool noOptions = (!emptyGenre && !emptyArtist && !emptyTitle);

            parameter = parameter.ToLower();
            string[] search_list = parameter.Split(' ');

            int total = 0;
            List<Song> tempList = new List<Song>();
            Global.Queue.Clear();

            if (displayList) tempList = Global.Queue;
            else tempList = Global.Songs;

            for (int i = 0; i < tempList.Count; i++)
            {
                bool searchCheck = true;
                bool optionsCheck = false;
                foreach (string term in search_list)
                {
                    bool termSearchCheck = false;
                    if (tempList[i].Artist == null)
                    {
                        if (emptyArtist) optionsCheck = true;
                    }
                    else if (tempList[i].Artist.ToLower().Contains(term) || term == "")
                        termSearchCheck = true;

                    if (tempList[i].Title == null)
                    {
                        if (emptyTitle) optionsCheck = true;
                    }
                    else if (tempList[i].Title.ToLower().Contains(term) || term == "")
                        termSearchCheck = true;

                    if (tempList[i].Genre == null)
                    {
                        if (emptyGenre) optionsCheck = true;
                    }
                    else if (tempList[i].Genre.ToLower().Contains(term) || term == "")
                        termSearchCheck = true;

                    if (tempList[i].FileName.ToLower().Contains(term) || term == "")
                        termSearchCheck = true;

                    searchCheck = searchCheck && termSearchCheck;
                }

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
    }
}

