using System;
namespace ontime.Game.Region
{
    // Site biome data
    public class SiteBiomeData
    {
        public string Name;
        public bool Evil;               // Evil biomes...

        public SiteBiomeData(string name, bool evil)
        {
            Name = name;
            Evil = evil;
        }
    }
}
