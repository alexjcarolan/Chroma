using System.Collections;

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Old_Drawline : MonoBehaviour
{
    // Start is called before the first frame update
    public SteamVR_TrackedObject trackedObj;
    //private LineRenderer currLine;
    private Old_graphics_line currLine;

    // List to store all the drawn meshes
    public List<GameObject> drawings = new List<GameObject>();

    private int numClicks = 0;


    // Update is called once per frame
    void Update()
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            GameObject go = new GameObject();
            //currLine = go.AddComponent<LineRenderer>();
            //currLine.startWidth = .1f;
            //currLine.endWidth = .1f;

            currLine = go.AddComponent<Old_graphics_line>();
            //go.AddComponent<MeshCollider>();


            //go.AddComponent<MeshFilter>();
            //go.AddComponent<MeshRenderer>();
            go.AddComponent<MeshFilter>();
            go.AddComponent<MeshRenderer>();
            go.AddComponent<MeshCollider>().convex = true;

            go.AddComponent<Interactable>();
            go.AddComponent<Rigidbody>().AddForce(new Vector3(0, -9.8f, 0));
            go.AddComponent<Throwable>();

            go.AddComponent<VelocityEstimator>();

            drawings.Add(go);
            numClicks = 0;
        }
        else if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            //currLine.positionCount=(numClicks + 1);
            //currLine.SetPosition(numClicks,trackedObj.transform.position );
            currLine.AddPoint(trackedObj.transform.position);
            numClicks++;
        }

        // Printing the total number of meshes in the array
        Debug.LogError($"Length of the drawings array is = {drawings.Count}");
    }
}