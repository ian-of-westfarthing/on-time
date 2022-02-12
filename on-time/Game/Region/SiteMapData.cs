using System;
namespace ontime.Game.Region
{
    // Site map data
    public class SiteMapData
    {
        public string type;

        public char tex;
        public ConsoleColor col;

        public SiteMapData(string type, char tex, ConsoleColor col)
        {
            this.type = type;
            this.tex = tex;
            this.col = col;
        }
    }
}
