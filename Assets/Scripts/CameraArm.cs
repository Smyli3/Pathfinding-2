//Uses basic three.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraArm : MonoBehaviour
{
    // IMPORTANT: the camera arm is a parent of the actual camera, which allows for rotation relative to a point (the camera arm's position)
    
    // The speed that the rotation of the camera will occur at
    public int rotationSpeed;

    // A Vector3 used to store, modify, and set the camera arm's rotation
    Vector3 currentRotation;

    // The maximum magnitude that the camera can rotate in the up/down direction
    public float rotationBounds;

    // Rotation keys
    public KeyCode left;
    public KeyCode right;
    public KeyCode up;
    public KeyCode down;

    // Makes sure that the camera arm's current rotation is within the magnitudes specified by rotationBounds
    // Also sets position of camera arm 
    private void Awake()
    {
        // Makes sure user input is a magnitude without negative signs
        rotationBounds = Mathf.Abs(rotationBounds);
        currentRotation = this.transform.eulerAngles;
        currentRotation = new Vector3(Mathf.Clamp(currentRotation.x, rotationBounds * -1, rotationBounds), currentRotation.y, 0);
        // Moves the camera arm to the center of the platform (where the main camera will be rotating relative to)
        this.transform.position = new Vector3(FindObjectOfType<Main>().x / 2 - 0.5f, -0.5f, FindObjectOfType<Main>().z / 2 - 0.5f);
    }

    // Runs a series of if/else statements to rotate the camera
    private void Update()
    {
        if (Input.GetKey(up))
        {
            currentRotation.x = Mathf.Clamp(currentRotation.x + rotationSpeed * Time.deltaTime, rotationBounds * -1, rotationBounds);
            this.transform.eulerAngles = currentRotation;
        }

        else if (Input.GetKey(down))
        {
            currentRotation.x = Mathf.Clamp(currentRotation.x - rotationSpeed * Time.deltaTime, rotationBounds * -1, rotationBounds);
            this.transform.eulerAngles = currentRotation;
        }

        if (Input.GetKey(right))
        {
            currentRotation.y += rotationSpeed * Time.deltaTime;
            this.transform.eulerAngles = currentRotation;
        }

        else if (Input.GetKey(left))
        {
            currentRotation.y -= rotationSpeed * Time.deltaTime;
            this.transform.eulerAngles = currentRotation;
        }
    }
}
