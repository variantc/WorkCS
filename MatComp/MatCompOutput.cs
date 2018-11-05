using System;
using System.Collections.Generic;

public class MatCompOutput
{
    public static void Main ()
    {
        Dictionary<ElementType,int[]> materialProperties = new Dictionary<ElementType,int[]>();
        int[] ar1 = {8, 10};
        int[] ar2 = {1, 1};
        materialProperties.Add(ElementType.Cr,ar1);
        materialProperties.Add(ElementType.Mo,ar2);

        MaterialGroup group;

        try 
        {
            if (materialProperties[ElementType.Cr][0] => 18 
                && materialProperties[ElementType.Cr][1] <= 29)
            {
                group = MaterialGroup.H; 
            }
        } catch { }
        try 
        {
            Console.WriteLine(materialProperties[ElementType.V]);
        } catch { }
    }
}

// Maybe read from file which contains each definition and the corresponding material group

// Assign to a dictionary?

public enum ElementType
{
    Al,
    B,
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

public enum MaterialGroup { A, B, C, D, E, F, G, H }
