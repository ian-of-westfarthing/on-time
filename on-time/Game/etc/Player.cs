using System;
using System.IO;

namespace ontime.Game.etc
{
    /// <summary>
    /// Player class used to represent the player in adventure mode.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// The players name.
        /// </summary>
        public string Name;
        /// <summary>
        /// The x position of the player.
        /// </summary>
        public int X;
        /// <summary>
        /// The y position of the player.
        /// </summary>
        public int Y;
        /// <summary>
        /// The z position of the player.
        /// </summary>
        public int Z;

        /// <summary>
        /// Save the player data to a file (region/Data/player)
        /// </summary>
        /// <param name="fileName">Where to write this data...</param>
        public void Save(string fileName)
        {

            // Write all the data to the specified file.
            File.WriteAllLines(fileName,
                new string[]
                {
                    Name,
                    Z.ToString(),
                    X.ToString(),
                    Y.ToString()
                });
        }

        /// <summary>
        /// Load player data from the file specified (region/Data/player)
        /// </summary>
        /// <param name="fileName">Where to read this data from</param>
        public void Load(string fileName)
        {
            // Read all the player data.
            string[] file = File.ReadAllLines(fileName);

            Name = file[0];
            Z = int.Parse(file[1]);
            X = int.Parse(file[2]);
            Y = int.Parse(file[3]);
        }
    }
}
