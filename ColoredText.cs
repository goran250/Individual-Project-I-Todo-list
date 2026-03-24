
using System;
using System.Runtime.CompilerServices;

namespace Project_I_Todo_list
{
    public static class ColoredText
    {
        public static void WriteLine(string text, ConsoleColor Color)
        {
            Console.ForegroundColor = Color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void Write(string text, ConsoleColor Color)
        {
            Console.ForegroundColor = Color;
            Console.Write(text);
            Console.ResetColor();
        }
    }
}