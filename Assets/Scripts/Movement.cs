using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public LayerMask gameLayers;
    public float maxDistance;
    RaycastHit raycastHit;
    public GameObject currentMorphBot;
    Selection selection;
    public KeyCode translate;
    Rulesets rulesets;
    Functions functions;
    Pathfinding pathfinding;
    Main main;
    public bool isPathfinding;

    private void Awake()
    {
        selection = GetComponent<Selection>();
        rulesets = GetComponent<Rulesets>();
        functions = GetComponent<Functions>();
        pathfinding = GetComponent<Pathfinding>();
        main = GetComponent<Main>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isPathfinding == false)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, maxDistance, gameLayers) && raycastHit.transform.gameObject.layer == 9)
            {
                selection.enabled = true;

                if (raycastHit.transform.gameObject == currentMorphBot && currentMorphBot != null)
                {
                    currentMorphBot = null;
                }

                else
                {
                    currentMorphBot = raycastHit.transform.gameObject;
                    selection.hoverMorphBot.GetComponent<MeshRenderer>().material = selection.defaultMat;
                    selection.hoverMorphBot = currentMorphBot;
                    selection.hoverMorphBot.GetComponent<MeshRenderer>().material = selection.hover; 
                    selection.enabled = false;
                }
            }
        }

        if (Input.GetKeyDown(translate) && currentMorphBot != null && isPathfinding == false)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, maxDistance, gameLayers))
            {
                Vector3Int endLocation = functions.Vector3ToGrid(raycastHit);

                if (rulesets.WithinArray(endLocation))
                {
                    isPathfinding = true;
                    Vector3Int pos = Vector3Int.RoundToInt(currentMorphBot.transform.position);
                    main.grid[pos.x, pos.y, pos.z].walkable = true;
                    pathfinding.FindPath(Vector3Int.RoundToInt(currentMorphBot.transform.position), endLocation);
                    // initiate pathfinding so its reusable, also make sure you cant exit modes or use the T key again and that selection
                    // and clicking on blocks cant happen
                    // also make sure to reset all the Node parents to make sure anything doesnt happen (do this only if find issues)

                    //make the currentNode walkable "true" 
                }
            }
        }
    }
}
