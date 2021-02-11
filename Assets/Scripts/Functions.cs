using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Functions : MonoBehaviour
{
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
}
