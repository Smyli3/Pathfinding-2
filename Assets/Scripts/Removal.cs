using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Removal : MonoBehaviour
{
    GameObject currentMorphBot;
    public LayerMask gameLayers;
    RaycastHit raycastHit;
    public int maxRaycastDistance;

    private void Update()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, maxRaycastDistance, gameLayers))
        {
            if (raycastHit.transform.gameObject.layer == 9)
            {
                currentMorphBot = raycastHit.transform.gameObject;
            }

            else
            {
                currentMorphBot = null;
            }
        }

        else
        {
            currentMorphBot = null;
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (currentMorphBot != null)
            {
                Destroy(currentMorphBot);
            }
        }
    }
}
