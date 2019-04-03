using System;
using System.Collections.Generic;
//using System.Math;

public class MatComp
{
    // Weld Group Properties
    static float b = 0;
    static float d = 0;
    static float Pa = 0;
    static float Pb = 0;
    static float Pc = 0;
    static float Ma = 0;
    static float Mb = 0;
    static float Mc = 0;
    static float allowableStress = 0;

    static List<string> acceptInputs = new List<string> {"1","2","3","4","5","6","7","8","9","B","b","C","c","Q","q"};

    //Dims dims = new Dims();

    public static void Main()
    {

        WeldGroup weldGroup = WeldGroup.NoVal;
        // Set to start in Main Menu
        State menuState = State.MainMenu;
        bool quit = false;

        // Loop until 'q' is pressed
        while (!quit)
        {

            // Main Menu
            if (menuState == State.MainMenu)
            {
                Console.Clear();
                PrintMainMenu();

                bool loop = true;
                string input = "";
                while (loop) {
                    input = Console.ReadLine();
                    foreach (string s in acceptInputs) {
                        if (input == s) {
                            loop = false;
                        }
                    }
                    if (loop) {
                        Console.WriteLine("Invalid Input");
                    }
                }

                switch (input)
                {
                    case "1":
                        // Take to Confirm Weld Group Page
                        menuState = State.ConfirmWeldType;
                        weldGroup = WeldGroup.ParallelA; 
                        break;
                    case "2":
                        // Take to Confirm Weld Group Page
                        menuState = State.ConfirmWeldType;
                        weldGroup = WeldGroup.ParallelB; 
                        break;
                    case "3":
                        // Take to Confirm Weld Group Page
                        menuState = State.ConfirmWeldType;
                        weldGroup = WeldGroup.WShapeA; 
                        break;
                    case "4":
                        // Take to Confirm Weld Group Page
                        menuState = State.ConfirmWeldType;
                        weldGroup = WeldGroup.WShapeB; 
                        break;
                    case "5":
                        menuState = State.ConfirmWeldType;
                        weldGroup = WeldGroup.Rectangle; 
                        break;
                    case "q":
                    case "Q":
                        quit = true;
                        continue;
                    default:
                        break;
                }
            } 

            // Enter Properties
            else if (menuState == State.ConfirmWeldType)
            {
                Console.Clear();
                PrintConfirmText();
                switch (weldGroup) {
                    case WeldGroup.ParallelA:
                        PrintDiagramParallelA();                
                        break;
                    case WeldGroup.ParallelB:
                        PrintDiagramParallelB();                
                        break;
                    case WeldGroup.WShapeA:
                        PrintDiagramWShapeA();                
                        break;
                    case WeldGroup.WShapeB:
                        PrintDiagramWShapeB();                
                        break;
                    case WeldGroup.Rectangle:
                        PrintDiagramRectangle();
                        break;
                    default:
                        Console.WriteLine("\n        INVALID SELECTION");
                        break;
                }
                Console.WriteLine("Confirm Weld Type (\"y\")");
                Console.WriteLine("");
                Console.WriteLine("Press any key to return to Main Menu:");
                Console.WriteLine("");
                // Wait for enter:
                string input = Console.ReadLine();
                if (input == "y" || input == "Y")
                    menuState = State.EnterProperties;
                else
                    menuState = State.MainMenu;
            }

            else if (menuState == State.EnterProperties)
            {
                Console.Clear();
                PrintPropertiesMenu(weldGroup);
                // Wait for enter:
                string input = Console.ReadLine();
                
                string val = "";

                bool entering = true;

                while (entering == true)
                {
                    switch (input)
                    {
                        case "1":
                            Console.WriteLine("Enter value for \"b\":  ");
                            val = Console.ReadLine();
                            if (val == "") {
                                entering = false;
                                break;
                            }
                            b = float.Parse(val);
                            break;
                        case "2":
                            Console.WriteLine("Enter value for \"d\":  ");
                            val = Console.ReadLine();
                            if (val == "") {
                                entering = false;
                                break;
                            }
                            d = float.Parse(val);
                            break;
                        case "3":
                            Console.WriteLine("Enter value for \"Pa\":  ");
                            val = Console.ReadLine();
                            if (val == "") {
                                entering = false;
                                break;
                            }
                            Pa = float.Parse(val);
                            break;
                        case "4":
                            Console.WriteLine("Enter value for \"Pb\":  ");
                            val = Console.ReadLine();
                            if (val == "") {
                                entering = false;
                                break;
                            }
                            Pb = float.Parse(val);
                            break;
                        case "5":
                            Console.WriteLine("Enter value for \"Pc\":  ");
                            val = Console.ReadLine();
                            if (val == "") {
                                entering = false;
                                break;
                            }
                            Pc = float.Parse(val);
                            break;
                        case "6":
                            Console.WriteLine("Enter value for \"Ma\":  ");
                            val = Console.ReadLine();
                            if (val == "") {
                                entering = false;
                                break;
                            }
                            Ma = float.Parse(val);
                            break;
                        case "7":
                            Console.WriteLine("Enter value for \"Mb\":  ");
                            val = Console.ReadLine();
                            if (val == "") {
                                entering = false;
                                break;
                            }
                            Mb = float.Parse(val);
                            break;
                        case "8":
                            Console.WriteLine("Enter value for \"Mc\":  ");
                            val = Console.ReadLine();
                            if (val == "") {
                                entering = false;
                                break;
                            }
                            Mc = float.Parse(val);
                            break;
                        case "9":
                            Console.WriteLine("Enter Allowable Stress:  ");
                            val = Console.ReadLine();
                            if (val == "") {
                                entering = false;
                                break;
                            }
                            allowableStress = float.Parse(val);
                            entering = false;
                            break;

                        case "b":
                        case "B":
                            menuState = State.MainMenu;
                            entering = false;
                            break;

                        case "c":
                        case "C":
                            entering = false;
                            if (b <= 0 || d <= 0) {
                                Console.WriteLine("Weld dimensions must be greater than zero");
                                Console.ReadLine();
                                break;
                            }
                            if (allowableStress <= 0) {
                                Console.WriteLine("Allowable Stress must be greater than zero");
                                Console.ReadLine();
                                break;
                            }
                            menuState = State.Calculating;
                            break;
                            
                        case "q":
                        case "Q":
                            quit = true;
                            entering = false;
                            continue;
                        default:
                            Console.ReadLine();
                            entering = false;
                            break;
                    }
                    if (entering == true) {
                        int iInput = int.Parse(input) + 1;
                        input = iInput.ToString();
                    }
                }
            }

            else if (menuState == State.Calculating) 
            {
                Console.Clear();
                PrintCalculatingHeader();
                switch (weldGroup) {
                    case WeldGroup.ParallelA:
                        PerformCalculationsParrallelA();
                        break;
                    case WeldGroup.ParallelB:
                        PerformCalculationsParrallelB();
                        break;
                    case WeldGroup.WShapeA:
                        PerformCalculationsWShapeA();
                        break;
                    case WeldGroup.WShapeB:
                        PerformCalculationsWShapeB();
                        break;
                    case WeldGroup.Rectangle:
                        PerformCalculationsRectangle();
                        break;
                }

                PrintCalculationResults();

                // Wait for enter:
                string input = Console.ReadLine();
                switch (input) {
                    case "":
                        menuState = State.EnterProperties;
                        break;
                    default:
                        // For now, just default to returning to properties
                        // menu
                        menuState = State.EnterProperties;
                        // quit = true;
                        break;
                }
            }
        }
    }

