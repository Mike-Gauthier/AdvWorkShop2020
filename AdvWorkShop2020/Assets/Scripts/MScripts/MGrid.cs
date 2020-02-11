using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGrid : MonoBehaviour
{
    public Transform player;
    public LayerMask unWalkable; //establishes unwalkable ground in grid
    public Vector2 gridWorldSize; 
    public float nodeRadius;
    Node[,] mGrid; //establishes 2D grid, Z will be written as y

    float nodeDiameter;
    int gridSizeX;
    int gridSizeY;

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt (gridWorldSize.x / nodeDiameter); //converts float and vector 2 values
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter); //converts float and vector 2 values

        CreateGrid();
    }

    void CreateGrid()
    {
        mGrid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward *gridWorldSize.y/2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool isWalkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unWalkable));
                mGrid[x, y] = new Node(isWalkable, worldPoint);
            }
        }

    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return mGrid[x,y];
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if(mGrid!= null)
        {
            Node playerNode = NodeFromWorldPoint(player.position);
            foreach (Node n in mGrid)
            {
                Gizmos.color = (n.isWalkable) ? Color.white : Color.red;
                if (playerNode == n)
                {
                    Gizmos.color = Color.cyan;
                }
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }

    
}
