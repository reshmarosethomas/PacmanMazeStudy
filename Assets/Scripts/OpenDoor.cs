using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    Transform pacmanPos;
    Transform blinkyPos;
    Transform inkyPos;
    Transform pinkyPos;
    Transform clydePos;

    SpriteRenderer doorRenderer;
    public SpriteRenderer[] knobRenderer;

    float sensorStrength = 1.5f;

    float distPacman = 0f, distBlinky = 0f, distInky = 0f, distPinky = 0f, distClyde =0f;
    public int door_open_num = 0;
    bool isPacmanPassingDoor = false;

    // Start is called before the first frame update
    void Start()
    {
        pacmanPos = GameObject.Find("pacman").GetComponent<Transform>();
        blinkyPos = GameObject.Find("blinky").GetComponent<Transform>();
        inkyPos = GameObject.Find("inky").GetComponent<Transform>();
        pinkyPos = GameObject.Find("pinky").GetComponent<Transform>();
        clydePos = GameObject.Find("clyde").GetComponent<Transform>();

        doorRenderer = transform.parent.gameObject.GetComponent<SpriteRenderer>();
        knobRenderer = transform.parent.GetComponentsInChildren<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        distPacman = Vector2.Distance(pacmanPos.position, transform.position);
        distBlinky = Vector2.Distance(blinkyPos.position, transform.position);
        distInky = Vector2.Distance(inkyPos.position, transform.position);
        distPinky = Vector2.Distance(pinkyPos.position, transform.position);
        distClyde = Vector2.Distance(clydePos.position, transform.position);

        if (distPacman < sensorStrength || distBlinky < sensorStrength || distInky < sensorStrength || distPinky < sensorStrength || distClyde < sensorStrength)
        {
            
            Color tempDoorColor = doorRenderer.color;
            tempDoorColor.a = 0f;
            doorRenderer.color = tempDoorColor;

            foreach (SpriteRenderer childRenderer in knobRenderer)
            {
                Color tempKnobColor = childRenderer.color;
                tempKnobColor.a = 0f;
                childRenderer.color = tempKnobColor;
            }

            if (distPacman < sensorStrength && !isPacmanPassingDoor)
            {
                door_open_num += 1;
                isPacmanPassingDoor = true;
            }

            if (distPacman >= sensorStrength)
                isPacmanPassingDoor = false;

        }

        else
        {
            
            Color tempDoorColor = doorRenderer.color;
            tempDoorColor.a = 1f;
            doorRenderer.color = tempDoorColor;

            foreach (SpriteRenderer childRenderer in knobRenderer)
            {
                Color tempKnobColor = childRenderer.color;
                tempKnobColor.a = 1f;
                childRenderer.color = tempKnobColor;
            }

            isPacmanPassingDoor = false;

        }

        
    }
}
