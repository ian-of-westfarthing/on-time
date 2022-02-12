using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

using ontime.Game;

namespace ontime.Game.Region
{
    // Just info for a save
    public class RegionData
    {
        public int Region;      // Save/region1
        public string Name;

        public int Width = 100;  // Width in chunks
        public int Height = 100; // Height in chunks

        // The map
        public bool Started;
        public bool Started_Map;
        public bool FortressMode = false;

        public Site[,] Map;

        // Loading and saving of region data
        public void Loadf(string fileName)
        {
            string[] data = File.ReadAllLines(fileName);

            Region = int.Parse(data[0]);
            Name = data[1];
            Width = int.Parse(data[2]);
            Height = int.Parse(data[3]);
            Started = data[4] == "true";
            Started_Map = data[5] == "true";
        }
        public void Savef(string fileName)
        {
            string[] data = new string[6];

            data[0] = Region.ToString();
            data[1] = Name;
            data[2] = Width.ToString();
            data[3] = Height.ToString();
            data[4] = Started ? "true" : "false";
            data[5] = Started_Map ? "true" : "false";

            File.WriteAllLines(fileName, data);
        }

        // Loading and saving of maps, WITH compression
        public void LoadMapf(string fileName)
        {
            byte[] data = File.ReadAllBytes(fileName);

            int loc = 0;

            if (Map == null)
                Map = new Site[Width, Height];

            // Types
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (Map[x, y] == null)
                        Map[x, y] = new Site();

                    Map[x, y].Type = data[loc];
                    loc++;
                }
            }

            // Biomes
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Map[x, y].Biome = data[loc];
                    loc++;
                }
            }
        }
        public void SaveMapf(string fileName)
        {
            List<byte> data = new List<byte>();

            // Types
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    data.Add(Map[x, y].Type);
                }
            }

            // Biomes
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    data.Add(Map[x, y].Biome);
                }
            }

            File.WriteAllBytes(fileName, data.ToArray());
            Thread.Sleep(100);
        }


        // Very simple region generation
        public void Generate()
        {
            // VERY basic map generation
            Map = new Site[Width, Height];

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Map[x, y] = new Site();
                    Map[x, y].New();
                }
            }
        }

        // in-built menus
        public void ViewMap()
        {
            // Map viewer!
            bool Run = true;

            int CX = 0;
            int CY = 0;

            // while Loop / View/Embark the map
            while (Run)
            {
                RenderMapView(CX, CY);

                switch (MainClass.ReadKey().Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (CX > 0)
                            CX--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (CX < Width - 1)
                            CX++;
                        break;
                    case ConsoleKey.UpArrow:
                        if (CY > 0)
                            CY--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (CY < Height - 1)
                            CY++;
                        break;
                    case ConsoleKey.Escape:
                        Run = false;
                        break;
                }
            }
        }


        // Render the map view, used in multiple areas
        public void RenderMapView(int CX, int CY, bool select = false)
        {
            // Clear the screen
            Graphics.ClearScreen();

            int RX = 0;
            int RY = 0;



            // Render the map part
            for (int x = CX - 39; x < Width; x++)
            {
                for (int y = CY - 9; y < Height; y++)
                {

                    // GRAPHICS
                    if (x == CX && y == CY)
                    {
                        Graphics.WriteAt('x', RX, RY);
                    }
                    else if (x >= 0 && x < Width && y >= 0 && y < Height)
                    {
                        Graphics.WriteAt(Game.Shared.SiteMapDatas[Map[x, y].Type].tex, RX, RY, Game.Shared.SiteMapDatas[Map[x, y].Type].col);
                    }

                    // bounds checking
                    if (RY >= 18)
                        break;
                    RY++;
                }
                RY = 0;

                if (RX >= 78)
                    break;
                RX++;
            }


            // Info about this site
            Graphics.WriteLine("[ " + Map[CX, CY].Name + " - a " + Game.Shared.SiteBiomeDatas[Map[CX, CY].Biome].Name + " " + Game.Shared.SiteMapDatas[Map[CX, CY].Type].type, 1, 19);
            Graphics.WriteLine("]", 78, 19);
            Graphics.WriteLine("[+--------------------------------------------------------------------------+]", 1, 20);




            // Selecting a site to embark/Just viewing the world
            if (select)
            {
                Graphics.WriteLine("escape - select site/embark " + Name + ".", 1, 21, new ConsoleColor[] { ConsoleColor.Red, ConsoleColor.Red, ConsoleColor.Red, ConsoleColor.Red, ConsoleColor.Red, ConsoleColor.Red });
            }
            else
            {
                Graphics.WriteLine("escape - finish viewing " + Name + ".", 1, 21, new ConsoleColor[] { ConsoleColor.Red, ConsoleColor.Red, ConsoleColor.Red, ConsoleColor.Red, ConsoleColor.Red, ConsoleColor.Red });
            }
            // Other controls
            Graphics.WriteLine("arrows - move around map.", 1, 22, new ConsoleColor[] { ConsoleColor.Green, ConsoleColor.Green, ConsoleColor.Green, ConsoleColor.Green, ConsoleColor.Green, ConsoleColor.Green });

            // Then display the graphics
            Graphics.Display();
        }
    }
}
