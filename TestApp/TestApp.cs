using System;
using System.Collections.Generic;
using System.IO;

public class TestApp
{
    public static void Main()
    {
        bool quit = false;

        while (quit != true) {
            Console.Clear();
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("\n\n\n\n\n");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("\n\n\n\n\n");
            Console.WriteLine("---------------------------------------------");
            if (Console.ReadKey().Key == ConsoleKey.Q) {
                Console.Clear();
                quit = true;
            }
        }
    }
}
