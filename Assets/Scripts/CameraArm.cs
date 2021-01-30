using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraArm : MonoBehaviour
{
    public int rotationSpeed;
    Vector3 currentRotation;

    public float rotationBounds;

    public KeyCode left;
    public KeyCode right;
    public KeyCode up;
    public KeyCode down;

    private void Awake()
    {
        rotationBounds = Mathf.Abs(rotationBounds);
        currentRotation = this.transform.eulerAngles;
        currentRotation = new Vector3(Mathf.Clamp(currentRotation.x, rotationBounds * -1, rotationBounds), currentRotation.y, 0);
        this.transform.position = new Vector3(FindObjectOfType<Main>().x / 2 - 0.5f, -0.5f, FindObjectOfType<Main>().z / 2 - 0.5f);
    }

    private void Update()
    {
        if (Input.GetKey(up))
        {
            currentRotation.x = Mathf.Clamp(currentRotation.x + rotationSpeed * Time.deltaTime, rotationBounds * -1, rotationBounds);
            this.transform.eulerAngles = currentRotation;
        }

        if (Input.GetKey(down))
        {
            currentRotation.x = Mathf.Clamp(currentRotation.x - rotationSpeed * Time.deltaTime, rotationBounds * -1, rotationBounds);
            this.transform.eulerAngles = currentRotation;
        }

        if (Input.GetKey(right))
        {
            currentRotation.y += rotationSpeed * Time.deltaTime;
            this.transform.eulerAngles = currentRotation;
        }

        if (Input.GetKey(left))
        {
            currentRotation.y -= rotationSpeed * Time.deltaTime;
            this.transform.eulerAngles = currentRotation;
        }
    }
}
