using System;
using System.Collections.Generic;
using System.IO;

public class MatProp
{
    public static void Main()
    {
        // Set to start in Main Menu
        State menuState = State.MainMenu;
        bool quit = false;

        // Location of .dat files
        const string DATALOC = "./data/";

        Dictionary<int,double> SyData = new Dictionary<int,double>()
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

        Dictionary<int,double> SuData = new Dictionary<int,double>();
        Dictionary<int,double> SmData = new Dictionary<int,double>();
        Dictionary<int,double> EData = new Dictionary<int,double>();

        string matName = "";

        DirectoryInfo dir = new DirectoryInfo(@DATALOC);
        FileInfo[] matDatFiles = dir.GetFiles("*.dat");
        Console.WriteLine("Total number of files: {0}", matDatFiles.Length);
        foreach( FileInfo f in matDatFiles)
        {
            Console.WriteLine("Name is : {0}", f.Name);
            Console.WriteLine("Length of the file is : {0}", f.Length);
            Console.WriteLine("Creation time is : {0}", f.CreationTime);
            Console.WriteLine("Attributes of the file are : {0}",
                             f.Attributes.ToString());
            StreamReader s = File.OpenText(DATALOC + f.Name);
            string read = null;
            while ((read = s.ReadLine()) != null)
            {
                Console.WriteLine(read);
            }
            // WRONG
            matName = s.ReadLine();
            s.Close();
        }


        Material mat = new Material(matName,SyData,SuData,SmData,EData);
        mat.PrintValues(mat.Sy);

        Console.Write("Enter Temperature: ");
        string input = Console.ReadLine();
        // TODO: Add here methods for dealing with erroneous inputs
        int temp = Int32.Parse(input);
        
        Console.WriteLine(mat.ReturnValue(temp));
        Console.WriteLine(mat.materialName);

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
    public Dictionary<int,double> Sy = new Dictionary<int,double>();
    public Dictionary<int,double> Su = new Dictionary<int,double>();
    public Dictionary<int,double> Sm = new Dictionary<int,double>();
    public Dictionary<int,double> E = new Dictionary<int,double>();

    // Constructor
    public Material(string name, 
                    Dictionary<int,double> SyArray, 
                    Dictionary<int,double> SuArray, 
                    Dictionary<int,double> SmArray, 
                    Dictionary<int,double> EArray)
    {
        materialName = name;
        Sy = SyArray;
        Su = SuArray;
        Sm = SmArray;
        E = EArray;
    }

    public double ReturnValue (int temperature) {
        double val1;
        double val2;

        int t1;
        int t2;

        // Since all code materials are -20->100degF (?) make at least 100degF
        if(temperature <= 100)
            temperature = 100;

        if(temperature > 1000) {
            Console.WriteLine("Temperature out of range");
            return 0;
        }

        // Try to get a value at the exact temperature
        if(Sy.TryGetValue(temperature, out val1))
            return val1;
        // Then if nec., round down to nearest 'step', then step down until find value
        else  {
            int step = 50;

            var tTemp = temperature - temperature%step;
            while(!Sy.TryGetValue(tTemp, out val1)) {
                tTemp -= step;
            }
            t1 = tTemp;

            // Then do the opposite, stepping up
            tTemp = temperature - temperature%step + step;
            while(!Sy.TryGetValue(tTemp, out val2)) {
                tTemp += step;
            }
            t2 = tTemp;
        }

        // return interpolated value 
        return(val1 + (temperature - t1)*(val2-val1)/(t2-t1));

    }

    public void PrintValues (Dictionary<int,double> dict) {
        foreach (KeyValuePair<int,double> data in dict) {
            Console.WriteLine("\t{0} degF: \t{1}", data.Key, data.Value);
        }
    }
}

public enum State 
{
    MainMenu, 
    EnterMaterialProps,
    DisplayResults
};
