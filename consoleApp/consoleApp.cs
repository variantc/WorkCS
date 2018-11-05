using System;

public class consoleApp
{
    public static void Main ()
    {
        int height = 40;
        int width = 70;
        int numLines = 20;
        int counter = numLines;

        bool quit = false;
        bool nextTurn = false;
        int posX = 10;
        int posY = 5;

        while (!quit) 
        {
            
            nextTurn = false;
            while (!nextTurn)
            {
                Console.WriteLine("Here");
//                switch (Console.ReadKey().Key)
//                {
//                    case ConsoleKey.A :
//                        Console.WriteLine(counter);
//                        break;
//                }

                if (Console.ReadKey().Key == ConsoleKey.A)
                {
                    Console.Clear();
                    Console.WriteLine("Bongo");
                    nextTurn = true;
                }
//                counter--;
            }

        }

//        for (int j = 0; j < numLines - 1; j++)
//        {
//            for (int i = 0; i < numLines; i++)
//            {
//                if (counter == i)
//                {
//                    Console.WriteLine("Two");
//                }
//                Console.WriteLine(i + " " + numLines);
//            }
//            Console.ReadLine();
//            counter--;
//        
//        }
    
    }
}
