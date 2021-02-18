using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    Main main;
    Functions functions;
    Movement movement;
    PathMovement pathMovement;
    public List<Node> pathfinder;
    public bool hasFoundPath;

    private void Awake()
    {
        main = GetComponent<Main>();
        functions = GetComponent<Functions>();
        movement = GetComponent<Movement>();
        pathMovement = GetComponent<PathMovement>();
    }
    public void FindPath(Vector3Int startPos, Vector3Int endPos)
    {
        // try clearing pathfinder if have issues
        hasFoundPath = false;
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
                hasFoundPath = true;
                RetracePath(startNode, endNode);
                pathMovement.tempPos = startPos;
                pathMovement.index = 0;
                pathMovement.MoveCube();
                Debug.Log("PATH HAS BEEN FOUND");
            }

            foreach (Node neighbor in functions.GetNeighbors(currentNode))
            {
                if (closedSet.Contains(neighbor))
                {
                    continue;
                }

                int newCost = currentNode.gCost + CalculateCost(currentNode, neighbor);
                
                if (newCost < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = newCost;
                    neighbor.hCost = CalculateCost(neighbor, endNode);
                    neighbor.parent = currentNode;

                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }

        if (!hasFoundPath)
        {
            Debug.Log("PATH NOT FOUND");
            movement.isPathfinding = false;
            main.grid[startPos.x, startPos.y, startPos.z].walkable = false;

            // make node position of currentMorphBot unwalkable
            // make it so that you pathfind again (enable the necessary scripts)
            // possibly display a message.
        }
    }

    private int CalculateCost(Node nodeA, Node nodeB)
    {
        Vector3Int posA = nodeA.position;
        Vector3Int posB = nodeB.position;

        int xCost = Mathf.Abs(posB.x - posA.x);
        int yCost = Mathf.Abs(posB.y - posA.y);
        int zCost = Mathf.Abs(posB.z - posA.z);

        return xCost + yCost + zCost;
    }

    private void RetracePath(Node start, Node end)
    {
        List<Node> path = new List<Node>();
        Node current = end;

        while (current != start)
        {
            path.Add(current);
            current = current.parent;
        }

        path.Reverse();
        pathfinder = path;
    }
}
