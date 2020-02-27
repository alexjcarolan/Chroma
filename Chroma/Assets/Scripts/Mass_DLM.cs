using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mass_DLM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void setMass(GameObject drawing ){
        drawing.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        print("Mass is " + drawing.GetComponent<GraphicsLineRenderer>().vertCount);
        //drawing.GetComponent<Rigidbody>().mass = drawing.GetComponent<GraphicsLineRenderer>().vertCount/10000;
        drawing.GetComponent<Rigidbody>().mass = 1;

    }


}