    private static void PrintCalculationResults() {
        Console.WriteLine("");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("");
        float fa = Math.Abs(Pa/Dims.La) + Math.Abs(Mc*Dims.Cb/Dims.PMI);
        // Console.Write("     fa = ");  
        // Console.Write(string.Format("{0.000}",fa));  
        // Console.WriteLine(" lbf/in.");  
        Console.WriteLine("     fa = " + fa.ToString("#.###") + " lbf/in.");  
        float fb = Math.Abs(Pb/Dims.Lb) + Math.Abs(Mc*Dims.Ca/Dims.PMI);
        Console.WriteLine("     fb = " + fb.ToString("#.###") + " lbf/in.");  
        float fc = Math.Abs(Pc/Dims.Lc) + Math.Abs(Ma/Dims.SMa) + Math.Abs(Mb/Dims.SMb);
        Console.WriteLine("     fc = " + fc.ToString("#.###") + " lbf/in.");  
        Console.WriteLine("");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("");
        float fr = (float)Math.Sqrt(fa*fa + fb*fb + fc*fc);
        Console.WriteLine("     fr = " + fr.ToString("#.###") + " lbf/in.");  
        Console.WriteLine("");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("");
        float reqSize = fr / allowableStress;
        Console.WriteLine("     Required Weld Size = " + reqSize.ToString("#.###")  + " in.");  
        Console.WriteLine("");
        Console.WriteLine("========================================"); 
        Console.WriteLine("");
        Console.WriteLine("     Press any key to return");
        Console.WriteLine("");
        Console.WriteLine("========================================"); 
    }

