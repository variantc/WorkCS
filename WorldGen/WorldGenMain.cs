using System;
using System.Collections.Generic;

//public class Map : Array
//{
//    public char[,] map;
//
//    public char[,](int height, int width)
//    {
//        this.map = new char[height,width];
//    }
//}

public class WorldGenMain
{
    static public void Main ()
    {
        int height = 100;
        int width = 200;
        int numLandSeeds = 30;
        int numWoodSeeds = 6;
        int growthLength = 32;
        int woodGrowthLength = 8;
        char landChar = '.';
        char spaceChar = ' ';
        char woodChar = '*';
//        char woodChar = '\u25B5';

        char[,] map = new char[height,width];
//        char[,] map = new char[,](height,width);
        int[,] seeds = new int[numLandSeeds,2];
        int[] seedCoord = new int[2];

        Random rnd = new Random();
        
        // Assign ranges of control variables
        numLandSeeds = rnd.Next((height+width)/30,(height+width)/10);
        numWoodSeeds = rnd.Next((height+width)/100,(height+width)/20);
        growthLength = rnd.Next(40,60)-numLandSeeds;
        woodGrowthLength = 16;

        InitialiseBlankMap(map,spaceChar);

        for (int i=0;i<numLandSeeds;i++)
        {
            seedCoord = AddLandSeeds(map,rnd,landChar);
            AddLandSeeds(map,rnd,landChar);
//            seeds[i,0] = seedCoord[0];
//            seeds[i,1] = seedCoord[1];
        }
        DisplayMap (map);

//        GrowLandSeeds(map,rnd,seeds,growthLength);
        for (int i=0;i<growthLength;i++)
        {
            ExpandSeeds(map,rnd,spaceChar,landChar);
        }
        DisplayMap (map);

//        for (int i=0;i<numLandSeeds;i++)
//        {
//            Console.Write(seeds[i,0]);
//            Console.Write(",");
//            Console.WriteLine(seeds[i,1]);
//        }

        for (int i=0;i<numWoodSeeds;i++)
        {
            AddWoodSeed(map,rnd,woodChar,landChar);
        }
        DisplayMap (map);

        for (int i=0;i<woodGrowthLength;i++)
        {
            ExpandWood(map,rnd,woodChar,landChar);
        }
        
        DisplayMap (map);

        int[] searchLocation = new int[2] {height/2, width/2};
        List<char> resourceList = new List<char>();

        resourceList = (IntRange(map,searchLocation,25,spaceChar));
        foreach(char res in resourceList)
        {
            Console.Write(res);
        }
        Console.WriteLine("");
        Console.WriteLine(CountResources(resourceList,landChar));
        Console.WriteLine("");
        Console.WriteLine(CountResources(resourceList,woodChar));
    }

    static void InitialiseBlankMap (char[,] m, char space)
    {
        int i;
        int j;
        for (i=0;i<m.GetLength(0);i++)
        {
            for (j=0;j<m.GetLength(1);j++)
            {
                m[i,j]= space;
            }
        }
    }

    static int[] AddLandSeeds (char[,] m, Random r, char land)
    {
        int h;
        int w;
        int[] seedLoc = new int[2];

        h = r.Next(0,m.GetLength(0));
        w = r.Next(0,m.GetLength(1));
        seedLoc[0] = h;
        seedLoc[1] = w;

        // m[h,w]='#';
        m[seedLoc[0],seedLoc[1]]=land;
        
        return seedLoc;
    }

    static void ExpandSeeds (char[,] m, Random r, char space, char land)
    {

        for (int i=0;i<m.GetLength(0);i++)
        {
            for (int j=0;j<m.GetLength(1);j++)
            {
                if (m[i,j] == space)
                {
                    continue;
                }

                int chance = r.Next(0,4);

                switch(chance)
                {
                    case 0:
                        if (i==0)
                        {
                            break;
                        }
                        m[i-1,j] = land;
                        break;
                    case 1:
                        if (j==0)
                        {
                            break;
                        }
                        m[i,j-1] = land;
                        break;
                    case 2:
                        if (i == m.GetLength(0) - 1)
                        {
                            break;
                        }
                        m[i+1,j] = land;
                        break;
                    case 3:
                        if (j == m.GetLength(1) - 1)
                        {
                            break;
                        }
                        m[i,j+1] = land;
                        break;
                }
                        
            }
        }
    }

