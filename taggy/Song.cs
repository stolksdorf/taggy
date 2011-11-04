using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace taggy
{
    public class Song
    {
        public string Path;
        public string FileName;
        private TagLib.File tagFile;

        public Song(TagLib.File file, string path)
        {
            tagFile = file;
            Path = path;
            int temp = path.LastIndexOf('\\');
            FileName = path.Substring(temp+1, path.Length - temp - 1);
        }

        #region Accessors
        public string Title
        {
            get
            {
                return tagFile.Tag.Title;
            }
            set
            {
                tagFile.Tag.Title = value;
                tagFile.Save();
            }
        }

        public string Artist
        {
            get
            {
                return tagFile.Tag.FirstArtist;
            }
            set
            {
                string[] tempArtist = { value };
                tagFile.Tag.Artists = tempArtist;
                tagFile.Save();
            }
        }

        public string Album
        {
            get
            {
                return tagFile.Tag.Album;
            }
            set
            {
                string tempAlbum = value ;
                tagFile.Tag.Album = tempAlbum;
                tagFile.Save();
            }
        }

        public string Genre
        {
            get
            {
                return tagFile.Tag.FirstGenre;
            }
            set
            {
                string[] tempGenre = { value };
                tagFile.Tag.Genres = tempGenre;
                tagFile.Save();
            }
        }



        #endregion

        
        public void Play()
        {
            Process.Start(Path);
        }

        public void Delete()
        {

        }

    }
}
