using System;
using System.Collections.Generic;

public class MatProp
{
    public static void Main()
    {
        // Set to start in Main Menu
        State menuState = State.MainMenu;
        bool quit = false;


//        Console.WriteLine(Enum.GetNames(typeof(ElementType)).Length);

//        ElementType[] elemArray = (ElementType[])Enum.GetNames(typeof(ElementType));

        // Loop until 'q' is pressed
        string message = "";
        while (!quit)
        {

            // Main Menu
            if (menuState == State.MainMenu)
            {
                Console.Clear();
                Console.WriteLine(message);
                message = "";
                PrintMainMenu();

                string input = Console.ReadLine();

                // TODO: make then input value automatically pick the correct dictionary
                // entry
                switch (input)
                {
                    case "1":
                        // Take to Material Property menu
                        menuState = State.EnterMaterialProps;
                        break;

                    case "a":
                    case "A":
                        menuState = State.DisplayResults;
                        break;

                    case "q":
                    case "Q":
                        quit = true;
                        continue;
                    default:
                        message = "\n          INVALID SELECTION";
                        break;
                }
            } 

            // Enter Properties
            else if (menuState == State.EnterMaterialProps)
            {
                Console.Clear();
                menuState = State.MainMenu;
            }
            
            // Display Results
            else if (menuState == State.DisplayResults)
            {
                Console.Clear();
                Console.WriteLine();

                Console.WriteLine("Enter to end");
                // Wait for enter:
                Console.ReadLine();
                quit = true;
            }
        }
    }


    public static void EnterMaterialPropsMenu(string minMax)
    {
        Console.WriteLine("");
        Console.WriteLine("========================================");
        Console.WriteLine("");
        Console.WriteLine(" Enter " + minMax + " percentage composition:");
        Console.WriteLine("");
        Console.WriteLine("========================================");
        Console.WriteLine("");
        
    }

    // print out the menu
    private static void PrintMainMenu()
    {
        PrintAvailableElements();
    }

    static void PrintAvailableElements()
    {
        Console.WriteLine("");
        Console.WriteLine("========================================");
        Console.WriteLine("");
        Console.WriteLine("Select element in composition by number:");
        Console.WriteLine("");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("");
        
        
        Console.WriteLine("");
        Console.WriteLine("         Enter 'a' to analyse ");
        Console.WriteLine("");
        Console.WriteLine("         Enter 'q' to quit ");
        Console.WriteLine("");
        Console.WriteLine("========================================");
        Console.WriteLine("");
        Console.WriteLine("");

    }
}

public class Material 
{
    public string materialName;
    public int[,] SyArray;
    public int[,] SuArray;
    public int[,] SmArray;
    public int[,] EArray;

    public Material(string name, int[,] Sy, int[,] Su, int[,] Sm, int[,] E)
    {
        materialName = name;
        SyArray = Sy;
        SuArray = Su;
        SmArray = Sm;
        EArray = E;
    }
}

public enum State 
{
    MainMenu, 
    EnterMaterialProps,
    DisplayResults
};

