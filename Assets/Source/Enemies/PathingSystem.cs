using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PathingSystem
{
    // the scaling used to calculate what node is what index
    // for instance, a factor of 0.5f would make index 1 land on 0.5, and index 2 on 1, etc
    public const float GRID_SCALING_FACTOR = 4f;
    private const int GRID_SIZE_HORIZONTAL = 50, GRID_SIZE_VERTICAL = 10;

    private static PathingNode[][][] nodeGrid;

    private static void RefreshNodes()
    {
        int counter = 0;

        // initialize jagged array
        nodeGrid = new PathingNode[GRID_SIZE_HORIZONTAL][][];
        for(int x = 0; x < GRID_SIZE_HORIZONTAL; x++)
        {
            nodeGrid[x] = new PathingNode[GRID_SIZE_VERTICAL][];            
        }
        for (int x = 0; x < GRID_SIZE_HORIZONTAL; x++)
        {
            for (int y = 0; y < GRID_SIZE_VERTICAL; y++)
            {
                nodeGrid[x][y] = new PathingNode[GRID_SIZE_HORIZONTAL];
            }
        }

        // create nodes
        for(int x = 0; x < GRID_SIZE_HORIZONTAL; x++)
        {
            for (int y = 0; y < GRID_SIZE_VERTICAL; y++)
            {
                for (int z = 0; z < GRID_SIZE_HORIZONTAL; z++)
                {
                    Vector3 vec = new Vector3(x*GRID_SCALING_FACTOR, 
                        y*GRID_SCALING_FACTOR, z*GRID_SCALING_FACTOR);

                    // do check here to decide what type of node needs to be created
                    // maybe an overlapbox?

                    nodeGrid[x][y][z] = new PathingNode(PathingNodeType.AIR, vec);
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
        int x = Mathf.RoundToInt(worldPosition.x / GRID_SCALING_FACTOR);
        int y = Mathf.RoundToInt(worldPosition.y / GRID_SCALING_FACTOR);
        int z = Mathf.RoundToInt(worldPosition.z / GRID_SCALING_FACTOR);

        // clamp values that fall outside of grid range
        x = Mathf.Clamp(x, 0, GRID_SIZE_HORIZONTAL-1);
        y = Mathf.Clamp(y, 0, GRID_SIZE_VERTICAL-1);
        z = Mathf.Clamp(z, 0, GRID_SIZE_HORIZONTAL-1);

        return nodeGrid[x][y][z];
    }
}

public struct PathingNode
{
    public Vector3 position;
    public PathingNodeType type;

    public PathingNode(PathingNodeType _type, Vector3 _position)
    {
        type = _type;
        position = _position;
    }
}

public enum PathingNodeType
{
    AIR, GROUND
}
