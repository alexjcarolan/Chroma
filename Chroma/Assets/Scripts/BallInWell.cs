using System.Collections;

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class BallInWell : MonoBehaviour
{
    // Make the boundaries of the well MeshColliders
    // Add a plane inside the well, give it a Boxcollider
    // Get the ball game object name (Ball (Clone))
    // Then do the OnCollisionEnter thing.

    public GameObject bridge;
    public GameObject cubeSpawner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        //    // open the bridge
        //print("BALL IN WELL");
        FindObjectOfType<AudioManager>().Play("ball_in_well");
        bridge.GetComponent<Animator>().enabled = true;
        GameObject.Find("GolfBallSpawner").GetComponent<CubeSpawner>().enabled = false;
    }
} 
