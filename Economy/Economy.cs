using System;

// Resource Type Definitions
public enum ResourceType { Null, Wood, Food }

// Main Class Economy
public class Economy
{
    static public void Main (string[] args)
    {
        Node n = new Node(ResourceType.Null, ResourceType.Wood,new float[]{3.2f,0.2f});
        n.TestFunc();
        Resource r = new Resource(n.outputResource, n);
        Console.WriteLine(r.position);
    }
}

// -----------------------------------------------------------------
// Classes
// -----------------------------------------------------------------
public class Node 
{
    private float[] position {get; set;}
    private ResourceType inputResource {get; set;} 
    private ResourceType outputResource {get; set;}
    
    public Node(ResourceType input, ResourceType output, float[] pos) {
        this.inputResource = input;
        this.outputResource = output;
        this.position = pos;
    }

    public void TestFunc() {
        //Console.WriteLine(ResourceType.Wood.ToString());
        Console.WriteLine(position[0].ToString()+position[1].ToString()+outputResource.ToString());
    }
}

public class Resource 
{
    public float[] position {get; protected set;}
    Node sourceNode;
    Node sinkNode;
    private ResourceType resourceType {get; set;} 

    public Resource (ResourceType resource, Node source) {
        this.sourceNode = source;
        this.position = source.position;
    }
}
