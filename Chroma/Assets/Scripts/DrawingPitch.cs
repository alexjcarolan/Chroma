using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingPitch : MonoBehaviour
{
    public SteamVR_TrackedObject objectTracked;
    public static Vector3 curr;
    public static Vector3 prev;
    // Start is called before the first frame update
    void Start()
    {
        print("HERE PITCH");
        objectTracked = GetComponent<SteamVR_TrackedObject>();
        curr = objectTracked.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        prev = curr;
        Debug.Log("PREV  " + prev);
        curr = objectTracked.transform.position;
        Debug.Log("CURR  " + curr);
    }
}
