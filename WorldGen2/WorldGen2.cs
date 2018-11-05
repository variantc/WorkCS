using System;

public class WorldGen2
{
    public static void Main (string[] args) 
    {
        // Declarations
        int width;
        int height;

        // Factors for determining the size of the radii of placement based
        // upon the defined width and height
        const float LARGE_FACT = 0.19f;
        const float SMALL_FACT = 0.1f;

        // The amount of times to apply the placements based on defind dimensions
        const int RAISE_LARGE_LOOPS_FACT = 1;
        const int LOWER_SMALL_LOOPS_FACT = 2;
        const int LOWER_SINGLE_LOOPS_FACT = 2;
        
        // Divosor for number of rivers to place
        const int RIVER_DIVISOR = 5;

        // create dimensions array to store return value ([width,height] from 
        // SetWidthAndheight function and then assign
        int[] dimensions = new int[2];
        dimensions = SetWidthAndheight(args); 
        {
            width = dimensions[0];     
            height = dimensions[1];
        }

        int avgDim = (height + width) / 2;
        // calculate the radii from average of height and width divided by inverse 
        // factors defined above
        int largeRad = (int)((float)avgDim * LARGE_FACT);
        int smallRad = (int)((float)avgDim * SMALL_FACT);

        int numOfRivers = avgDim / RIVER_DIVISOR;
        Console.WriteLine("num of rivers: " + numOfRivers);

        //========================================================================

        // Operation
        Map map = new Map (width, height);

        map.EvolveMap(1,RAISE_LARGE_LOOPS_FACT * avgDim,largeRad);
        map.EvolveMap(-1,LOWER_SMALL_LOOPS_FACT * avgDim,smallRad);
        map.EvolveMap(0,LOWER_SINGLE_LOOPS_FACT * avgDim,1);

        map.ConvertMap();
        map.PrintCharMap();
        map.PrintMap();

        map.RiverFunc(numOfRivers);
        map.PrintCharMap();

    }
    
    // Use command line args to set width and height or if none provided, prompt 
    // and store from user
    static int[] SetWidthAndheight(string[] args)
    {

        int[] dims = new int[2];

        // if no arguments passed into program, prompt, otherwise set to width and
        // height
        if (args.Length == 0) 
        {
            // Prompt for user-inputted dimensions
            Console.WriteLine("Enter width: ");
            dims[0] = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter height: ");
            dims[1] = int.Parse(Console.ReadLine());
        }
        else if (args.Length != 0) 
        {
            if (args.Length == 1) 
            {
                dims[0] = dims[1] = int.Parse(args[0]);
            }
            if (args.Length == 2) 
            {
                dims[0] = int.Parse(args[0]);
                dims[1] = int.Parse(args[1]);
            }
        }
        
        return dims;
    }
}

public class Map
{
    // Add get and set?
    int width;
    int height;
    int [,] map;
    char [,] charMap;
    Random r = new Random();

    // chars for drawing map
    char charWater = ' ';
    char charPlains = '\u25FB';
    char charHills = '\u25B4';
    char charMountains = '\u26F0';
    char charRiver = '~';
//    char charWater = '~';
//    char charPlains = '\u25FB';
//    char charHills = '\u25B4';
//    char charMountains = '\u26F0';
//    char charRiver = '\u25A0';

    // Constructor which assigns width and height and initialises array based
    // on these values
    public Map (int _width, int _height)
    {
        this.width = _width;
        this.height = _height;
        map = new int[width,height];
        GenerateMap(0);
    }