    private static void PerformCalculationsParrallelA() 
    { 
        Dims.La = 2 * d;
        Console.WriteLine("     La = " + Dims.La.ToString("#.###")  + " in.");
        Dims.Lb = 2 * d;
        Console.WriteLine("     Lb = " + Dims.Lb.ToString("#.###")  + " in.");
        Dims.Lc = 2 * d;
        Console.WriteLine("     Lc = " + Dims.Lc.ToString("#.###")  + " in.");
        Dims.Ca = b / 2;
        Console.WriteLine("     Ca = " + Dims.Ca.ToString("#.###")  + " in.");
        Dims.Cb = d / 2;
        Console.WriteLine("     Cb = " + Dims.Cb.ToString("#.###")  + " in.");
        Dims.SMa = d*d / 3;
        Console.WriteLine("     SMa = " + Dims.SMa.ToString("#.###")  + " in.^2");
        Dims.SMb = b * d;
        Console.WriteLine("     SMb = " + Dims.SMb.ToString("#.###")  + " in.^2");
        Dims.PMI = (d/6) * (3*b*b + d*d);
        Console.WriteLine("     PMI = " + Dims.PMI.ToString("#.###")  + " in.^3");  
    }

    private static void PerformCalculationsParrallelB() 
    { 
        Dims.La = 2 * b;
        Console.WriteLine("     La = " + Dims.La.ToString("#.###")  + " in.");
        Dims.Lb = 2 * b;
        Console.WriteLine("     Lb = " + Dims.Lb.ToString("#.###")  + " in.");
        Dims.Lc = 2 * b;
        Console.WriteLine("     Lc = " + Dims.Lc.ToString("#.###")  + " in.");
        Dims.Ca = b / 2;
        Console.WriteLine("     Ca = " + Dims.Ca.ToString("#.###")  + " in.");
        Dims.Cb = d / 2;
        Console.WriteLine("     Cb = " + Dims.Cb.ToString("#.###")  + " in.");
        Dims.SMa = b*d;
        Console.WriteLine("     SMa = " + Dims.SMa.ToString("#.###")  + " in.^2");
        Dims.SMb = b*b / 2;
        Console.WriteLine("     SMb = " + Dims.SMb.ToString("#.###")  + " in.^2");
        Dims.PMI = (b/6) * (3*d*d + b*b);
        Console.WriteLine("     PMI = " + Dims.PMI.ToString("#.###")  + " in.^3");  
    }

    private static void PerformCalculationsWShapeA() 
    { 
        Dims.La = 4 * b;
        Console.WriteLine("     La = " + Dims.La.ToString("#.###")  + " in.");
        Dims.Lb = 2 * d;
        Console.WriteLine("     Lb = " + Dims.Lb.ToString("#.###")  + " in.");
        Dims.Lc = 2 * (2*b + d);
        Console.WriteLine("     Lc = " + Dims.Lc.ToString("#.###")  + " in.");
        Dims.Ca = b / 2;
        Console.WriteLine("     Ca = " + Dims.Ca.ToString("#.###")  + " in.");
        Dims.Cb = d / 2;
        Console.WriteLine("     Cb = " + Dims.Cb.ToString("#.###")  + " in.");
        Dims.SMa = (d/3) * (6*b + d);
        Console.WriteLine("     SMa = " + Dims.SMa.ToString("#.###")  + " in.^2");
        Dims.SMb = 2 * b*b / 3;
        Console.WriteLine("     SMb = " + Dims.SMb.ToString("#.###")  + " in.^2");
        Dims.PMI = (d*d/6) * (6*b + d) + b*b*b/3;
        Console.WriteLine("     PMI = " + Dims.PMI.ToString("#.###")  + " in.^3");  
    }

