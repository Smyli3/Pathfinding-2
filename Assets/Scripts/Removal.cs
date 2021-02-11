using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Removal : MonoBehaviour
{
    GameObject currentMorphBot;
    public LayerMask gameLayers;
    RaycastHit raycastHit;
    public int maxRaycastDistance;
    Main main;

    private void Awake()
    {
        main = GetComponent<Main>();
    }

    private void Update()
    {
        // Checks every frame to see if camera is pointing to any cube, registering it as currentMorphBot if true
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, maxRaycastDistance, gameLayers))
        {
            if (raycastHit.transform.gameObject.layer == 9)
            {
                currentMorphBot = raycastHit.transform.gameObject;
            }

            // Sets currentMorphBot to null if no cube is detected
            else
            {
                currentMorphBot = null;
            }
        }

        else
        {
            currentMorphBot = null;
        }

        // Deletes currentMorphBot if it is not null
        if (Input.GetMouseButtonDown(1))
        {
            if (currentMorphBot != null)
            {
                Vector3Int location = Vector3Int.RoundToInt(currentMorphBot.transform.position);
                main.grid[location.x, location.y, location.z].walkable = true;
                Destroy(currentMorphBot);
            }
        }
    }
}
