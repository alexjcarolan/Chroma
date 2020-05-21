using System.Collections;

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Valve.VR.InteractionSystem;
using HTC.UnityPlugin.Vive;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DrawLineManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Material draw_material;
    public SteamVR_TrackedObject trackedObj;
    //private LineRenderer currLine;
    private GraphicsLineRenderer currLine;
    public GameObject sizeObject;

    public static int drawingCount;

    // List to store all the drawn meshes
    public static List<GameObject> drawings = new List<GameObject>();
    public List<float> lineSizes = new List<float> { 0.01f, 0.11f, 0.21f, 0.31f, 0.41f, 0.51f };

    private int numClicks = 0;
    private bool doubleClick;
    private float clickTimer;
    private float timeBetweenClicks = 1;
    public int lineIndex = 0;
    public static Transform child = null;
    public static Transform parent = null;

    private GameObject go;

    void Start()
    {
        GraphicsLineRenderer.lmat = draw_material;
        drawingCount = drawings.Count;
    }



    void callback()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);

        drawingCount = drawings.Count;
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {

            bool flag = FindObjectOfType<AudioManager>().drawing_playing;
            if (!flag)
            {
              FindObjectOfType<AudioManager>().Play("draw_sound");
              FindObjectOfType<AudioManager>().drawing_playing = true;
            }

            go = new GameObject();
            go.name = "drawing" + drawingCount;
            go.tag = "Drawing";


            currLine = go.AddComponent<GraphicsLineRenderer>();

            go.AddComponent<MeshFilter>();
            go.AddComponent<MeshRenderer>();
            go.AddComponent<MeshCollider>().convex = true;
            if(SceneManager.GetActiveScene().name != "secondRoom")
            {
                go.AddComponent<DrawingsAttach>();
            }
            go.AddComponent<Interactable>();
            Rigidbody rb = go.AddComponent<Rigidbody>();
            go.AddComponent<DrawingStruct>().startTime = Time.time;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            go.AddComponent<Throwable>();

            drawings.Add(go);
            numClicks = 0;

        }
        else if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            bool flag = FindObjectOfType<AudioManager>().drawing_playing;
            if (flag)
            {
              FindObjectOfType<AudioManager>().Stop("draw_sound");
              FindObjectOfType<AudioManager>().drawing_playing = false;
            }
            print("TOUCH UP");
            if (SceneManager.GetActiveScene().name == "secondRoom")
            {
                print("adding mass dlm now");
                go.AddComponent<Mass_DLM>();
                go.GetComponent<Mass_DLM>().setMass();
            }
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
            sizeObject.transform.localScale += new Vector3(2.0f, 0f, 2.0f);
            if (sizeObject.transform.localScale.x > 12.0f)
            {
                sizeObject.transform.localScale = new Vector3(2.0f, 0.5000005f, 2.0f);
            }
            
            GraphicsLineRenderer.lineSize = lineSizes[lineIndex];
        }
    }
}
