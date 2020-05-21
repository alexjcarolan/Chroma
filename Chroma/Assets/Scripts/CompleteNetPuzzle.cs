using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class CompleteNetPuzzle : MonoBehaviour
{
    public GameObject teleportArea;
    public GameObject bridgeTeleport;
    public GameObject bridge;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<GraphicsLineRenderer>() != null)
        {
            teleportArea.GetComponent<TeleportArea>().locked = false;
            bridgeTeleport.GetComponent<TeleportArea>().locked = false;
            bridge.GetComponent<Animator>().enabled = true;
        }
    }
}
