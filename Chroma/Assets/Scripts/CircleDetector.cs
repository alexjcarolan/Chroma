using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class CircleDetector : MonoBehaviour
{
    public static bool flag = false;
    public GameObject bridge;
    public GameObject teleportBridge;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (flag == true)
        {
            bridge.GetComponent<Animator>().enabled = true;
            GetComponent<CircleDetector>().enabled = false;
            teleportBridge.GetComponent<TeleportArea>().locked = false;
        }
        
    }

    public static void CallFunction()
    {
        
        GameObject ob = GameObject.FindGameObjectWithTag("Drawing");
        if (ob != null && flag == false)
        {
            int det = Dll.Load();
            if (det == 1)
            {
                print("Detected");
                flag = true; 
            }
            
        }
    }

    internal static class Dll
    {
        [DllImport("testdetect")]
        internal static extern int Load();
    }
}