    static void GrowLandSeeds (char[,] m, Random r, int[,] seedList, int growth)
    {
        for (int i=0;i<seedList.GetLength(0)-1;i++)
        {
            for (int j=0;j<growth;j++)
            {
                int chance = r.Next(0,10);
                int vert = 0;
                int horiz = 0;
            
                int x;
                int y;

                if (chance == 0) break;

                switch(chance)
                {
                    case 1:
                        if (seedList[i,0]==0) 
                        {
                            break;
                        }
                        x = seedList[i,0] - 1;
                        x = seedList[i,0] - 1;
                        m[seedList[i,0]-1,seedList[i,1]] = '#';
                        break;
                    case 2:
                        if (seedList[i,1]==0) 
                        {
                            break;
                        }
                        m[seedList[i,0],seedList[i,1]-1] = '#';
                        break;
                    case 3:
                        if (seedList[i,0]>=m.GetLength(0)) 
                        {
                            break;
                        }
                        m[seedList[i,0]+1,seedList[i,1]] = '#';
                        break;
                    case 4:
                        if (seedList[i,1]>=m.GetLength(0)) 
                        {
                            break;
                        }
                        m[seedList[i,0],seedList[i,1]+1] = '#';
                        break;
                    case 5:
                        if (seedList[i,0]<2) 
                        {
                            break;
                        }
                        m[seedList[i,0]-1,seedList[i,1]] = '#';
                        m[seedList[i,0]-2,seedList[i,1]] = '#';
                        break;
                    case 6:
                        if (seedList[i,1]<2) 
                        {
                            break;
                        }
                        m[seedList[i,0],seedList[i,1]-1] = '#';
                        m[seedList[i,0],seedList[i,1]-2] = '#';
                        break;
                    case 7:
                        if (seedList[i,0]>=m.GetLength(0)-1) 
                        {
                            break;
                        }
                        m[seedList[i,0]+1,seedList[i,1]] = '#';
                        m[seedList[i,0]+2,seedList[i,1]] = '#';
                        break;
                    case 8:
                        if (seedList[i,0]>=m.GetLength(1)-1) 
                        {
                            break;
                        }
                        m[seedList[i,0],seedList[i,1]+1] = '#';
                        m[seedList[i,0],seedList[i,1]+1] = '#';
                        break;
                    case 9:
                        continue;
                }
            }

        }

    }

    static void InitialisearMap (char[,] m, Random r)
    {
        int i;
        int j;
        for (i=0;i<m.GetLength(0);i++)
        {
            for (j=0;j<m.GetLength(1);j++)
            {
                int num = r.Next(0,2);
                if (num == 1)
                {
                    m[i,j] = '#';
                }
                
                if (num == 0)
                {
                    m[i,j]=' ';
                }
            }
        }
    }

    static void DisplayMap (char[,] m)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        int i;
        int j;
        for (i=0;i<m.GetLength(0);i++)
        {
            for (j=0;j<m.GetLength(1);j++)
            {
                Console.Write(m[i,j]);
//                Console.Write(" ");
            }
            Console.WriteLine("");
        }
        Console.WriteLine("");

    }

    static char[,] EvolveMap (char[,] m)
    {
        int i = 1;
        int j = 1;

        for (i=1;i<m.GetLength(0);i++)
        {
            for (j=1;j<m.GetLength(1);j++)
            {
                if (m[i,j] == '#') break;

                if ((m[i,j-1]=='#' || m[i-1,j]=='#' || (m[i-1,j]=='#' || m[i,j+1]=='#') || 
                            (m[i,j+1]=='#' || m[i+1,j]=='#') || (m[i+1,j]=='#' || m[i,j-1]=='#')))
                {
                    Console.WriteLine(m[i,j]);
                    m[i,j] = '#';
                    Console.Write("Changing ");Console.Write(i);
                    Console.Write(" ");Console.WriteLine(j);
                }
            }
        }
        return m;
    }

    static void AddWoodSeed (char[,] m, Random r, char wood, char land)
    {
        int h;
        int w;
        int[] seedLoc = new int[2];

        h = r.Next(0,m.GetLength(0));
        w = r.Next(0,m.GetLength(1));

        if (m[h,w]==land)
        {
            m[h,w]=wood;
        }
    }

   static void ExpandWood (char[,] m, Random r, char wood, char land)
    {

        for (int i=0;i<m.GetLength(0);i++)
        {
            for (int j=0;j<m.GetLength(1);j++)
            {
                if (m[i,j] == land)
                {
                    continue;
                }

                if (m[i,j] == wood)
                {
                    int chance = r.Next(0,4);

                    switch(chance)
                    {
                        case 0:
                            if (i==0)
                            {
                                break;
                            }
                            if (m[i-1,j] == land)
                            {
                                m[i-1,j] = wood;
                            }
                            break;
                        case 1:
                            if (j==0)
                            {
                                break;
                            }
                            if (m[i,j-1] == land)
                            {
                                m[i,j-1] = wood;
                            }
                            break;
                        case 2:
                            if (i == m.GetLength(0) - 1)
                            {
                                break;
                            }
                            if (m[i+1,j] == land)
                            {
                                m[i+1,j] = wood;
                            }
                            break;
                        case 3:
                            if (j == m.GetLength(1) - 1)
                            {
                                break;
                            }
                            if (m[i,j+1] == land)
                            {
                                m[i,j+1] = wood;
                            }
                            break;
                    }
                        
                }
            }
        }
    }

    static List<char> IntRange (char[,] m, int[] loc, int range, char space)
    {

        List<char> resources = new List<char>();

        for (int i=0; i<range; i++)
        {
            for (int j=0; j<range-1; j++)
            {
                if (Math.Pow(j,2)+Math.Pow(i,2) > Math.Pow(range,2))
                    continue;
                if (m[loc[0]+i,loc[1]+j] == space)
                    continue;
                resources.Add(m[loc[0]+i,loc[1]+j]);
            }
            
        }
        return resources;
    }

    static int CountResources (List<char> resources, char resType)
    {
        int count = 0;
        for (int i=0; i<resources.Count; i++)
        {
            if (resources[i] == resType)
                count += 1;
        }
        return count;
    }


}










