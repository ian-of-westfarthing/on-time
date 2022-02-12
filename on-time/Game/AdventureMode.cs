using System;
namespace ontime.Game
{
    /// <summary>
    /// This is adventure mode, play as a rogue.
    /// </summary>
    public static class AdventureMode
    {
        public static etc.Player Player;

        /// <summary>
        /// Run the game. / Play the game.
        /// </summary>
        public static void Run()
        {
            bool run = true;

            while (run)
            {
                DrawGraphics();
                Graphics.Display();

                switch (MainClass.ReadKey().Key)
                {

                }
            }
        }

        /// <summary>
        /// Update the world
        /// </summary>
        public static void Update()
        {
            if(Player.Z > 0 && Shared.BlockData[Shared.CurrentSite_Map.Blocks[Player.Z - 1, Player.X, Player.Y].ID].gen == GenType.air)
            {
                Player.Z--;
            }
        }

        /// <summary>
        /// Draw the graphics for adventure mode.
        /// </summary>
        public static void DrawGraphics()
        {
            // Bounds checking
            if (Player.Z < 0)
                Player.Z = 0;

            if (Player.Z >= Shared.CurrentSite_Map.Tall)
                Player.Z = Shared.CurrentSite_Map.Tall - 1;

            // Rendering positions
            int RX = 0;
            int RY = 0;

            // Rendering the screen
            for (int x = Player.X - 39; x < Player.X + 40; x++)
            {
                for(int y = Player.Y - 12; y < Player.Y + 12; y++)
                {
                    if (x >= 0 && x < Shared.CurrentSite_Map.Width && y >= 0 && y < Shared.CurrentSite_Map.Height)
                    {
                        Graphics.WriteAt(Shared.BlockData[Shared.CurrentSite_Map.Blocks[Player.Z, x, y].ID].texture, RX, RY, Shared.BlockData[Shared.CurrentSite_Map.Blocks[Player.Z, x, y].ID].fg, Shared.BlockData[Shared.CurrentSite_Map.Blocks[Player.Z, x, y].ID].bg);
                    }

                    RY++;
                }
                RY = 0;
                RX++;
            }
        }
    }
}
