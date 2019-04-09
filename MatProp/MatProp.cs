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

        DirectoryInfo dir = new DirectoryInfo(@DATALOC);
        FileInfo[] matDatFiles = dir.GetFiles("*.dat");
        Console.WriteLine("\n=================================================\n");
        Console.WriteLine("Total number of files: {0}", matDatFiles.Length);

        Material[] mats = new Material[matDatFiles.Length];
        int materialIndex = 0;

        // Create materials from data files
        foreach( FileInfo f in matDatFiles) {
            Console.WriteLine("\n=================================================\n");
            Console.WriteLine("Filename : {0}", f.Name);
            Console.WriteLine("File Length : {0}", f.Length);
            Console.WriteLine("Creation Time : {0}", f.CreationTime);
            StreamReader s = File.OpenText(DATALOC + f.Name);
            string read = null;
            List<string> lines = new List<string>();

            while ((read = s.ReadLine()) != null)
                lines.Add(read);

            // Get the name of the material from the 1st line, and year from the 2nd
            // TODO: Check?
            string matName = lines[0];
            string year = lines[1];

            // Close the stream
            s.Close();
            
            Console.WriteLine("\n=================================================\n");

            mats[materialIndex] = new Material(matName,year);
            mats[materialIndex].CreateArrays(lines.ToArray());
            //mats[materialIndex].PrintValues(mats[materialIndex].Sy);
            materialIndex++;
            Console.WriteLine("\n=================================================\n");
        }


        Console.Write("\nEnter Temperature: \n");
        // TODO: string input = Console.ReadLine();

        // TODO: Add here methods for dealing with erroneous inputs
        int temp = 60; // Int32.Parse(input);
        
        for (int i=0; i<mats.Length; i++) {
            Console.WriteLine("Sm of {0} = {1}", 
                    mats[i].materialName,
                    mats[i].ReturnValue(temp,"Sm"));
            Console.WriteLine(mats[i].ReturnValue(temp,"Sy"));
            Console.WriteLine(mats[i].ReturnValue(temp,"E"));
            Console.WriteLine("Material Name: " + mats[i].materialName);
        }

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
    public Dictionary<int,double> Sy = new Dictionary<int,double>();
    public Dictionary<int,double> Su = new Dictionary<int,double>();
    public Dictionary<int,double> Sm = new Dictionary<int,double>();
    public Dictionary<int,double> E = new Dictionary<int,double>();
    public string codeYear;

    // Constructor
    public Material(string name, 
                    //Dictionary<int,double> SyArray, 
                    //Dictionary<int,double> SuArray, 
                    //Dictionary<int,double> SmArray, 
                    //Dictionary<int,double> EArray,
                    string year)
    {
        materialName = name;
        //Sy = SyArray;
        //Su = SuArray;
        //Sm = SmArray;
        //E = EArray;
        codeYear = year;
    }

    public double ReturnValue (int temperature, string property) {
        double val1;
        double val2;

        int t1;
        int t2;

        // temp dictionary to access data
        Dictionary<int,double> dict = new Dictionary<int,Double>();

        // choose which dictionary to access:
        switch (property) {
            case "Sy" :
                dict = Sy;
                break;
            case "Su" :
                dict = Su;
                break;
            case "Sm" :
                dict = Sm;
                break;
            case "E" :
                dict = E;
                break;
            default :
                Console.WriteLine("Error in Material.ReturnValue");
                break;
        }

        // Since all code materials are -20->100degF (?) make at least 100degF
        // BUT: If E value, go to 70 instead
        if((property == "E") && (temperature < 70))
            temperature = 70;
        else if(temperature <= 100)
            temperature = 100;

        if(temperature > 1000) {
            Console.WriteLine("Temperature out of range");
            return 0;
        }

        // Try to get a value at the exact temperature
        if(dict.TryGetValue(temperature, out val1)) {
            if (property == "E")
                val1*=1000;
            return val1;
        }
        // Then if nec., round down to nearest 'step', then step down until find value
        else  {
            int step = 10;

            var tTemp = temperature - temperature%step;
            while(!dict.TryGetValue(tTemp, out val1)) {
                tTemp -= step;
                // emergency escape:
                if (tTemp < 0) {
                    Console.WriteLine("ERROR: Missing or erroneous " + property +
                                        " data for " + this.materialName);
                    break;
                }
            }
            t1 = tTemp;

            // Then do the opposite, stepping up
            tTemp = temperature - temperature%step + step;
            while(!dict.TryGetValue(tTemp, out val2)) {
                tTemp += step;
                // emergency escape:
                if (tTemp > 10000) {
                    Console.WriteLine("ERROR: Missing or erroneous " + property +
                                        " data for " + this.materialName);
                    break;
                }
            }
            t2 = tTemp;
        }

        // return interpolated value 
        double returnVal = val1 + (temperature - t1)*(val2-val1)/(t2-t1);
        if (property == "E")
            returnVal *= 1000;
        return(returnVal);

    }

    public void PrintValues (Dictionary<int,double> dict) {
        Console.Write("\n");
        foreach (KeyValuePair<int,double> data in dict) {
            Console.WriteLine("\t{0} degF: \t{1}", data.Key, data.Value);
        }
    }

    public void CreateArrays (string[] dataStrings) {
        // store the 'type' of data so we can store the values in the correct array
        string targetType = "";
        string[] strings = new string[2];

        Console.WriteLine("Parsing data");

        // change the target type each time we hit the keywords Sy, Su, etc.
        for (int i=0; i<dataStrings.Length; i++) {
            switch(dataStrings[i]) {
                case "Sy" :
                    targetType = "Sy";
                    Console.WriteLine("Target Type: Sy");
                    continue;
                case "Su" :
                    targetType = "Su";
                    Console.WriteLine("Target Type: Su");
                    continue;
                case "Sm" :
                    targetType = "Sm";
                    Console.WriteLine("Target Type: Sm");
                    continue;
                case "E" :
                    targetType = "E";
                    Console.WriteLine("Target Type: E");
                    continue;
                case "" :
                    targetType = "";
                    Console.WriteLine("/");
                    continue;
                default :
                    Console.Write(".");
                    break;
            }

            // split the data and store in the relevant dictionary
            switch(targetType) {
                case "Sy" :
                    strings = dataStrings[i].Split('\t');
                    Sy[Int32.Parse(strings[0])] = double.Parse(strings[1]);
                    break;
                case "Su" :
                    strings = dataStrings[i].Split('\t');
                    Su[Int32.Parse(strings[0])] = double.Parse(strings[1]);
                    break;
                case "Sm" :
                    strings = dataStrings[i].Split('\t');
                    Sm[Int32.Parse(strings[0])] = double.Parse(strings[1]);
                    break;
                case "E" :
                    strings = dataStrings[i].Split('\t');
                    E[Int32.Parse(strings[0])] = double.Parse(strings[1]);
                    break;
                case "" :
                    continue;
                default :
                    Console.WriteLine("Error in Material.CreateArrays");
                    break;
            }
        }
    }
}

public enum State 
{
    MainMenu, 
    EnterMaterialProps,
    DisplayResults
};
