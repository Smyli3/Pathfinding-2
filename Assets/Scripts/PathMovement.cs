using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMovement : MonoBehaviour
{
    Functions functions;
    Movement movement;
    Main main;
    Pathfinding pathfinding;

    public float initialTimer;
    float currentTimer;
    float timePercent;
    public int index;

    public Vector3 tempPos;

    private void Awake()
    {
        functions = GetComponent<Functions>();
        movement = GetComponent<Movement>();
        main = GetComponent<Main>();
        pathfinding = GetComponent<Pathfinding>();
        currentTimer = initialTimer;
    }
    public void MoveCube()
    {
        // set index to 0 each time you start.
        // set temppos to current position of currentMorphBot
        // do the 2 things above in pathfinding script.
        movement.currentMorphBot.transform.position = Vector3.Lerp(tempPos, pathfinding.pathfinder[index].position, Timer());

        if (movement.currentMorphBot.transform.position == pathfinding.pathfinder[index].position)
        {
            movement.currentMorphBot.transform.position = pathfinding.pathfinder[index].position;
            tempPos = movement.currentMorphBot.transform.position;
            currentTimer = initialTimer;
            index++;
        }

        if (movement.currentMorphBot.transform.position != pathfinding.pathfinder[pathfinding.pathfinder.Count - 1].position)
        {
            Invoke("MoveCube", Time.deltaTime);
        }

        else
        {
            Vector3Int newPos = Vector3Int.RoundToInt(tempPos);
            movement.isPathfinding = false;
            main.grid[newPos.x, newPos.y, newPos.z].walkable = false;
            movement.currentMorphBot.name = "MorphBot(" + newPos.x + ", " + newPos.y + ", " + newPos.z + ")";
            // change name of currentMoprhBot to the position that it is in
            // make the current position of currentMorphBot into a Node, and make the walkable into false.
            print("FINISHED");
        }
    }

    private float Timer()
    {
        currentTimer = Mathf.Clamp(currentTimer - Time.deltaTime, 0, initialTimer);
        timePercent = 1 - (currentTimer / initialTimer);

        return timePercent;

    }
}