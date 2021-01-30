using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    public float zoomSpeed;
    public float maxZoom;
    Vector3 currentZoom;

    private void Start()
    {
        currentZoom = this.transform.localPosition;
    }

    private void Update()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            currentZoom.z = Mathf.Clamp(currentZoom.z + (Input.mouseScrollDelta.y * Time.deltaTime * zoomSpeed), maxZoom * -1, -3);
            this.transform.localPosition = currentZoom;
        }
    }
}
