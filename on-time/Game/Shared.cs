using System;
using System.Collections.Generic;
using System.IO;

using ontime.Game.Site;
using ontime.Game.Region;

namespace ontime.Game
{
    public static class Shared
    {
        public static RegionData CurrentRegion;
        public static Region.Site CurrentSite;
        public static Map CurrentSite_Map;

        public static int Site_X;
        public static int Site_Y;

        public static BlockData[] BlockData;

        public static SiteMapData[] SiteMapDatas =
        {
            new SiteMapData("plains", '~', ConsoleColor.Green),
            new SiteMapData("mountains", '^', ConsoleColor.Gray)
        };

        public static SiteBiomeData[] SiteBiomeDatas =
        {
            new SiteBiomeData("cold", false),
            new SiteBiomeData("temperate", false),
            new SiteBiomeData("warm", false),

            new SiteBiomeData("wet", false),
            new SiteBiomeData("snowy", false)
        };


        // Load game related stuff
        public static void Load()
        {
            List<BlockData> bd = new List<BlockData>();

            foreach (string s in File.ReadAllLines("Data/Blocks/blocks.txt"))
            {
                try
                {
                    bd.Add(new BlockData(s));
                }
                catch
                {
                    Console.WriteLine("* could not load block: " + s);
                }
            }

            BlockData = bd.ToArray();
        }

        // Load a region
        public static void LoadRegion()
        {
            Console.Clear();

            Console.WriteLine("Loading...");
            string Region_Folder = "Save/region" + CurrentRegion.Region + "/";

            // Check in case we need to load the map
            if(CurrentRegion.Map == null)
            {
                Console.WriteLine("          Map(s)");
                CurrentRegion.LoadMapf(Region_Folder + "Data/map");
            }

            // If this region has been played.
            if (CurrentRegion.Started)
            {
                Console.WriteLine("          Save data...");
                if (!CurrentRegion.FortressMode)
                {
                    AdventureMode.Player = new etc.Player();
                    AdventureMode.Player.Z = CurrentSite_Map.Tall - 1;
                    AdventureMode.Player.Load(Region_Folder + "Data/player");
                }
            }
            else
            {
                if (!CurrentRegion.FortressMode)
                {
                    AdventureMode.Player = new etc.Player();
                }
            }

            if (CurrentRegion.Started_Map)
            {
                if (CurrentSite_Map == null)
                    CurrentSite_Map = new Map();
                    CurrentSite_Map.Init();

                CurrentSite_Map.Loadf("Save/region" + CurrentRegion.Region + "/Data/Site/map");
            }

            Graphics.Reset();
        }

        // Save a region
        public static void SaveRegion()
        {
            // Region data / region map
            CurrentRegion.Savef("Save/region" + CurrentRegion.Region + "/.region");
            CurrentRegion.SaveMapf("Save/region" + CurrentRegion.Region + "/Data/map");

            if (CurrentRegion.Started)
            {
                if (!CurrentRegion.FortressMode)
                {
                    AdventureMode.Player.Save("Save/region" + CurrentRegion.Region + "/Data/player");
                }
            }

            if (CurrentRegion.Started_Map)
            {
                CurrentSite_Map.Savef("Save/region" + CurrentRegion.Region + "/Data/Site/map");
            }
        }

        // Before playing a new region you must set it up first.
        public static void SetupNewWorld()
        {
            Graphics.Reset();
            bool Run = true;

            int CX = CurrentRegion.Width / 2;
            int CY = CurrentRegion.Height / 2;

            while (Run)
            {
                CurrentRegion.RenderMapView(CX, CY, true);

                switch (MainClass.ReadKey().Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (CX > 0)
                            CX--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (CX < CurrentRegion.Width - 1)
                            CX++;
                        break;
                    case ConsoleKey.UpArrow:
                        if (CY > 0)
                            CY--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (CY < CurrentRegion.Height - 1)
                            CY++;
                        break;
                    case ConsoleKey.Escape:
                        Run = false;
                        break;
                }
            }


            // Set up some data stuff
            Site_X = CX;
            Site_Y = CY;

            CurrentSite = CurrentRegion.Map[Site_X, Site_Y];
            // Generate the site,  and ready it for play...
            GenerateSite();

            CurrentRegion.Started = true;
            CurrentRegion.Started_Map = true;

            Directory.CreateDirectory("Save/region" + CurrentRegion.Region + "/Data/Site/");

            SaveRegion();

        }

        //Generate the current site, when creating a new world.
        public static void GenerateSite()
        {
            CurrentSite_Map = new Map();

            CurrentSite_Map.Init();
            CurrentSite_Map.Generate();
        }
    }

    public class BlockData
    {
        public string name;
        public char texture;
        public ConsoleColor fg;
        public ConsoleColor bg;

        public GenType gen;
        public int genData;

        public bool Visible = true;

        public BlockData(string f)
        {
            string[] split = f.Split(':');

            name = split[0];
            texture = split[1][0];
            fg = (ConsoleColor)int.Parse(split[2]);
            bg = (ConsoleColor)int.Parse(split[3]);

            for(int a = 0; a < 4; a++)
            {
                if (split[4] == ((GenType)a).ToString())
                    gen = (GenType)a;
            }

            genData = int.Parse(split[5]);

            Visible &= split[6] != "no";
        }
    }

    public enum GenType
    {
        air = 0,
        grass = 1,
        stone = 2,
        dirt = 3,

    }
}
