using System;

public class IntRange
{
    static public void Main (string[] args)
    {
        int r = 0;
        if (args.Length > 0)
            r = Convert.ToInt32(args[0]);
        else
            r = 100;
//        int[] x = new int[16];
//        int[] y = new int[16];
        double xd = 0;
        double yd = 0; 
        double theta = 0;
        int number = (int)(2*(double)r*Math.PI);

//        for (int i=0;i<=x.GetLength(0);i++)
        for (int i=0;i<=number;i++)
        {
//            x[i] = (int)r*Math.Cos(theta);
//            y[i] = (int)r*Math.Sin(theta);
            xd = r*Math.Cos(theta);
            yd = r*Math.Sin(theta);
            
//            Console.Write(xd);
//            Console.Write(", ");
//            Console.WriteLine(yd);

            Console.Write((int)xd);
            Console.Write(", ");
            Console.WriteLine((int)yd);
            theta += 2*Math.PI/number;

        }
    }
}