    private static void PerformCalculationsWShapeB() 
    { 
        Dims.La = 2 * b;
        Console.WriteLine("     La = " + Dims.La.ToString("#.###")  + " in.");
        Dims.Lb = 2 * d;
        Console.WriteLine("     Lb = " + Dims.Lb.ToString("#.###")  + " in.");
        Dims.Lc = 2 * (b + d);
        Console.WriteLine("     Lc = " + Dims.Lc.ToString("#.###")  + " in.");
        Dims.Ca = b / 2;
        Console.WriteLine("     Ca = " + Dims.Ca.ToString("#.###")  + " in.");
        Dims.Cb = d / 2;
        Console.WriteLine("     Cb = " + Dims.Cb.ToString("#.###")  + " in.");
        Dims.SMa = (d/3) * (3*b + d);
        Console.WriteLine("     SMa = " + Dims.SMa.ToString("#.###")  + " in.^2");
        Dims.SMb = b*b / 3;
        Console.WriteLine("     SMb = " + Dims.SMb.ToString("#.###")  + " in.^2");
        Dims.PMI = (d*d/6) * (3*b + d) + b*b*b/6;
        Console.WriteLine("     PMI = " + Dims.PMI.ToString("#.###")  + " in.^3");  
    }

    private static void PerformCalculationsRectangle() 
    { 
        Dims.La = 2 * b;
        Console.WriteLine("     La = " + Dims.La.ToString("#.###")  + " in.");
        Dims.Lb = 2 * d;
        Console.WriteLine("     Lb = " + Dims.Lb.ToString("#.###")  + " in.");
        Dims.Lc = 2 * (b + d);
        Console.WriteLine("     Lc = " + Dims.Lc.ToString("#.###")  + " in.");
        Dims.Ca = b / 2;
        Console.WriteLine("     Ca = " + Dims.Ca.ToString("#.###")  + " in.");
        Dims.Cb = d / 2;
        Console.WriteLine("     Cb = " + Dims.Cb.ToString("#.###")  + " in.");
        Dims.SMa = (d/3) * (3*b + d);
        Console.WriteLine("     SMa = " + Dims.SMa.ToString("#.###")  + " in.^2");
        Dims.SMb = (b/3) * (3*d + b);
        Console.WriteLine("     SMb = " + Dims.SMb.ToString("#.###")  + " in.^2");
        Dims.PMI = (b+d)*(b+d)*(b+d)/6;
        Console.WriteLine("     PMI = " + Dims.PMI.ToString("#.###")  + " in.^3");  
    }
    
    
    // print out the menu
    private static void PrintMainMenu()
    {
        PrintOptions();
    }

    static void PrintOptions()
    {
        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine("========================================");
        Console.WriteLine("");
        Console.WriteLine("Select Weld Group Type:");
        Console.WriteLine("");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("");
        
        // loop through each element type in WeldGroup enum and print
        int i = 0;
        foreach (WeldGroup weldGroup in Enum.GetValues(typeof(WeldGroup)))
        {
            // FIXME using i from zero rather than one since added 'NoVal' value to the WeldGroup
            // enum
            if (i == 0)
            {
                i++;
                continue;
            }
            Console.WriteLine(" " + i + " - " + weldGroup);
            i++;
        }
        
        Console.WriteLine("");
        Console.WriteLine("         Enter 'q' to quit ");
        Console.WriteLine("");
        Console.WriteLine("========================================");
        Console.WriteLine("");

    }

    static void PrintConfirmText() {
        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine("========================================");
        Console.WriteLine("");
        Console.WriteLine("Confirm Weld Group Type:");
        Console.WriteLine("");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("");
    }

    static void PrintCalculatingHeader()
    {
        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine("========================================");
        Console.WriteLine("");
        Console.WriteLine("Calculated Values:");
        Console.WriteLine("");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("");
    }

    static void PrintDiagramParallelA() {
        Console.WriteLine("    < b >            \"b\" axis");
        Console.WriteLine(" ^ |     |            |");
        Console.WriteLine("   |     |            |");
        Console.WriteLine(" d |     |            |____  \"a\" axis");
        Console.WriteLine("   |     |           /");
        Console.WriteLine(" v |     |          /  \"c\" axis");
        Console.WriteLine("");
        Console.WriteLine("========================================");
        Console.WriteLine("");
    }

