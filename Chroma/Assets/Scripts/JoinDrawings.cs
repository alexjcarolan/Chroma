using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinDrawings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        int i = 0;
        if (collision.collider.name == "drawing")
        { 
            print("COLLISION");
            this.gameObject.AddComponent<HingeJoint>().connectedBody = collision.gameObject.GetComponent<Rigidbody>();
            
            //var joint = this.GetComponent<FixedJoint>();
            //ContactPoint contactPoint = collision.GetContact(0);
            //joint.connectedAnchor = contactPoint.point;
            //joint.connectedBody = collision.collider.attachedRigidbody;
            //collision.collider.gameObject.GetComponent<FixedJoint>().connectedBody = this.GetComponent<Rigidbody>();


            //var joint = this.GetComponent<FixedJoint>();
            //joint.connectedBody = collision.collider.attachedRigidbody;
            //Transform temp = transform;
           // transform.parent = collision.collider.transform;
            //collision.collider.gameObject.GetComponent<RigidbodyConstraints>().
            //collision.collider.gameObject.AddComponent<Cons>
            //collision.collider.transform.parent = temp;
            //collision.collider.name = "drawing" + i.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if(transform.parent != null && transform.parent.name == "drawing")
        {
            string parentName = this.transform.parent.name;

            // GameObject go;
            // go = this.gameObject.
            if (parentName == "drawing")
            {
                transform.parent = this.transform;
               // Rigidbody rb = transform.parent.GetComponent<Rigidbody>();
               // Vector3 AbsoluteMovement = transform.position - transform.parent.position;
               // rb.constraints = RigidbodyConstraints.None;
               // rb.constraints =
              //  this.transform.parent.localPosition += AbsoluteMovement;
               // rb.constraints = RigidbodyConstraints.FreezeAll;
             //   print("HERE PARENT EXISTS");
                //transform.parent = transform;
            }*/
            

       // }
       // int childCount = this.transform.childCount;
       // while (childCount > 0)
       // {
       //     ChildMovement(childCount);
       //     childCount--;
       // }

    }

    /*void ChildMovement( int i )
    {
        GameObject go = GameObject.Find("drawing" + i.ToString());
        //this.GetComponent<Transform>();
        Transform x = GetComponent<Transform>();
        x = go.transform;
    }*/

    
}
