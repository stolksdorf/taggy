using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace taggy
{
    public class SettingsClass
    {
        private string Path;
        public SettingsClass(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("Could not find settings file. Creating a new one.");
                //File.CreateText(path);
            }
            Path = path;
        }

        public bool keyExists(string key)
        {
            if (System.IO.File.Exists(Path))
            {
                string[] lines = System.IO.File.ReadAllLines(Path);
                foreach (string line in lines)
                {
                    if (line.StartsWith(key))
                        return true;
                }
                return false;
            }
            return false;
        }
        public string getItem(string key)
        {
            if (System.IO.File.Exists(Path))
            {
                string[] lines = System.IO.File.ReadAllLines(Path);

                foreach (string line in lines)
                {
                    if (line.StartsWith(key))
                        return line.Replace(key + ":", "");
                }
                return "";
            }
            return "";
        }
        //Finds the item with the key and updates its value
        public void updateItem(string key, string value)
        {
            if (System.IO.File.Exists(Path))
            {
                string[] lines = System.IO.File.ReadAllLines(Path);
                System.IO.StreamWriter file = new System.IO.StreamWriter(Path);
                bool updated = false;

                for (int t1 = 0; t1 < lines.Length; t1++)
                {
                    if (lines[t1].StartsWith(key))
                    {
                        file.WriteLine(key + ":" + value);
                        updated = true;
                    }
                    else
                        file.WriteLine(lines[t1]);
                }
                //If we couldn't find the key, it adds it to the file
                if (!updated)
                    file.WriteLine(key + ":" + value);
                file.Close();
            }
        }
        //Deletes all items with key, and adds all of the new values and keys to the end
        public void updateItems(string key, List<string> values)
        {
            if (System.IO.File.Exists(Path))
            {
                string[] lines = System.IO.File.ReadAllLines(Path);
                System.IO.StreamWriter file = new System.IO.StreamWriter(Path);
                bool updated = false;

                for (int t1 = 0; t1 < lines.Length; t1++)
                {
                    if (lines[t1].StartsWith(key))
                    {
                        file.WriteLine(key + ":" + String.Join(",",values));
                        updated = true;
                    }
                    else
                        file.WriteLine(lines[t1]);
                }
                //If we couldn't find the key, it adds it to the file
                if (!updated)
                    file.WriteLine(key + ":" + String.Join(",", values));
                file.Close();
            }

        }




    }
}