    static void PrintDiagramParallelB() {
        Console.WriteLine("    <  b  >          \"b\" axis");
        Console.WriteLine("    _______           |");
        Console.WriteLine(" ^                    |");
        Console.WriteLine(" d                    |____  \"a\" axis");
        Console.WriteLine(" v  _______          /");
        Console.WriteLine("                    /  \"c\" axis");
        Console.WriteLine("");
        Console.WriteLine("========================================");
        Console.WriteLine("");
    }
    static void PrintDiagramWShapeA() {
        Console.WriteLine("   <  b  >            ");
        Console.WriteLine("   -------           ");
        Console.WriteLine(" ^ -------           \"b\" axis");   
        Console.WriteLine("     | |              |");
        Console.WriteLine(" d   | |              |");
        Console.WriteLine("     | |              |____  \"a\" axis");
        Console.WriteLine(" v -------           /");
        Console.WriteLine("   -------          /  \"c\" axis");
        Console.WriteLine("");
        Console.WriteLine("========================================");
        Console.WriteLine("");
    }

    static void PrintDiagramWShapeB() {
        Console.WriteLine("   <  b  >            ");
        Console.WriteLine("   _______           \"b\" axis");  
        Console.WriteLine(" ^   | |              |");
        Console.WriteLine("     | |              |");  
        Console.WriteLine(" d   | |              |____  \"a\" axis");   
        Console.WriteLine("     | |             /");
        Console.WriteLine(" v _______          /  \"c\" axis");
        Console.WriteLine("");
        Console.WriteLine("========================================");
        Console.WriteLine("");
    }

    static void PrintDiagramRectangle() {
        Console.WriteLine("   <  b  >            ");
        Console.WriteLine("   _______           \"b\" axis");  
        Console.WriteLine(" ^ |     |            |");
        Console.WriteLine("   |     |            |");  
        Console.WriteLine(" d |     |            |____  \"a\" axis");   
        Console.WriteLine("   |     |           /");
        Console.WriteLine(" v _______          /  \"c\" axis");
        Console.WriteLine("");
        Console.WriteLine("========================================");
        Console.WriteLine("");
    }

    static void PrintPropertiesMenu(WeldGroup wGroup) {
        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine("========================================");
        Console.WriteLine("");
        Console.WriteLine("Enter Weld Group Properties:");
        Console.WriteLine("(Select using numbers 1-9)");
        Console.WriteLine("----------------------------------------");

        switch (wGroup) {
            case WeldGroup.ParallelA:
                PrintDiagramParallelA();
                break;
            case WeldGroup.ParallelB:
                PrintDiagramParallelB();
                break;
            case WeldGroup.WShapeA:
                PrintDiagramWShapeA();
                break;
            case WeldGroup.WShapeB:
                PrintDiagramWShapeB();
                break;
            case WeldGroup.Rectangle:
                PrintDiagramRectangle();
                break;
        }

        Console.WriteLine("     1: b = " + b + " in.;       2: d = " + d + " in.");
        Console.WriteLine("");
        Console.WriteLine("     3: Pa = " + Pa + " kip");
        Console.WriteLine("     4: Pb = " + Pb + " kip");
        Console.WriteLine("     5: Pc = " + Pc + " kip");
        Console.WriteLine("");
        Console.WriteLine("     6: Ma = " + Ma + " kip-in.");
        Console.WriteLine("     7: Mb = " + Mb + " kip-in.");
        Console.WriteLine("     8: Mc = " + Mc + " kip-in.");
        Console.WriteLine("");
        Console.WriteLine("     9: Allowable Stress = " + allowableStress + " ksi");
        Console.WriteLine("");
        Console.WriteLine("     B: Back to Main Menu");
        Console.WriteLine("     C: Perform Calculation");
        Console.WriteLine("     Q: Quit");
        Console.WriteLine("");
        Console.WriteLine("========================================");
        Console.WriteLine("");
    }

}



public enum State 
{
    MainMenu, 
    ConfirmWeldType,
    EnterProperties,
    Calculating
};

public enum WeldGroup 
{
    NoVal,
    ParallelA, 
    ParallelB, 
    WShapeA, 
    WShapeB,
    Rectangle
};

public struct Dims {
    public static float La;
    public static float Lb;
    public static float Lc;
    public static float Ca;
    public static float Cb;
    public static float SMa;
    public static float SMb;
    public static float PMI;
}


