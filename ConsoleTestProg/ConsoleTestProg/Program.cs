using System;

namespace ConsoleTestProg
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello World!");
			char test = (char)Console.ReadKey ();
			if (test == 'a') 
			{
				Console.WriteLine ("bang");
			}
		}
	}
}
