using System;

namespace CTest
{
    public class ClassTest
    {
        public static void Main(string[] args)
        {
            WriterClass writer = new WriterClass();

            Console.WriteLine("This is a test");

            writer.WriteStuff("So is this");
        }
    }

    class WriterClass
    {
        public void WriteStuff(string str)
        {
            Console.WriteLine(str);
        }
    }
}
