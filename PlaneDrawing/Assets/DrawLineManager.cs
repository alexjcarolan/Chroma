using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineManager : MonoBehaviour
{
    // Start is called before the first frame update
    public SteamVR_TrackedObject trackedObj;
    private LineRenderer currLine;

    private int numClicks= 0;

    // Update is called once per frame
    void Update()
    {  
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            GameObject go = new GameObject();
            currLine = go.AddComponent<LineRenderer>();
            currLine.startWidth = .1f;
            currLine.endWidth = .1f;
            numClicks = 0;

        }else if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            currLine.positionCount=(numClicks + 1);
            currLine.SetPosition(numClicks,trackedObj.transform.position );
            numClicks++;
        }

    }
}
