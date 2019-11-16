using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public SteamVR_TrackedObject trackedObject;
    private LineRenderer currentLine;           // to be able to save the last drawn thing everytime update is called
    private int clickCount = 0;

    // Update is called once per frame
    void Update()
    {   
        // Getting the Input (tracked object's index)
        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObject.index);

        // Condition to check if the trigger is pulled (button on the bottom of the controller)
        if(device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            //Debug.LogError("Button Pressed");   
            GameObject go = new GameObject();                       // creating a new Game Object
            currentLine = go.AddComponent<LineRenderer>();          // adding the Line Renderer Component
            clickCount = 0;
        }
        else if(device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {   // if the button is held down, continue to add more points to the line

            // Need to update the size of the Line Renderer everytime a point is added
            currentLine.positionCount = clickCount + 1;

            // sets the position of a vertex in a line
            currentLine.SetPosition(clickCount, device.transform.pos);
            clickCount += 1;
        }
    }
}
