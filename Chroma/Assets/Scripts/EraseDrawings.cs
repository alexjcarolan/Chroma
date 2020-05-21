using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

public class EraseDrawings : MonoBehaviour
{
    public SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;
    public bool boolpressed;
   // private List<GameObject> listOfChildren = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);
        boolpressed = ViveInput.GetPress(HandRole.LeftHand, ControllerButton.Menu);
        print("update:this is bool pressed: " + boolpressed);
    }

    public void OnTriggerStay(Collider other)
    {
        print("COLLIDED");
        print("got graphics rend "+ other.gameObject.GetComponent<GraphicsLineRenderer>() != null);
        print("this is bool pressed2: " + boolpressed);
        if (ViveInput.GetPress(HandRole.LeftHand, ControllerButton.Menu) && (other.gameObject.GetComponent<GraphicsLineRenderer>() != null))
        {
            GameObject par = other.gameObject.GetComponent<DrawingStruct>().parent;
            if (par == null)
            {
                par = other.gameObject;
            }

            while (par.GetComponent<DrawingStruct>().parent != null && par.GetComponent<DrawingStruct>().parent.tag == "Drawing" )
            {
                par = par.GetComponent<DrawingStruct>().parent;
                print("HEREEE");

            }
            DrawLineManager.drawings.Remove(par);
            print("PARNAME" + par.name);
            Destroy(par);
            FindObjectOfType<AudioManager>().Play("delete_drawing");



            var childrenArr = GetChildRecursive(par);
            print("CHILD ARR COUNT " + childrenArr.Count);
            

            print("MENU BUTTON DELETE");

                foreach (GameObject ob in childrenArr)
                {
                    if (DrawLineManager.drawings.Contains(ob.gameObject))
                    {
                        print("IT CONTAINS");
                        DrawLineManager.drawings.Remove(ob.gameObject);
                    }
                }
            print("DRAW LINE MAN COUNT = "+ DrawLineManager.drawings.Count);
            print("NOT DELETED " +  DrawLineManager.drawings[0].gameObject.name);


        }
    }





    private List<GameObject> GetChildRecursive(GameObject obj)
    {
        List<GameObject> listOfChildren = new List<GameObject>();
        if (null == obj)
            return listOfChildren;

        foreach (Transform child in obj.transform)
        {
            if (null == child)
                continue;
            //child.gameobject contains the current child you can do whatever you want like add it to an array
            listOfChildren.Add(child.gameObject);
            listOfChildren.AddRange(GetChildRecursive(child.gameObject));
        }
        return listOfChildren;
    }
}
