using System;

public class Hello
{
    static void Main (string[] args)
    {
        int a = 2;
        int b = 3;
        Console.Write("Enter logon : ");
        Console.Write("Logging on user {0}\n", Console.ReadLine());
        Console.WriteLine("Initialising....\n\n=====================\nRunning....\nFinding Solution....\nDone\n\n.");
        // To test using numbers in the WriteLine function
        Console.WriteLine("What happens here: " + a + b);
        Console.WriteLine("What happens here: " + (a + b));
        Console.WriteLine(a + b);
    }
}