    // Find the value of the highest points in generated map
    public int FindMaxAlt() 
    {
        int max = 0;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (max < map[j,i])
                {
                    max = map[j,i];
                }
            }
        }
        return max;
    }

    // TODO: Improve - 
    // Randomly selects a site anywhere on the array, if this
    // site is equal to maxAlt, then returns this site location
    public int[] FindRandMaxAltLoc(int maxAlt)
    {
        int x;
        int y;
        int[] siteLoc = new int[2];
        bool foundSite = false;

        // loop until foundSite is true
        while (!foundSite)
        {
            x = r.Next (0, width);
            y = r.Next (0, height);

            if (map[x,y] == maxAlt)
            {
                siteLoc[0] = x;
                siteLoc[1] = y;
                foundSite = true;
            }
        }
        return siteLoc;
    }

    
    // Collect river placement
    public void RiverFunc(int numRivers) 
    {
        // FIXME: The problem is that the highest point is found using
        // FindRandMaxAltLoc, however, this is not stored, so the same locations
        // are used over and over
        for (int i=0; i<numRivers; i++)
        {
            // Find the highest altitude generated
            int maxAlt = this.FindMaxAlt();

            // Pick one of these highest points
            int [] site = FindRandMaxAltLoc(maxAlt);
            Console.WriteLine(site[0]);
            Console.WriteLine(site[1]);
            PlaceRiver(site);
            Console.WriteLine("Placed river");
        }
    }

    public void PlaceRiver (int[] startLoc)
    {
        int [] newLoc = startLoc;
        int [] newerLoc = {-1,-1};
//        Console.WriteLine("newLoc: " + newLoc[0] + "," + newLoc[1]);
        // GrowRiver returns the argument int array if no new valid river placement, thus
        // run until newLoc == newerLoc
        while (newLoc != newerLoc)
        {
//            Console.WriteLine("now, newerLoc: " + newerLoc[0] + "," + newerLoc[1]);
            newerLoc = newLoc;
            newLoc = GrowRiver(newerLoc);
//            Console.WriteLine("now, newLoc: " + newLoc[0] + "," + newLoc[1]);
//            Console.WriteLine("=============================");
        }
//        Console.WriteLine("Finished river");
    }

    // Return the next location river 'grows' to, otherwise, return same location
    public int[] GrowRiver (int[] startLoc)
    {
        int x = startLoc[0];
        int y = startLoc[1];
        int alt = map[x,y];
        int lowest = alt;
        int [] lowestLoc = new int[2];
        bool foundLoc = false;

//        Console.WriteLine("Here");
        
        // Check that startLoc gave values in array map
        if ((x < 0) || (x > width) || (y < 0) || (y > height))
        {
//            Console.WriteLine("OOB1");
        }

        // look in adjacent cells, find lowest value
        for (int i=-1; i<2; i++)
        {
//            Console.WriteLine("i= " + i + " x= " + x);
            for (int j=-1; j<2; j++)
            {
//                Console.WriteLine("j= " + j + " y= " + y);
                // Ignore the central point - highest loc
                if ((i == 0) && (j == 0))
                {
                    continue;
                }
                // Check in bounds of array
                if ((x+i < 0) || (x+i > width - 1) || (y+j < 0) || (y+j > height - 1))
                {
//                    Console.WriteLine("OOB");
                    continue;
                }
                if (map[x+i,y+j] < lowest)
                {
                    lowest = map[x+i,y+j];
                    lowestLoc[0] = x+i;
                    lowestLoc[1] = y+j;
                    foundLoc = true;
                }
            }
        }
//        Console.WriteLine("DoneLoop");
       
        // use counter to escape from infinite loop
        int escapeCounter = 0;
        int escapeLimit = 25;

        // if no lower value choose equal value at random
        while (!foundLoc)
        {
//            Console.WriteLine("Now Here");
            // random int -1 to 1 and add to both x and y
            int newX = x + r.Next(-1,2);
            int newY = y + r.Next(-1,2);

            // Break from loop if too many unsucessful loops
            if (escapeCounter > escapeLimit)
            {
                break;
            }

            // ignore +0x,+0y
            if ((newX == x) && (newY == y))
            {
//                Console.WriteLine("Randomly chosen centre");
                escapeCounter++;
//                Console.WriteLine("Incrementing escapeCounter: " + escapeCounter);
                continue;
            }
            // Check in bounds of array
            if ((newX < 0) || (newX > width - 1) || (newY < 0) || (newY > height - 1))
            {
//                Console.WriteLine("Randomly out of bounds");
                escapeCounter++;
//                Console.WriteLine("Incrementing escapeCounter: " + escapeCounter);
                continue;
            }

            // if this map value is equal to alt, make lowest loc
            if (map[newX,newY] == alt)
            {
                lowestLoc[0] = newX;
                lowestLoc[1] = newY;
                foundLoc = true;
//                Console.WriteLine("Found location");
            }
        }
       
        // Check if tile is water or out of bounds
        if (charMap[lowestLoc[0],lowestLoc[1]] == charWater)
        {
            // return startLoc to break loop in PlaceRiver
            return startLoc;
        }
        if (lowestLoc[0] == 0 ||
            lowestLoc[0] == width ||
            lowestLoc[1] == 0 ||
            lowestLoc[1] == height)
        {
            // return startLoc to break loop in PlaceRiver
            return startLoc;
        }
        else {
            // if the location is not already a river (to stop infinite loop between
            // two river tiles) return startLoc to exit loop in PlaceRiver
            if (charMap[lowestLoc[0],lowestLoc[1]] == charRiver)
            {
                return startLoc;
            }
            else {
                // change char to river representation
                charMap[lowestLoc[0],lowestLoc[1]] = charRiver;
                // return lowestLoc to continue loop in PlaceRiver until value is same 
                // as water level or loc is out of bounds (if statements, above)
                return lowestLoc;
            }
        }
        
    }
   
    // Generate initial map array from width and height
    public void GenerateMap(int val) 
    {
        // Loop through and initialise each element to be zero?
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                map[j,i] = val;
            }
        }
    }

    // Adds changeVal to a random element
    public void EvolveMap(int changeVal, int loops, int rad)
    {
        // if changeVal is passed a value of zero, that means a random int -1 to 1
        // create bool to keep track of random change requested to keep random if many
        // loops involved
        bool randChange = false;


        // radius of 1 means no 'extra' elements considered 
        for (int i=0; i<loops; i++)
        {
            // random value since 0 entered as arg.  Considering bias to raise?
            if (changeVal == 0) 
            {
                changeVal = r.Next (-1,2+1);
                randChange = true;
            }

            // to avoid going out of bounds, limit random number using rad
            //int yRand = r.Next (0+(rad-1), width-(rad-1));
            //int xRand = r.Next (0+(rad-1), height-(rad-1));
            int yRand = r.Next (0, width);
            int xRand = r.Next (0, height);

            // maybe include negative value?

            // loop through and add to all elements within rad
            // TODO: change iteration values to be consistent and clearer
            for (int j=0; j<width; j++)
            {
                for (int k=0; k<height; k++)
                {
                    int a = yRand - j;
                    int b = xRand - k;

                    if ((a*a + b*b) < (rad * rad))
                    {
                        map[j,k] += changeVal;
                        // temp: don't let go negative or above 9
                        if (map[j,k] < 0)
                            map[j,k] = 0;
                        if (map[j,k] > 9)
                            map[j,k] = 9;
                    }
                }
            }
            // if random change requested, reset changeVal for next cycle
            if (randChange == true)
            {
                changeVal = 0;
            }
        }
    }

    // Convert int array to char array based upon defined limit levels
    public void ConvertMap ()
    {
        const int LIMIT_1 = 1;
        const int LIMIT_2 = 5;

        charMap = new char[width,height];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (map[j,i] < LIMIT_1)
                    charMap[j,i] = charWater;
                else if (map[j,i] < LIMIT_2-1)
                    charMap[j,i] = charPlains;
                else if (map[j,i] < LIMIT_2+1)
                    charMap[j,i] = charHills;
                else if (map[j,i] >= LIMIT_2+1)
                    charMap[j,i] = charMountains;
            }
        }
    }

    public void PrintCharMap()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Console.Write(charMap[j,i] + " "); 
            }
            Console.WriteLine("");
        }
    }

    // Loop through and print array
    public void PrintMap ()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Console.Write(map[j,i] + ","); 
            }
            Console.WriteLine("");
        }
        
    }

}
