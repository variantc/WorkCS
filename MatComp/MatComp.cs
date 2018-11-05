using System;
using System.Collections.Generic;

public class MatComp
{
    public static void Main()
    {
        MaterialProperties materialProperties = new MaterialProperties();

        ElementType elementType = ElementType.NoVal;
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
                        elementType = ElementType.Al;
                        menuState = State.EnterMaterialProps;
                        break;

                    case "a":
                    case "A":
                        materialProperties.Print();
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
                materialProperties.AssignElementProportion(elementType);
                menuState = State.MainMenu;
            }
            
            // Display Results
            else if (menuState == State.DisplayResults)
            {
                Console.Clear();
                Console.WriteLine();
                materialProperties.Print();

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
        
        // loop through each element type in ElementType enum and print
        int i = 0;
        foreach (ElementType elementType in Enum.GetValues(typeof(ElementType)))
        {
            // FIXME using i from zero rather than one since added 'NoVal' value to the ElementType
            // enum
            if (i == 0)
            {
                i++;
                continue;
            }
            Console.WriteLine(" " + i + " - " + elementType);
            i++;
        }
        
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

public class MaterialProperties
{
    public Dictionary<ElementType,int[]> materialProperties = new Dictionary<ElementType,int[]>();
    private int length = 0;

    public int Length
    {
        get { return this.length; }
    }

    public void AssignElementProportion(ElementType elementType)
    {
        // Check for value
        MatComp.EnterMaterialPropsMenu("minimum");
        int min = int.Parse(Console.ReadLine());
        MatComp.EnterMaterialPropsMenu("maximum");
        int max = int.Parse(Console.ReadLine());
        AddElementPropery(elementType,min,max);
    }

    public void AddElementPropery (ElementType elemType, int min, int max)
    {
        int[] range = new int[2];
        range[0] = min;
        range[1] = max;
        materialProperties.Add(elemType,range);
        length++;
    }

    public void Print ()
    {
        Console.WriteLine(materialProperties[ElementType.Al][0] 
                        + " "
                        + materialProperties[ElementType.Al][1]);
    }
}

public enum State 
{
    MainMenu, 
    EnterMaterialProps,
    DisplayResults
};

// Enumerated list of element types
public enum ElementType 
{
    NoVal,
    Al,
    C,
    Ca,
    Cb,
    Cr,
    Cu,
    Mn,
    Mo,
    N,
    Ni,
    P,
    S,
    Se,
    Si,
    Ti,
    V,
    W
};

