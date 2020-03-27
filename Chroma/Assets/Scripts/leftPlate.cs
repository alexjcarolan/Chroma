using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftPlate : MonoBehaviour
{
    public static float mass = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void OnCollisionEnter(Collision collider)
    {
        if (collider.collider.tag == "Drawing")
        {
            mass += collider.collider.GetComponent<Rigidbody>().mass;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
