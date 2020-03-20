using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBouncy : MonoBehaviour
{
    public PhysicMaterial mat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionExit() {
        if(this.gameObject.GetComponent<SphereCollider>().material.name != "Bounce")
        {
            gameObject.GetComponent<SphereCollider>().material = mat;
        }
    }
}
