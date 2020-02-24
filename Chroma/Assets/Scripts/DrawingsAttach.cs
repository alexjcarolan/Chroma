using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingsAttach : MonoBehaviour
{
    protected Transform stuckTo = null;
    protected Vector3 offset = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<GraphicsLineRenderer>() != null)
        {
            HingeJoint hj = GetComponent<HingeJoint>();
            hj.connectedBody = other.rigidbody;
        }
    }
}
