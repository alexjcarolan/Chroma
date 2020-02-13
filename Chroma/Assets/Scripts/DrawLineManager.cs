using System.Collections;

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Valve.VR.InteractionSystem;
using HTC.UnityPlugin.Vive;
using UnityEngine.Events;

public class DrawLineManager : MonoBehaviour
{
    // Start is called before the first frame update
    public SteamVR_TrackedObject trackedObj;
    //private LineRenderer currLine;
    private GraphicsLineRenderer currLine;

    // List to store all the drawn meshes
    public List<GameObject> drawings = new List<GameObject>();

    private int numClicks = 0;
    private bool doubleClick;
    private float clickTimer;
    private float timeBetweenClicks = 1;


    void callback()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        clickTimer += Time.deltaTime;
        print(GraphicsLineRenderer.lineSize);

        if (clickTimer > timeBetweenClicks)
        {
            clickTimer = 0;
            doubleClick = false;
        }

        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            if(doubleClick)
            {
                if (GraphicsLineRenderer.lineSize < 0.52f)
                {
                    GraphicsLineRenderer.lineSize += 0.1f;
                }
            }
            else
            {
                doubleClick = true;
                //GraphicsLineRenderer.lineSize = 0.01f;
            }

            GameObject go = new GameObject();
            //currLine = go.AddComponent<LineRenderer>();
            //currLine.startWidth = .1f;
            //currLine.endWidth = .1f;
 
            currLine = go.AddComponent<GraphicsLineRenderer>();
            //go.AddComponent<MeshCollider>();
            

            //go.AddComponent<MeshFilter>();
            //go.AddComponent<MeshRenderer>();
            go.AddComponent<MeshFilter>();
            go.AddComponent<MeshRenderer>();
            go.AddComponent<MeshCollider>().convex = true;

            go.AddComponent<Interactable>();
            Rigidbody rb = go.AddComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            go.AddComponent<Throwable>();

            //go.AddComponent<VelocityEstimator>();

            drawings.Add(go);
            numClicks = 0;
        }
        else if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            currLine.AddPoint(trackedObj.transform.position);
            numClicks++;
        }
        
        // Printing the total number of meshes in the array
        //Debug.LogError($"Length of the drawings array is = {drawings.Count}");
    }
}