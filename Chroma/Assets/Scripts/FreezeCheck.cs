using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

public class FreezeCheck : MonoBehaviour
{
    GameObject leftController;

    // Start is called before the first frame update
    void Start()
    {
        leftController = GameObject.FindGameObjectWithTag("LeftController");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var device = SteamVR_Controller.Input((int)leftController.GetComponent<SteamVR_TrackedObject>().index);
        if (!(transform.parent != null && transform.parent.parent != null))
        {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            gameObject.GetComponent<Rigidbody>().velocity = device.velocity * 2;
            gameObject.GetComponent<Rigidbody>().angularVelocity = device.angularVelocity * 2;
            Destroy(GetComponent<FreezeCheck>());
        }
    }
}

