using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    Main main;

    private void Awake()
    {
        main = GetComponent<Main>();
    }
    public void FindPath(Vector3Int startPos, Vector3Int endPos)
    {
        Node startNode = main.grid[startPos.x, startPos.y, startPos.z];
        Node endNode = main.grid[endPos.x, endPos.y, endPos.z];

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();

        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];

            for (int i = 1;  i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || (openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost))
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == endNode)
            {
                return;
            }
        }
    }
}
