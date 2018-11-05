using System;
using System.IO;

class ReadFile
{
    public static void Main()
    {
        StreamReader reader = new StreamReader("matData.dat");
        
        while (reader.EndOfStream == false)
        {
            string line = reader.ReadLine();
            Console.WriteLine("Material Class: " + line[0]);
            try {
                float quantityA = (float)line[1];
            Console.WriteLine(quantityA);
            }
            catch {}
            try {
                char quantityA = line[1];
            Console.WriteLine(quantityA);
            }
            catch {}
        }
        reader.Close();
    }
}
