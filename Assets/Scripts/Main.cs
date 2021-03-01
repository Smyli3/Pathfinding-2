//Uses basic three in addition to UnityEngine.UI which allows for input from the user.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    // Main script that handles the main core of the game

    // Initialize the 'mode' enum, which will be used to update the script accordingly
    public enum mode {move, place}

    // Instance of the 'mode' enum
    public mode currentMode;

    // The key that will be used to switch between modes
    public KeyCode modeSwitch;

    // The text that will display the current mode and its colors
    public Text modeText;
    public Color placementMode;
    public Color movementMode;

    // Grid specifications and the grid itself that can be set in the inspector.
    public Node[,,] grid;
    public int x;
    public int y;
    public int z;

    // Reference to 'Platform' prefab
    public GameObject platformRef;

    // Platform parent that all instantiated platforms will be attached to
    public GameObject platformPar;

    Placement placement;
    Removal removal;
    Selection selection;
    Movement movement;

    // Draws a wired 3D cube in scene view that shows the size of the array
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector3(x / 2 - 0.5f, y / 2 - 0.5f, z / 2 - 0.5f), new Vector3(x, y, z));
    }

    // Game start - used to display the initial mode when the game runs and initialize grid
    private void Awake()
    {
        y += 1;
        placement = GetComponent<Placement>();
        removal = GetComponent<Removal>();
        selection = GetComponent<Selection>();
        movement = GetComponent<Movement>();
        CreateGrid();
        CreatePlatform();
        UpdateMode();
    }

    // Game update - used for switching between modes
    private void Update()
    {
        if (Input.GetKeyDown(modeSwitch) && movement.isPathfinding == false)
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
                modeText.color = placementMode;
                break;

            case mode.move:
                modeText.text = "Movement Mode";
                modeText.color = movementMode;
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
                removal.enabled = true;

                if (selection.hoverMorphBot != null)
                {
                    selection.UnselectBlock();
                    selection.hoverMorphBot = null;
                }

                selection.enabled = false;

                if (movement.currentMorphBot != null)
                {
                    movement.currentMorphBot = null;
                }

                movement.enabled = false;
                break;

            case mode.move:
                placement.morphBotHover.SetActive(false);
                placement.enabled = false;
                removal.enabled = false;
                selection.enabled = true;
                movement.enabled = true;
                break;
        }
    }

    // Creates beginning MorphBots platform
    private void CreatePlatform()
    {
        for (int a = 0; a < x; a++)
        {
            for (int b = 0; b < z; b++)
            {
                GameObject platform = Instantiate(platformRef, new Vector3(a, 0, b), Quaternion.identity);
                platform.name = "Platform(" + a + ", 0, " + b + ")";
                platform.transform.SetParent(platformPar.transform);
                grid[a, 0, b].walkable = false;
            }
        }
    }
    
    // Creates beginning MorphBots grid using Node class
    private void CreateGrid()
    {
        grid = new Node[x, y, z];

        for (int a = 0; a < x; a++)
        {
            for (int b = 0; b < y; b++)
            {
                for (int c = 0; c < z; c++)
                {
                    grid[a, b, c] = new Node(new Vector3Int(a, b, c), true);
                }
            }
        }
    }
}
