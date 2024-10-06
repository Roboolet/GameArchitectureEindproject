using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathingSystem
{
    // the scaling used to calculate what node is what index
    // for instance, a factor of 0.5f would make index 1 land on 0.5, and index 2 on 1, etc
    private const float gridScalingFactor = 0.5f;
    private const int gridSizeHori = 50, gridSizeVert = 30;

    private static PathingNode[][][] nodeGrid;

    private static void RefreshNodes()
    {
        int counter = 0;

        // initialize jagged array
        nodeGrid = new PathingNode[gridSizeHori][][];
        for(int x = 0; x < gridSizeHori; x++)
        {
            nodeGrid[x] = new PathingNode[gridSizeVert][];            
        }
        for (int x = 0; x < gridSizeHori; x++)
        {
            for (int y = 0; y < gridSizeVert; y++)
            {
                nodeGrid[x][y] = new PathingNode[gridSizeHori];
            }
        }

        // create nodes
        for(int x = 0; x < gridSizeHori; x++)
        {
            for (int y = 0; y < gridSizeVert; y++)
            {
                for (int z = 0; z < gridSizeHori; z++)
                {
                    Vector3 vec = new Vector3(x*gridScalingFactor, 
                        y*gridScalingFactor, z*gridScalingFactor);

                    // do check here to decide what type of node needs to be created
                    // maybe an overlapbox?

                    nodeGrid[x][y][z] = new PathingNode(PathingNodeType.AIR);
                    counter++;
                }
            }
        }

        Debug.Log("Initialized PathingSystem, created " + counter + " nodes");
    }

    /// <summary>
    /// Returns a copy of the closest PathingNode to the given position
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <returns></returns>
    public static PathingNode GetClosestNode(Vector3 worldPosition)
    {
        if(nodeGrid == null) { RefreshNodes(); }

        // divide position by scaling, then round that number to get the index
        int x = Mathf.RoundToInt(worldPosition.x / gridScalingFactor);
        int y = Mathf.RoundToInt(worldPosition.y / gridScalingFactor);
        int z = Mathf.RoundToInt(worldPosition.z / gridScalingFactor);

        // clamp values that fall outside of grid range
        x = Mathf.Clamp(x, 0, gridSizeHori);
        y = Mathf.Clamp(y, 0, gridSizeVert);
        z = Mathf.Clamp(z, 0, gridSizeHori);

        return nodeGrid[x][y][z];
    }
}

public struct PathingNode
{
    public PathingNodeType type;

    public PathingNode(PathingNodeType _type)
    {
        type = _type;
    }
}

public enum PathingNodeType
{
    AIR, GROUND
}
