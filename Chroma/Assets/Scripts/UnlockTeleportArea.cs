using System.Collections;

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class UnlockTeleportArea : MonoBehaviour 
{
    public GameObject bridge1;
    public GameObject bridge2;
    //public GameObject bridge3;

    public GameObject teleport1;
    public GameObject teleport2;
    public GameObject teleport3;
    public GameObject teleportBridge1;
    public GameObject teleportBridge2;
    //public GameObject teleport4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if(bridge1.GetComponent<Animator>().enabled == true)
        {
            teleportBridge1.GetComponent<TeleportArea>().locked = false;
            teleport2.GetComponent<TeleportArea>().locked = false;
        }
        if (bridge2.GetComponent<Animator>().enabled == true)
        {
            teleportBridge2.GetComponent<TeleportArea>().locked = false;
            teleport3.GetComponent<TeleportArea>().locked = false;
        }
        //if (bridge3.GetComponent<Animator>().enabled == true)
        //{
        //    teleport4.GetComponent<TeleportArea>().locked = false;
        //}
    }
}