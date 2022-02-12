using System;
using System.IO;

namespace ontime
{
    class MainClass
    {
        public static string version = "v0.1.0-alpha_1";
        public static ConsoleColor BorderColor = ConsoleColor.Gray;


        // Main()
        public static void Main(string[] args)
        {
            // lol :D
            Console.WriteLine(" -+ SaberStorm +-  (of the highest quality)");

            // run the preferences like commands.
            Console.WriteLine("ontime version " + version);
            Console.WriteLine("running prefs...");

            foreach(string s in File.ReadAllLines("User/prefs.txt"))
            {
                Cmd(s);
            }

            Console.WriteLine("loading game data...");

            Console.WriteLine("----Loading shared data");
            Game.Shared.Load();

            Console.WriteLine("----Loading AdventureMode data");
            Game.Data.AdventureMode.Load();
            Console.WriteLine("----Loading FortressMode data");
            Game.Data.FortressMode.Load();
            Console.WriteLine("----Loading menu data");
            Game.Data.Menus.Load();

            Console.WriteLine("----Seeding RNG.");
            Game.Gen.Seed();

            Console.WriteLine("initializing ODBGRS");

            Console.CursorVisible = false;

            // ~ODBGRS~
            Graphics.Reset();

            // Run the main menu with TBML, so people can cutomize it!
            TBML.RunMenu.Run(File.ReadAllText("Menus/MainMenu.tbml").ToCharArray());

            Console.CursorVisible = true;
        }

        public static ConsoleKeyInfo ReadKey()
        {
            var k = Console.ReadKey(true);

            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }

            return k;
        }

        public static void Cmd(string cmd)
        {
            string[] split = cmd.Split();

            if(split[0] == "bc")
            {
                BorderColor = (ConsoleColor)int.Parse(split[1]);
                Console.WriteLine("----Set border color.");

            }
            else if(split[0] == "" || split[0] == "#")
            {

            }
            else
            {
                Console.WriteLine("----Unknown command: " + split[0]);
            }
        }
    }
}
