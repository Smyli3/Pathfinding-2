using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rulesets : MonoBehaviour
{
    Main main;

    private void Awake()
    {
        main = GetComponent<Main>();
    }

    public bool WithinArray(Vector3Int position)
    {
        if (position.x >= main.x || position.x < 0)
        {
            return false;
        }

        if (position.y >= main.y || position.y < 0)
        {
            return false;
        }

        if (position.z >= main.z || position.z < 0)
        {
            return false;
        }

        return true;
    }
}
