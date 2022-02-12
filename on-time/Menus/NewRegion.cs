using System;
using System.IO;
using ontime.Game.Region;

namespace ontime.Menus
{
    // Create a new region to play on
    public static class NewRegion
    {
        public static void Run()
        {
            bool Run = true;
            Random rand = new Random((int)DateTime.Now.Ticks);

            RegionData rd = new RegionData
            {
                Name = Game.Gen.Name()
            };

            rd.Region = 0;

            while (true)
            {
                rd.Region++;

                if(!Directory.Exists("Save/region" + rd.Region))
                {
                    break;
                }
            }

            bool Make = true;

            while (Run)
            {
                Graphics.ClearScreen();
                Graphics.WriteLine("[R]EGION NAME  :" + rd.Name, 30, 5);
                Graphics.WriteLine("WORLD [W]IDTH  :" + rd.Width, 30, 6);
                Graphics.WriteLine("WORLD [H]EIGHT :" + rd.Height, 30, 7);

                Graphics.WriteLine("r - Random name.", 2, 20, new ConsoleColor[] { ConsoleColor.Green });
                Graphics.WriteLine("q - Cancel.", 2, 21, new ConsoleColor[] { ConsoleColor.Red });
                Graphics.WriteLine("y - Finish region.", 2, 22, new ConsoleColor[] { ConsoleColor.Green });
                Graphics.Display();

                switch (MainClass.ReadKey().KeyChar)
                {
                    case 'R':
                        Console.ResetColor();
                        Console.Clear();

                        Console.WriteLine("Region name:");
                        rd.Name = Console.ReadLine();

                        Graphics.Reset();
                        break;
                    case 'W':
                        Console.ResetColor();
                        Console.Clear();

                        Console.WriteLine("Region width:");
                        rd.Width = int.Parse(Console.ReadLine());

                        Graphics.Reset();
                        break;
                    case 'H':
                        Console.ResetColor();
                        Console.Clear();

                        Console.WriteLine("Region height:");
                        rd.Height = int.Parse(Console.ReadLine());

                        Graphics.Reset();
                        break;
                    case 'y':
                        Run = false;
                        break;
                    case 'q':
                        Run = false;
                        Make = false;
                        break;
                    case 'r':
                        rd.Name = Game.Gen.Name();
                        break;

                }
            }

            if (Make)
                CreateRegion(rd);
        }


        public static void CreateRegion(RegionData rd)
        {
            Console.Clear();
            Console.WriteLine("Creating region " + rd.Region);
            Console.WriteLine("    " + rd.Name);

            // Working directory for this save
            Directory.CreateDirectory("Save/region" + rd.Region);
            rd.Savef("Save/region" + rd.Region + "/.region");

            Directory.CreateDirectory("Save/region" + rd.Region + "/Data");

            Console.WriteLine("Forming " + rd.Name + " in darkness.");

            rd.Generate();
            Console.WriteLine("    Saving map.");
            rd.SaveMapf("Save/region" + rd.Region + "/Data/map");

            Graphics.Reset();

            rd.ViewMap();
        }
    }
}
