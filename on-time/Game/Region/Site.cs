using System;
namespace ontime.Game.Region
{
    // Site class
    public class Site
    {
        public static int Width = 128;  // Width/Height, used for main site; used to calculate travel distance.
        public static int Height = 128;
        public static int Tall = 32;


        public string Name;             // I.E. new york
        public byte Type;               // I.E. plains, mountains, etc.
        public byte Biome;              // I.E. cold, temperate, warm, swamp, et.

        public void New()
        {
            if (Game.Gen.rand.Next(0, 3) == 0)
            {
                Name = Game.Gen.LegendaryName(false);
            }
            else
            {
                Name = Game.Gen.Name();
            }
            Type = 0;

            if (Game.Gen.rand.Next(0, 10) == 0)
            {
                Type = 1;
            }


            Biome = 1;// temperate
        }
    }
}
