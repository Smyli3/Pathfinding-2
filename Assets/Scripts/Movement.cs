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
    KeyCode translate;
    Rulesets rulesets;
    Functions functions;
    Pathfinding pathfinding;

    private void Awake()
    {
        selection = GetComponent<Selection>();
        rulesets = GetComponent<Rulesets>();
        functions = GetComponent<Functions>();
        pathfinding = GetComponent<Pathfinding>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, maxDistance, gameLayers))
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

        if (Input.GetKeyDown(translate) && currentMorphBot != null)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, maxDistance, gameLayers))
            {
                Vector3Int endLocation = functions.Vector3ToGrid(raycastHit);

                if (rulesets.WithinArray(endLocation))
                {
                    // TURN ON LATER pathfinding.FindPath(Vector3Int.RoundToInt(currentMorphBot.transform.position), endLocation);


                    // initiate pathfinding so its reusable, also make sure you cant exit modes or use the T key again and that selection
                    // and clicking on blocks cant happen
                }
            }
        }
    }
}
