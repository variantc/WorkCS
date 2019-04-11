using System;
using System.Collections.Generic;
using System.IO;

public class MatProp
{
    static int TEMP = 350;

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

            mats[materialIndex] = new Material(matName,year,materialIndex+1);
            mats[materialIndex].CreateArrays(lines.ToArray());
            //mats[materialIndex].PrintValuesOfDict(mats[materialIndex].Sy);
            materialIndex++;
            Console.WriteLine("\n=================================================\n");
        }


//        Console.Write("\nEnter Temperature: \n");
//        // TODO: string input = Console.ReadLine();
//
//        // TODO: Add here methods for dealing with erroneous inputs
//        int temp = 60; // Int32.Parse(input);
//        
//        for (int i=0; i<mats.Length; i++) {
//            Console.WriteLine("Sm of {0} = {1}", 
//                    mats[i].materialName,
//                    mats[i].ReturnValue(temp,"Sm"));
//            Console.WriteLine(mats[i].ReturnValue(temp,"Sy"));
//            Console.WriteLine(mats[i].ReturnValue(temp,"E"));
//            Console.WriteLine("Material Name: " + mats[i].materialName);
//        }

        // Selected material variable for use in menu system
        Material selectedMaterial = new Material();

        Console.WriteLine("\n=================================================\n");
        Console.WriteLine("Total number of files: {0}", matDatFiles.Length);

        // Loop until 'q' is pressed
        string message = "";
        while (!quit)
        {

            // Main Menu
            if (menuState == State.MainMenu)
            {
                //Console.Clear();
                Console.WriteLine(message);
                message = "";
                PrintMainMenu(mats);

                bool MAT_IS_SELECTED = false;

                string input = Console.ReadLine();
                int matNum;

                try {
                    matNum = Int32.Parse(input);
                    MAT_IS_SELECTED = true;
                }
                catch {
                    matNum = -1;
                    if (input != "q" && input != "t")
                        Console.WriteLine("\tInvalid Entry");
                }
                if (matNum > 0) {
                    foreach (Material m in mats) {
                        if (m.materialNumber == matNum)
                            selectedMaterial = m;
                            m.PrintValues();
                    }
                }
                else {
                    switch (input)
                    {
                        case "a":
                        case "A":
                            menuState = State.DisplayResults;
                            break;
                        case "t":
                        case "T":
                            Console.WriteLine("\n\tEnter temperature:");
                            TEMP = Int32.Parse(Console.ReadLine());
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

                // Unneeded? :
                if (MAT_IS_SELECTED) {
                    menuState = State.MaterialSelected;
                    MAT_IS_SELECTED = false;
                }
            } 

            // Enter Properties
            else if (menuState == State.MaterialSelected)
            {
                Console.Clear();
                menuState = State.PropertyMenu;
            }
            
            else if (menuState == State.PropertyMenu)
            {
                MaterialSelectedMenu();
                Dictionary<int,string> choicesDict = selectedMaterial.ListAvailableProperties();

                string input = Console.ReadLine(); 
                int selProp;

                try {
                    selProp = Int32.Parse(input);
                }
                catch {
                    selProp = -1;
                    if (input != "q" && input != "t")
                        Console.WriteLine("\tInvalid Entry");
                }
                if (selProp > 0) {
                    string selPropStr = choicesDict[selProp];
//                    int selTemp;
//                    Console.WriteLine("\nEnter Temperature: ");
//                    input = Console.ReadLine();
//                    try {
//                        selTemp = Int32.Parse(input);
//                    }
//                    catch {
//                        selTemp = -1;
//                        if (selPropStr != "q")
//                            Console.WriteLine("\tInvalid Entry");
//                    }
//                    if (selTemp > 0) {
                        //double val = selectedMaterial.ReturnValue(selTemp,selPropStr);
                        double val = selectedMaterial.ReturnValue(TEMP,selPropStr);
                        Console.WriteLine("\n----------------------------------------\n");
                        Console.WriteLine("\t{0}: At {1} degF, \n\n\t\t{2} = {3} ksi",
                                            selectedMaterial.materialName,
                                            //selTemp,
                                            TEMP,
                                            selPropStr,
                                            val);
                        Console.WriteLine("\n----------------------------------------\n");
                        menuState = State.MainMenu;
//                }
                }
                else
                    menuState = State.MainMenu;

            }
            
            // Display Results
            else if (menuState == State.DisplayResults)
            {
                Console.Clear();
                Console.WriteLine();

                Console.WriteLine("\tEnter to end");
                // Wait for enter:
                string input = Console.ReadLine();
                quit = true;
            }
        }
    }


    public static void MaterialSelectedMenu()
    {
        Console.WriteLine("");
        Console.WriteLine("========================================");
        Console.WriteLine("");
        Console.WriteLine("\tSelect Property: ");
        Console.WriteLine("");
        Console.WriteLine("========================================");
        Console.WriteLine("");
        
    }

    // print out the menu
    private static void PrintMainMenu(Material[] matArray)
    {
        Console.WriteLine("");
        Console.WriteLine("========================================");
        Console.WriteLine("");

        PrintMaterialOptions(matArray);

        Console.WriteLine("");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("");
        Console.WriteLine("\tTemperature\t{0} degF",TEMP);
        Console.WriteLine("");
        Console.WriteLine("\tEnter 't' to change temperature");
        Console.WriteLine("");
        Console.WriteLine("        Enter 'q' to quit ");
        Console.WriteLine("");
        Console.WriteLine("========================================");
        Console.WriteLine("");
        Console.WriteLine("");

    }

    static void PrintMaterialOptions(Material[] matArray)
    {
        Console.WriteLine("\tSelect Material:");
        Console.WriteLine("");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("");
        
        foreach (Material m in matArray) {
            Console.WriteLine("\t{0}\t{1}", m.materialNumber,m.materialName);
        }
        
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

    public int materialNumber;

    // Dummy constructor
    public Material() {} 

    // Constructor
    public Material(string name, string year, int matNum)
    {
        materialName = name;
        codeYear = year;
        materialNumber = matNum;
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

    // Test to see if each dict is  present, and print dict if they are
    public void PrintValues() {
        if (Sy.Count > 0)
            PrintValuesOfDict(Sy,"Sy");
        if (Su.Count > 0)
            PrintValuesOfDict(Su,"Su");
        if (Sm.Count > 0)
            PrintValuesOfDict(Sm,"Sm");
        if (E.Count > 0)
            PrintValuesOfDict(E,"E");
    }
    
    public void PrintValuesOfDict (Dictionary<int,double> dict, string dictName) {
        Console.Write("\nAvailable Data - {0}:", dictName);
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

    public Dictionary<int,string> ListAvailableProperties () {
        Dictionary<int,string> dict = new Dictionary<int,string>();
        int i = 1;
        string returnStr = "";
        if (Sy.Count > 0)  {
            returnStr = "Sy";
            Console.WriteLine("\t{0}\t{1}",i,returnStr);
            dict[i] = returnStr;
            i++;
        }
        if (Su.Count > 0) {
            returnStr = "Su";
            Console.WriteLine("\t{0}\t{1}",i,returnStr);
            dict[i] = returnStr;
            i++;
        }
        if (Sm.Count > 0) {
            returnStr = "Sm";
            Console.WriteLine("\t{0}\t{1}",i,returnStr);
            dict[i] = returnStr;
            i++;
        }
        if (E.Count > 0) {
            returnStr = "E";
            Console.WriteLine("\t{0}\t{1}",i,returnStr);
            dict[i] = returnStr;
            i++;
        }
        return dict;
    }
}

public enum State 
{
    MainMenu, 
    MaterialSelected,
    PropertyMenu,
    DisplayResults
};
