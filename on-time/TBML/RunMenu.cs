using System;
using System.Collections.Generic;

namespace ontime.TBML
{
    public static class RunMenu
    {
        public static void Run(char[] tbml)
        {
            string menu = "";
            bool center_x = false;
            bool center_y = false;

            int start_y = 0;
            int start_x = 0;

            List<string> text = new List<string>();

            // format file
            for(int i = 0; i < tbml.Length; i++)
            {
                if (tbml[i] == '<' || tbml[i] == '>')
                    tbml[i] = '|';
            }

            // process file
            for(int i = 0; i < tbml.Length; i++)
            {
                if(tbml[i] == '|')
                {
                    string tag = "";
                    string contents = "";

                    for (int j = i + 1; j < tbml.Length; j++)
                    {
                        if (tbml[j] == '|')
                        {
                            for (int a = j + 1; a < tbml.Length; a++)
                            {
                                if(tbml[a] == '|')
                                {
                                    i = a;
                                    break;
                                }
                                else
                                {
                                    contents += tbml[a];
                                }
                            }
                            break;
                        }
                        else
                        {
                            tag += tbml[j];
                        }
                    }

                    if(tag == "tx")
                    {
                        text.Add("x" + contents);
                    }
                    else if(tag == "txs")
                    {
                        text.Add("!" + contents);
                    }
                    else if(tag == "ms")
                    {
                        text.Add("1" + contents);
                    }
                    else if(tag == "menu")
                    {
                        menu = contents;
                    }
                    else if(tag == "style")
                    {
                        string[] split = contents.Split(':');

                        if(split[0] == "centered")
                        {
                            if (split[1] == "x")
                            {
                                center_x = true;
                            }
                            else if (split[1] == "y")
                            {
                                center_y = true;
                            }
                            else
                            {
                                Graphics.Reset();
                                Graphics.WriteLine("TBML ERROR : UNKNOWN AXIS \"" + split[1] + "\"", 30, 5, ConsoleColor.Red);
                                Graphics.Display();

                                Console.ReadKey(true);

                                Environment.Exit(-1);
                            }
                        }
                        else if (split[0] == "start")
                        {
                            if (split[1] == "x")
                            {
                                start_x = int.Parse(split[2]);
                            }
                            else if (split[1] == "y")
                            {
                                start_y = int.Parse(split[2]);
                            }
                            else
                            {
                                Graphics.Reset();
                                Graphics.WriteLine("TBML ERROR : UNKNOWN AXIS \"" + split[1] + "\"", 30, 5, ConsoleColor.Red);
                                Graphics.Display();

                                Console.ReadKey(true);

                                Environment.Exit(-1);
                            }
                        }
                        else
                        {
                            Graphics.Reset();
                            Graphics.WriteLine("TBML ERROR : UNKNOWN STYLE \"" + split[0] + "\"", 30, 5, ConsoleColor.Red);
                            Graphics.Display();

                            Console.ReadKey(true);

                            Environment.Exit(-1);
                        }
                    }
                    else if(tag == "break")
                    {
                        text.Add("2" + contents);
                    }
                    else if(tag == "!")
                    {

                    }
                    else
                    {
                        Graphics.Reset();
                        Graphics.WriteLine("TBML ERROR : UNKNOWN TAG \"" + tag + "\"", 30, 5, ConsoleColor.Red);
                        Graphics.Display();

                        Console.ReadKey(true);

                        Environment.Exit(-1);
                    }
                }
            }

            // display file
            if (center_y)
                start_y = 12 - text.Count / 2;

            Graphics.ClearScreen();

            foreach (string s in text)
            {
                string r = "";

                if (center_x)
                {
                    start_x = 40 - (s.Length - 1) / 2;
                }

                int rx = start_x;
                int ry = start_y;

                int fg = 15;
                int bg = 0;

                if(s[0] == '!')
                {
                    string[] split = s.Remove(0, 1).Split(':');
                    rx = int.Parse(split[0]);
                    ry = int.Parse(split[1]);
                    fg = int.Parse(split[2]);
                    bg = int.Parse(split[3]);

                    r = split[4];
                }
                else if(s[0] == '1')
                {
                    if(menu == "main-menu")
                    {
                        fg = 12;

                        r = Game.Data.Menus.Greetings
                        [new Random((int)DateTime.Now.Ticks).Next(0, Game.Data.Menus.Greetings.Length)];

                        if (center_x)
                        {
                            start_x = 40 - r.Length / 2;
                        }
                    }
                }
                else if(s[0] == '2')
                {
                    r = "";
                }
                else
                {
                    r = s.Remove(0, 1);
                }

                Graphics.WriteLine(r, rx, ry, (ConsoleColor)fg, (ConsoleColor)bg);

                start_y++;
            }

            Graphics.Display();

            // input loop
            bool Run = true;

            while (Run)
            {
                if(menu == "main-menu")
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.P:
                            Menus.PlayGame.Run();
                            Run = false;
                            RunMenu.Run(tbml);
                            break;
                        case ConsoleKey.Q:
                            Graphics.Reset();
                            Run = false;
                            break;
                    }
                }
                else
                {
                    Graphics.Reset();
                    Graphics.WriteLine("TBML ERROR : UNKNOWN MENU \"" + menu + "\"", 30, 5, ConsoleColor.Red);
                    Graphics.Display();

                    Console.ReadKey(true);

                    Environment.Exit(-1);
                }
            }
        }
    }
}
