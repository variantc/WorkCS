using System;
using System.Collections.Generic;

public class MatProp
{
    public static void Main()
    {
        // Set to start in Main Menu
        State menuState = State.MainMenu;
        bool quit = false;

        Dictionary<int,double> Sy = new Dictionary<int,double>()
        { 
            {100,	36},
            {150,	33.8},
            {200,	33},
            {250,	32.4},
            {300,	31.8},
            {400,	30.8},
            {500,	29.3},
            {600,	27.6},
            {650,	26.7},
            {700,	25.8},
            {750,	24.9},
            {800,	24.1},
            {850,	23.4},
            {900,	22.8},
            {950,	22.1},
            {1000,	21.4}
        };

        Dictionary<int,double> Su = new Dictionary<int,double>();
        Dictionary<int,double> Sm = new Dictionary<int,double>();
        Dictionary<int,double> E = new Dictionary<int,double>();

        Material mat = new Material("newMat",Sy,Su,Sm,E);


/*
*/
        Console.Write("Enter Temperature: ");
        string input = Console.ReadLine();
        int temp = Int32.Parse(input);
        
        Console.WriteLine(mat.ReturnValue(temp));
//        for (int i=0; i<15; i++) {
//            if (mat.SyArray[i,0] > temp) {
//                if (i==0)
//                    Console.Write(mat.SyArray[i-1,1]);
//                else
//                    Console.Write(mat.SyArray[i-1,1]);
//                Console.WriteLine(" ksi.");
//            }
//        }

//        ElementType[] elemArray = (ElementType[])Enum.GetNames(typeof(ElementType));

        // Loop until 'q' is pressed
        string message = "";
//        while (!quit)
//        {
//
//            // Main Menu
//            if (menuState == State.MainMenu)
//            {
//                Console.Clear();
//                Console.WriteLine(message);
//                message = "";
//                PrintMainMenu();
//
//                string input = Console.ReadLine();
//
//                // TODO: make then input value automatically pick the correct dictionary
//                // entry
//                switch (input)
//                {
//                    case "1":
//                        // Take to Material Property menu
//                        menuState = State.EnterMaterialProps;
//                        break;
//
//                    case "a":
//                    case "A":
//                        menuState = State.DisplayResults;
//                        break;
//
//                    case "q":
//                    case "Q":
//                        quit = true;
//                        continue;
//                    default:
//                        message = "\n          INVALID SELECTION";
//                        break;
//                }
//            } 
//
//            // Enter Properties
//            else if (menuState == State.EnterMaterialProps)
//            {
//                Console.Clear();
//                menuState = State.MainMenu;
//            }
//            
//            // Display Results
//            else if (menuState == State.DisplayResults)
//            {
//                Console.Clear();
//                Console.WriteLine();
//
//                Console.WriteLine("Enter to end");
//                // Wait for enter:
//                Console.ReadLine();
//                quit = true;
//            }
//        }
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
    public Dictionary<int,double> SyArray = new Dictionary<int,double>();
    public Dictionary<int,double> SuArray = new Dictionary<int,double>();
    public Dictionary<int,double> SmArray = new Dictionary<int,double>();
    public Dictionary<int,double> EArray = new Dictionary<int,double>();

    public Material(string name, 
                    Dictionary<int,double> Sy, 
                    Dictionary<int,double> Su, 
                    Dictionary<int,double> Sm, 
                    Dictionary<int,double> E)
    {
        materialName = name;
        SyArray = Sy;
        SuArray = Su;
        SmArray = Sm;
        EArray = E;
    }

    public double ReturnValue (int temperature) {
        double val1;
        double val2;

        int t1;
        int t2;

        // Try to get a value at the exact temperature
        if(SyArray.TryGetValue(temperature, out val1))
            return val1;

        else  {
            int step = 50;

            var tTemp = temperature - temperature%step;
            while(!SyArray.TryGetValue(tTemp, out val1)) {
                tTemp -= step;
            }
            t1 = tTemp;

            tTemp = temperature - temperature%step + step;
            while(!SyArray.TryGetValue(tTemp, out val2)) {
                tTemp += step;
            }
            t2 = tTemp;
        }

        // return interpolated value
        return(val1 + (temperature - t1)*(val2-val1)/(t2-t1));

    }
}

public enum State 
{
    MainMenu, 
    EnterMaterialProps,
    DisplayResults
};
