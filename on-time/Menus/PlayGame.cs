using System;
using System.IO;
using ontime.Game;
using ontime.Game.Region;
using ontime.Menus;

namespace ontime.Menus
{
    /// <summary>
    /// Menu for playing and world setup.
    /// </summary>
    public static class PlayGame
    {
        /// <summary>
        /// Run the menu.
        /// </summary>
        public static void Run()
        {
            // Reset the graphics and run the menu...
            Graphics.Reset();
            bool Run = true;

            // Get a list of regions
            string[] Raw_Regions = 
                                    Directory.GetDirectories("Save/");
            // Extract all the data from them
            RegionData[] Regions = 
                                    new RegionData[Raw_Regions.Length];

            // Process the regions.
            for(int i = 0; i < Regions.Length; i++)
            {
                // Initialize the region data.
                Regions[i] = new RegionData();
                // Load from file.
                Regions[i].Loadf(Raw_Regions[i] + "/.region");
            }



            // This is the run loop
            while (Run)
            {
                // Clear the screen
                Graphics.ClearScreen();
                // Ontime version xx
                Graphics.WriteLine("-+ on-time - " + MainClass.version + " +-", 30, 5);


                // If there are > 0 regions add an option to play.
                if (Regions.Length > 0)
                {
                    // this option lets you start playing, or continue where you left off.
                    Graphics.WriteLine("P]lay or Start playing a region.", 30, 10);
                }

                // also create or quit to main menu
                Graphics.WriteLine("C]reate a new region.", 30, 11);
                Graphics.WriteLine("Q]uit to main menu.", 30, 12);
                // Display it all
                Graphics.Display();

                // Receive input
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.P:
                        // make sure there are regions
                        if (Regions.Length > 0)
                        {
                            // the selector
                            int sel = 0;
                            bool play = true;
                            
                            // this is the start/play menu
                            while (Run)
                            {
                                // cls
                                Graphics.ClearScreen();
                                
                                // instructions
                                Graphics.WriteLine("Press enter to START/PLAY, Up/Down Arrow to move cursor. Press ESCAPE to exit", 1, 1, ConsoleColor.Green);
                                
                                // display options
                                for(int a = 0; a < Regions.Length; a++)
                                {
                                    Graphics.WriteLine((sel == a ? "--------------------------------------->" : "") + (Regions[a].Started ? "[STARTED] " : "[START?] ") + Regions[a].Name + " : region" + Regions[a].Region, 1, a + 3);
                                }
                                
                                // ODBGRS
                                Graphics.Display();
                                
                                // input
                                switch (MainClass.ReadKey().Key)
                                {
                                    case ConsoleKey.UpArrow:
                                        if (sel > 0)
                                            sel--;
                                        break;
                                    case ConsoleKey.DownArrow:
                                        if (sel < Regions.Length - 1)
                                            sel++;
                                        break;
                                    case ConsoleKey.Enter:
                                        Run = false;
                                        break;
                                    case ConsoleKey.Escape:
                                        Run = false;
                                        play = false;
                                        break;
                                }
                            }
                            
                            
                            // enter instead of escape
                            if (play)
                            {
                                
                                // START
                                if (Regions[sel].Started)
                                {
                                    Shared.CurrentRegion = Regions[sel];
                                    Shared.LoadRegion();

                                    if (Shared.CurrentRegion.FortressMode)
                                        FortressMode.Run();
                                    else
                                        AdventureMode.Run();
                                }
                                // or PLAY
                                else
                                {
                                    Shared.CurrentRegion = Regions[sel];
                                    Shared.LoadRegion();
                                    Shared.SetupNewWorld();
                                }
                            }
                        }
                        break;
                    case ConsoleKey.C:
                        // New region creation menu.
                        NewRegion.Run();
                        break;
                    case ConsoleKey.Q:
                        // Quit
                        Graphics.Reset();
                        Run = false;
                        break;
                }
            }
        }
    }
}
