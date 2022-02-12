using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace ontime.Game.Site
{
    public class Map
    {
        public int Width = Region.Site.Width;   // Width/Height of the map (site)
        public int Height = Region.Site.Height;
        public int Tall = Region.Site.Tall;

        public Block[,,] Blocks;

        // Loading and saving of the main site.
        public void Loadf(string fileName)
        {
            byte[] data = File.ReadAllBytes(fileName);
            string[] info = File.ReadAllLines(fileName + "_info");

            Tall = int.Parse(info[0]);
            Width = int.Parse(info[1]);
            Height = int.Parse(info[2]);

            int loc = 0;

            for (int z = 0; z < Tall; z++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        Blocks[z, x, y].ID = BitConverter.ToInt16(data, loc);

                        loc += 2;
                    }
                }
            }
        }
        public void Savef(string fileName)
        {
            List<byte> data = new List<byte>();

            for (int z = 0; z < Tall; z++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        foreach (byte b in BitConverter.GetBytes(Blocks[z, x, y].ID))
                        {
                            data.Add(b);
                        }
                    }
                }
            }

            File.WriteAllBytes(fileName, data.ToArray());

            File.WriteAllLines(fileName + "_info", new string[] { Tall.ToString(), Width.ToString(), Height.ToString() });
        }


        // Init the map
        public void Init()
        {
            Blocks = new Block[Tall, Width, Height];        // Z, X, Y    Z is UP
            for(int z = 0; z < Tall; z++)
            {
                for (int x = 0; x < Width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        Blocks[z, x, y] = new Block();
                    }
                }
            }
        }

        public void Generate()
        {
            Block air = new Block();
            List<Block> stone = new List<Block>();
            List<Block> grass = new List<Block>();
            List<Block> dirt = new List<Block>();

            short cid = 0;

            foreach(BlockData b in Shared.BlockData)
            {
                switch (b.gen)
                {
                    case GenType.air:
                        air = new Block(cid);
                        break;
                    case GenType.stone:
                        stone.Add(new Block(cid));
                        break;
                    case GenType.grass:
                        grass.Add(new Block(cid));
                        break;
                    case GenType.dirt:
                        dirt.Add(new Block(cid));
                        break;
                }

                cid++;
            }

            FastNoiseLite fn = new FastNoiseLite((int)DateTime.Now.Ticks);
            fn.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
            fn.SetFractalOctaves(6);

            Thread.Sleep(32);

            FastNoiseLite fn2 = new FastNoiseLite((int)DateTime.Now.Ticks);
            fn2.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
            fn2.SetFractalOctaves(6);

            Random rand = new Random((int)DateTime.Now.Ticks);

            for (int x = 0; x < Width; x++)
            {
                for(int y = 0; y < Height; y++)
                {
                    int b = (int)fn.GetNoise(x * 10, y * 10) + Tall / 2;

                    for(int z = 0; z < b; z++)
                    {
                        if(z >= 0 && z < Tall)
                        {
                            if (stone.Count > 0)
                            {
                                bool go = true;

                                while (go)
                                {
                                    foreach(Block block in stone)
                                    {
                                        int chance = (int)(100 - Shared.BlockData[block.ID].genData - fn2.GetNoise(x * 5, y * 5));

                                        if (chance <= 1)
                                            chance = 2;

                                        if(rand.Next(0, chance) == 0)
                                        {
                                            go = false;
                                            Blocks[z, x, y] = block;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


        // Drawing tools
        public void Fill(int z, int x, int y, int toz, int tox, int toy, Block block)
        {
            for (; z < toz; z++)
            {
                for(; x < tox; x++) 
                { 
                    for(; y < toy; y++)
                    {
                        Blocks[z, x, y] = block;
                    }
                }
            }
        }
    }
}
