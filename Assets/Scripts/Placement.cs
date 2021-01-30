using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Placement : MonoBehaviour
{
    public GameObject morphBotHover;
    public GameObject morphBotHoverRef;
    public GameObject morphBotRef;
    public GameObject morphBotsPar;
    public int maxRaycastDistance;
    public LayerMask gameLayers;
    RaycastHit raycastHit;
    Rulesets rulesets;
    Functions functions;

    private void Awake()
    {
        rulesets = GetComponent<Rulesets>();
        functions = GetComponent<Functions>();
        morphBotHover = Instantiate(morphBotHoverRef, new Vector3(0, 0, 0), Quaternion.identity);
        morphBotHover.name = "MorphBot Highlight";
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out raycastHit, maxRaycastDistance, gameLayers))
        {
            Vector3 location = functions.Vector3ToGrid(raycastHit);

            if (rulesets.WithinArray(Vector3Int.RoundToInt(location)))
            {
                if (morphBotHover.activeSelf == false)
                {
                    morphBotHover.SetActive(true);
                }

                morphBotHover.transform.position = location;
            }

            else
            {
                if (morphBotHover.activeSelf != false)
                {
                    morphBotHover.SetActive(false);
                }
            }
        }

        else
        {
            if (morphBotHover.activeSelf != false)
            {
                morphBotHover.SetActive(false);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;
            Ray placementRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out raycastHit, maxRaycastDistance, gameLayers))
            {
                Vector3 location = functions.Vector3ToGrid(raycastHit);

                if (rulesets.WithinArray(Vector3Int.RoundToInt(location)))
                {
                    GameObject morphBot = Instantiate(morphBotRef, location, Quaternion.identity);
                    morphBot.transform.SetParent(morphBotsPar.transform);
                    morphBot.name = "MorphBot(" + location.x + ", " + location.y + ", " + location.z + ")";
                }
            }



        }
    }
}
