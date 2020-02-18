using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

public class EraseDrawings : MonoBehaviour
{
    public SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;
    public bool boolpressed;

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

    public void OnCollisionStay(Collision other)
    {
        print("COLLIDED");
        print("got graphics rend "+ other.gameObject.GetComponent<GraphicsLineRenderer>() != null);
        print("this is bool pressed2: " + boolpressed);
        if (ViveInput.GetPress(HandRole.LeftHand, ControllerButton.Menu) && (other.gameObject.GetComponent<GraphicsLineRenderer>() != null))
        {
        
            print("MENU BUTTON DELETE");
            Destroy(other.gameObject);
        }
    }
}
