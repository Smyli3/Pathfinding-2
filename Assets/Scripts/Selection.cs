using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    public Material hover;
    public Material defaultMat;
    RaycastHit raycastHit;
    public float maxDistance;
    public LayerMask gameLayers;
    public GameObject hoverMorphBot;

    private void Update()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, maxDistance, gameLayers))
        {
            if (raycastHit.transform.gameObject.layer == 9)
            {
                if (raycastHit.transform.gameObject != hoverMorphBot)
                {
                    if (hoverMorphBot != null)
                    {
                        UnselectBlock();
                    }

                    hoverMorphBot = raycastHit.transform.gameObject;
                    SelectBlock();
                }
            }

            else
            {
                if (hoverMorphBot != null)
                {
                    UnselectBlock();
                    hoverMorphBot = null;
                }
            }
        }

        else
        {
            if (hoverMorphBot != null)
            {
                {
                    UnselectBlock();
                    hoverMorphBot = null;
                }
            }
        }
    }

    public void SelectBlock()
    {
        hoverMorphBot.GetComponent<MeshRenderer>().material = hover;
    }

    public void UnselectBlock()
    {
        hoverMorphBot.GetComponent<MeshRenderer>().material = defaultMat;
    }
}
