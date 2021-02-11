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


    private void Awake()
    {
        selection = GetComponent<Selection>();    
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

                else //if (raycastHit.transform.gameObject != currentMorphBot)
                {
                    currentMorphBot = raycastHit.transform.gameObject;
                    selection.hoverMorphBot.GetComponent<MeshRenderer>().material = selection.defaultMat;
                    selection.hoverMorphBot = currentMorphBot;
                    selection.hoverMorphBot.GetComponent<MeshRenderer>().material = selection.hover; 
                    selection.enabled = false;
                }
            }
        }
    }

}
