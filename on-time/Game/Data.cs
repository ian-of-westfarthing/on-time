using System;
using System.IO;

namespace ontime.Game
{
    public static class Data
    {
        // Menus and related data
        public static class Menus
        {
            public static string[] Greetings;

            public static void Load()
            {
                Greetings = File.ReadAllLines("Data/greetings.txt");
            }
        }

        // Adventure mode, time-travel / exploration
        public static class AdventureMode
        {
            public static string Embark;
            public static string Seasons;
            public static string Death;

            public static void Load()
            {
                Embark  = File.ReadAllText("Data/AdventureMode/Text/embark.txt");
                Seasons = File.ReadAllText("Data/AdventureMode/Text/seasons.txt");
                Death   = File.ReadAllText("Data/AdventureMode/Text/death.txt");
            }
        }
        // Fortress mode, manage a fortress / retire to adventure mode
        public static class FortressMode
        {
            public static string Embark;
            public static string Seasons;
            public static string Death;

            public static void Load()
            {
                Embark  = File.ReadAllText("Data/FortressMode/Text/embark.txt");
                Seasons = File.ReadAllText("Data/FortressMode/Text/seasons.txt");
                Death   = File.ReadAllText("Data/FortressMode/Text/death.txt");
            }
        }
    }
}
