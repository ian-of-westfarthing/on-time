using System;
namespace ontime
{
    public static class Graphics
    {
        public static char[,] buffer = new char[79, 24];
        public static char[,] printed = new char[79, 24];

        public static ConsoleColor[,] cbuffer = new ConsoleColor[79, 24];
        public static ConsoleColor[,] cprinted = new ConsoleColor[79, 24];

        public static ConsoleColor[,] bcbuffer = new ConsoleColor[79, 24];
        public static ConsoleColor[,] bcprinted = new ConsoleColor[79, 24];

        // An efficient double buffered rendering system,
        //  which remembers each character printed and only prints
        //  neccesary changes.
        public static void Display()
        {
            for(int x = 0; x < 79; x++)
            {
                for(int y = 0; y < 24; y++)
                {
                    if     ( buffer[x, y] != printed[x, y]      // Text character buffers
                        || cbuffer[x, y]  != cprinted[x, y]     // Foreground color buffers
                        || bcbuffer[x, y] != bcprinted[x, y])    // Background color buffers
                    {
                        // Set foreground color, push to printed buffer
                        Console.ForegroundColor = cbuffer[x, y];
                        cprinted[x, y] = cbuffer[x, y];

                        // Set background color, push to printed buffer
                        Console.BackgroundColor = bcbuffer[x, y];
                        bcprinted[x, y] = bcbuffer[x, y];

                        // Set Cursor position, write text to screen
                        Console.SetCursorPosition(x, y);
                        Console.Write(buffer[x, y]);
                        printed[x, y] = buffer[x, y];
                    }
                }
            }

            Console.SetCursorPosition(0, 0);
        }

        // In case of emergency reset the entire display,
        //  in-game ctrl+R.
        public static void Reset()
        {
            for (int x = 0; x < 79; x++)
            {
                for (int y = 0; y < 24; y++)
                {
                    WriteAt(' ', x, y);
                }
            }

            Display();
        }

        public static void ClearScreen()
        {
            for (int x = 0; x < 79; x++)
            {
                for (int y = 0; y < 24; y++)
                {
                    WriteAt(' ', x, y);
                }
            }
        }

        // --- Graphics/Drawing methods ---

        // Write a character @ x/y of color: Color
        public static void WriteAt(char ch, int x, int y, ConsoleColor Color = ConsoleColor.White, ConsoleColor bgColor = ConsoleColor.Black)
        {
            x %= 79;
            y %= 24;

            buffer[x, y] = ch;
            cbuffer[x, y] = Color;
            bcbuffer[x, y] = bgColor;
        }

        // Write a line of text
        public static void WriteLine(string text, int x, int y, ConsoleColor Color = ConsoleColor.White, ConsoleColor bgColor = ConsoleColor.Black)
        {
            // Loop through all the characters in the string
            for (int i = 0; i < text.Length; i++)
            {
                WriteAt(text[i], x + i, y, Color, bgColor);
            }
        }

        // Write a line of text with different colors
        public static void WriteLine(string text, int x, int y, ConsoleColor[] Colors)
        {
            // Loop through all the characters in the string, print with colors
            for (int i = 0; i < text.Length; i++)
            {
                if(i < Colors.Length)
                {
                    WriteAt(text[i], x + i, y, Colors[i]);
                }
                else
                {
                    WriteAt(text[i], x + i, y, ConsoleColor.White);
                }
            }
        }
    }
}
