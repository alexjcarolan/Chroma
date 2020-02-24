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
    public Material draw_material;
    public SteamVR_TrackedObject trackedObj;
    //private LineRenderer currLine;
    private GraphicsLineRenderer currLine;

    // List to store all the drawn meshes
    public List<GameObject> drawings = new List<GameObject>();
    private List<float> lineSizes = new List<float> { 0.01f, 0.11f, 0.21f, 0.31f, 0.41f, 0.51f };

    private int numClicks = 0;
    private bool doubleClick;
    private float clickTimer;
    private float timeBetweenClicks = 1;
    private int lineIndex = 0;
    public static Transform child = null;
    public static Transform parent = null;

    void Start()
    {
        GraphicsLineRenderer.lmat = draw_material;
    }



    void callback()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        //clickTimer += Time.deltaTime;
        //print(GraphicsLineRenderer.lineSize);

        //if (clickTimer > timeBetweenClicks)
        //{
        //    clickTimer = 0;
        //    doubleClick = false;
        //}

        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            //print("press");
            //if(doubleClick)
            //{
            //    print("DOUBLE CLICKED");

            //    lineIndex += 1;
            //    lineIndex = lineIndex % (lineSizes.Count);
            //    GraphicsLineRenderer.lineSize = lineSizes[lineIndex];
            //    print(GraphicsLineRenderer.lineSize);
               
            //}
            //else
            //{
            //    doubleClick = true;
            //    //GraphicsLineRenderer.lineSize = 0.01f;
            //}

            GameObject go = new GameObject();
            go.name = "drawing";
          // go.transform.gameObject.tag = "drawing";
           // go.tag = "drawing";
            //currLine = go.AddComponent<LineRenderer>();
            //currLine.startWidth = .1f;
            //currLine.endWidth = .1f;
 
            currLine = go.AddComponent<GraphicsLineRenderer>();
            //go.AddComponent<MeshCollider>();
           
            go.AddComponent<MeshFilter>();
            go.AddComponent<MeshRenderer>();
            go.AddComponent<MeshCollider>().convex = true;

            go.AddComponent<Interactable>();
            Rigidbody rb = go.AddComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            go.AddComponent<Throwable>();
            go.AddComponent<HingeJoint>();
          
            //go.AddComponent<MeshCollider>().isTrigger = true;
            


            //go.AddComponent<VelocityEstimator>();

            drawings.Add(go);
            numClicks = 0;
            go.AddComponent<DrawingsAttach>();
        }
        else if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            currLine.AddPoint(trackedObj.transform.position);
            numClicks++;
            print("touched");
        }

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            lineIndex += 1;
            lineIndex = lineIndex % (lineSizes.Count);
            GraphicsLineRenderer.lineSize = lineSizes[lineIndex];
            print(GraphicsLineRenderer.lineSize);
        }

        // Printing the total number of meshes in the array
        //Debug.LogError($"Length of the drawings array is = {drawings.Count}");
    }
}