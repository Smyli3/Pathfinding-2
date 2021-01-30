using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    // Initialize the 'mode' enum, which will be used to update the script accordingly
    public enum mode {move, place}

    // Instance of the 'mode' enum
    public mode currentMode;

    // The key that will be used to switch between modes
    public KeyCode modeSwitch;

    // The text that will display the current mode
    public Text modeText;

    // Grid
    public Node[,,] grid;
    public int x;
    public int y;
    public int z;

    // Reference to 'Platform' prefab
    public GameObject platformRef;

    // Platform parent that all instantiated platforms will be attached to
    public GameObject platformPar;

    Placement placement;

    // Game start - used to display the initial mode when the game runs and initialize grid
    private void Awake()
    {
        placement = GetComponent<Placement>();
        grid = new Node[x, y, z];
        CreatePlatform();
        UpdateMode();
    }

    // Game update - used for switching between modes
    private void Update()
    {
        if (Input.GetKeyDown(modeSwitch))
        {
            UpdateMode();
        }
    }

    // Cycles between mode.place and mode.move and updates modeText and other scripts accordingly
    private void UpdateMode()
    {
        switch (currentMode)
        {
            case mode.place:
                currentMode = mode.move;
                break;

            case mode.move:
                currentMode = mode.place;
                break;
        }

        UpdateText();
        UpdateScripts();
    }

    // Updates modeText; called by UpdateMode
    private void UpdateText()
    {
        switch (currentMode)
        {
            case mode.place:
                modeText.text = "Placement Mode";
                modeText.color = new Color(1, 1, 0);
                break;

            case mode.move:
                modeText.text = "Movement Mode";
                modeText.color = new Color(0, 1, 1);
                break;
        }
    }

    // Updates relevant scripts by enabling/disabling them depending on the current mode
    private void UpdateScripts()
    {
        switch (currentMode)
        {
            case mode.place:
                placement.enabled = true;
                break;

            case mode.move:
                placement.morphBotHover.SetActive(false);
                placement.enabled = false;
                break;
        }
    }

    private void CreatePlatform()
    {
        for (int a = 0; a < x; a++)
        {
            for (int b = 0; b < z; b++)
            {
                GameObject platform = Instantiate(platformRef, new Vector3(a, -1, b), Quaternion.identity);
                platform.name = "Platform(" + a + ", -1, " + b + ")";
                platform.transform.SetParent(platformPar.transform);
            }
        }
    }
}
