using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Functions : MonoBehaviour

{
    Main main;
    Rulesets rulesets;

    private void Awake()
    {
        main = GetComponent<Main>();
        rulesets = GetComponent<Rulesets>();
    }

    public Vector3Int Vector3ToGrid(RaycastHit raycastHit)
    {
        Vector3 rawPosition = raycastHit.point - raycastHit.transform.position;
        Vector3 truePosition = new Vector3(0, 0, 0);

        if (rawPosition.x > 0)
        {
            truePosition.x = Mathf.Floor(rawPosition.x + 0.5f);
        }

        else if (rawPosition.x < 0)
        {
            truePosition.x = Mathf.Floor(rawPosition.x * -1 + 0.5f) * -1;
        }

        if (rawPosition.y > 0)
        {
            truePosition.y = Mathf.Floor(rawPosition.y + 0.5f);
        }

        else if (rawPosition.y < 0)
        {
            truePosition.y = Mathf.Floor(rawPosition.y * -1 + 0.5f) * -1;
        }

        if (rawPosition.z > 0)
        {
            truePosition.z = Mathf.Floor(rawPosition.z + 0.5f);
        }

        else if (rawPosition.z < 0)
        {
            truePosition.z = Mathf.Floor(rawPosition.z * -1 + 0.5f) * -1;
        }

        truePosition += raycastHit.transform.position;
        return Vector3Int.RoundToInt(truePosition);
    }

    public List<Node> GetNeighbors(Node currentNode)
    {
        List<Node> Neighbors = new List<Node>();
        int x = currentNode.position.x;
        int y = currentNode.position.y;
        int z = currentNode.position.z;

        if (rulesets.WithinArray(currentNode.position + Vector3Int.right) && main.grid[x + 1, y, z].walkable == true)
        {
            if (EmptyCheckX(true, currentNode.position))
            {
                Neighbors.Add(main.grid[x + 1, y, z]);
            }
        }

        if (rulesets.WithinArray(currentNode.position + Vector3Int.left) && main.grid[x - 1, y, z].walkable == true)
        {
            if (EmptyCheckX(false, currentNode.position))
            {
                Neighbors.Add(main.grid[x - 1, y, z]);
            }
        }

        if (rulesets.WithinArray(currentNode.position + Vector3Int.RoundToInt(Vector3.forward)) && main.grid[x, y, z + 1].walkable == true)
        {
            if (EmptyCheckZ(true, currentNode.position))
            {
                Neighbors.Add(main.grid[x, y, z + 1]);
            }
        }

        if (rulesets.WithinArray(currentNode.position + Vector3Int.RoundToInt(Vector3.back)) && main.grid[x, y, z - 1].walkable == true)
        {
            if (EmptyCheckZ(false, currentNode.position))
            {
                Neighbors.Add(main.grid[x, y, z - 1]);
            }
        }

        if (rulesets.WithinArray(currentNode.position + Vector3Int.up) && main.grid[x, y + 1, z].walkable == true)
        {
            if (EmptyCheckY(true, currentNode.position))
            {
                Neighbors.Add(main.grid[x, y + 1, z]);
            }
        }

        if (rulesets.WithinArray(currentNode.position + Vector3Int.down) && main.grid[x, y - 1, z].walkable == true)
        {
            if (EmptyCheckY(false, currentNode.position))
            {
                Neighbors.Add(main.grid[x, y - 1, z]);
            }
        }

        return Neighbors;
    }

    public bool EmptyCheckX(bool isRight, Vector3Int pos)
    {
        int sign;

        if (isRight)
        {
            sign = 1;
        }

        else
        {
            sign = -1;
        }

        if (rulesets.WithinArray(pos + Vector3Int.up) && main.grid[pos.x, pos.y + 1, pos.z].walkable == false)
        {
            if (rulesets.WithinArray(pos + Vector3Int.up + new Vector3Int(sign, 0, 0)) && main.grid[pos.x + sign, pos.y + 1, pos.z].walkable == false)
            {
                return true;
            }
        }

        if (rulesets.WithinArray(pos + Vector3Int.down) && main.grid[pos.x, pos.y - 1, pos.z].walkable == false)
        {
            if (rulesets.WithinArray(pos + Vector3Int.down + new Vector3Int(sign, 0, 0)) && main.grid[pos.x + sign, pos.y - 1, pos.z].walkable == false)
            {
                return true;
            }
        }

        if (rulesets.WithinArray(pos + Vector3Int.RoundToInt(Vector3.forward)) && main.grid[pos.x, pos.y, pos.z + 1].walkable == false)
        {
            if (rulesets.WithinArray(pos + Vector3Int.RoundToInt(Vector3.forward) + new Vector3Int(sign, 0, 0)) && main.grid[pos.x + sign, pos.y, pos.z + 1].walkable == false)
            {
                return true;
            }
        }

        if (rulesets.WithinArray(pos + Vector3Int.RoundToInt(Vector3.back)) && main.grid[pos.x, pos.y, pos.z - 1].walkable == false)
        {
            if (rulesets.WithinArray(pos + Vector3Int.RoundToInt(Vector3.back) + new Vector3Int(sign, 0, 0)) && main.grid[pos.x + sign, pos.y, pos.z - 1].walkable == false)
            {
                return true;
            }
        }

        return false;
    }

    public bool EmptyCheckY(bool isUp, Vector3Int pos)
    {
        int sign;

        if (isUp)
        {
            sign = 1;
        }

        else
        {
            sign = -1;
        }

        if (rulesets.WithinArray(pos + Vector3Int.right) && main.grid[pos.x + 1, pos.y, pos.z].walkable == false)
        {
            if (rulesets.WithinArray(pos + Vector3Int.right + new Vector3Int(0, sign, 0)) && main.grid[pos.x + 1, pos.y + sign, pos.z].walkable == false)
            {
                return true;
            }
        }

        if (rulesets.WithinArray(pos + Vector3Int.left) && main.grid[pos.x - 1, pos.y, pos.z].walkable == false)
        {
            if (rulesets.WithinArray(pos + Vector3Int.left + new Vector3Int(0, sign, 0)) && main.grid[pos.x - 1, pos.y + sign, pos.z].walkable == false)
            {
                return true;
            }
        }

        if (rulesets.WithinArray(pos + Vector3Int.RoundToInt(Vector3.forward)) && main.grid[pos.x, pos.y, pos.z + 1].walkable == false)
        {
            if (rulesets.WithinArray(pos + Vector3Int.RoundToInt(Vector3.forward) + new Vector3Int(0, sign, 0)) && main.grid[pos.x, pos.y + sign, pos.z + 1].walkable == false)
            {
                return true;
            }
        }

        if (rulesets.WithinArray(pos + Vector3Int.RoundToInt(Vector3.back)) && main.grid[pos.x, pos.y, pos.z - 1].walkable == false)
        {
            if (rulesets.WithinArray(pos + Vector3Int.RoundToInt(Vector3.back) + new Vector3Int(0, sign, 0)) && main.grid[pos.x, pos.y + sign, pos.z - 1].walkable == false)
            {
                return true;
            }
        }

        return false;
    }

    public bool EmptyCheckZ(bool isForward, Vector3Int pos)
    {
        int sign;

        if (isForward)
        {
            sign = 1;
        }

        else
        {
            sign = -1;
        }

        if (rulesets.WithinArray(pos + Vector3Int.right) && main.grid[pos.x + 1, pos.y, pos.z].walkable == false)
        {
            if (rulesets.WithinArray(pos + Vector3Int.right + new Vector3Int(0, 0, sign)) && main.grid[pos.x + 1, pos.y, pos.z + sign].walkable == false)
            {
                return true;
            }
        }

        if (rulesets.WithinArray(pos + Vector3Int.left) && main.grid[pos.x - 1, pos.y, pos.z].walkable == false)
        {
            if (rulesets.WithinArray(pos + Vector3Int.left + new Vector3Int(0, 0, sign)) && main.grid[pos.x - 1, pos.y, pos.z + sign].walkable == false)
            {
                return true;
            }
        }

        if (rulesets.WithinArray(pos + Vector3Int.up) && main.grid[pos.x, pos.y + 1, pos.z].walkable == false)
        {
            if (rulesets.WithinArray(pos + Vector3Int.up + new Vector3Int(0, 0, sign)) && main.grid[pos.x, pos.y + 1, pos.z + sign].walkable == false)
            {
                return true;
            }
        }

        if (rulesets.WithinArray(pos + Vector3Int.down) && main.grid[pos.x, pos.y - 1, pos.z].walkable == false)
        {
            if (rulesets.WithinArray(pos + Vector3Int.down + new Vector3Int(0, 0, sign)) && main.grid[pos.x, pos.y - 1, pos.z + sign].walkable == false)
            {
                return true;
            }
        }

        return false;
    }
}
