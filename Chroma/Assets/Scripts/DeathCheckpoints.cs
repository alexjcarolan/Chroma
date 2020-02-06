using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

public class DeathCheckpoints : MonoBehaviour
{

    public GameObject bridge1;
    public GameObject bridge2;
    public GameObject bridge3;

    //public GameObject player;
    public Player player;
    Vector3 resetPosition;

    public Transform steamCamera;
    public Transform steamController;


    private void Start()
    {
        //resetPosition = player.transform.position;
        //originTransform = player.trackingOriginTransform;
        //print($"Origin transform at start = {originTransform.position.x}, {originTransform.position.y}, {originTransform.position.z}");
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.name == "Arrow(Clone)")
        {
            print("COLLIDES WITH ARROW");
            //source.PlayOneShot(collision);
            FadeOut();
            ArrowShooter.deflections = 0;
        }
    }

    void FadeOut()
    {
        if (bridge1.GetComponent<Animator>().enabled == true)
        {
            Vector3 desiredHeadPos = new Vector3(7.74f, 0f, -0.7304688f);
            steamController.position = desiredHeadPos;
            print("UPDATED POS");
        }
        if (bridge2.GetComponent<Animator>().enabled == true)
        {
            Vector3 desiredHeadPos = new Vector3(17.03f, 0f, -0.7304688f);
            steamController.position = desiredHeadPos;
            print("UPDATED POS");
        }
        if (bridge3.GetComponent<Animator>().enabled == true)
        {
            //Vector3 desiredHeadPos = new Vector3(26.59f, 0f, -0.7304688f);
            //steamController.position = desiredHeadPos;
            print("UPDATED POS");
        }
        print("fade out called");
        SteamVR_Fade.Start(Color.black, 10, false);
        //player.GetComponent<DeathCheckpoints>().enabled = false;
        //player.GetComponent<DeathCheckpoints>().enabled = true;
    }
}
