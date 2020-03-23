using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBouncy : MonoBehaviour
{
    public PhysicMaterial mat;
    public float releaseVelocity = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("GolfBall"))
        {
            if (go.transform.position == new Vector3(-15.05f, 0.3631643f, -6.08f))
            {
                CubeSpawner.golfSpawn = false;
                break;
            }
            else
            {
                CubeSpawner.golfSpawn = true;
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.GetComponent<GraphicsLineRenderer>() != null)
        {
            gameObject.GetComponent<Rigidbody>().velocity *= releaseVelocity;
        }
    }

    //void OnCollisionExit() {
    //    if(this.gameObject.GetComponent<SphereCollider>().material.name != "Bounce")
    //    {
    //        gameObject.GetComponent<SphereCollider>().material = mat;
    //    }
    //}
}
