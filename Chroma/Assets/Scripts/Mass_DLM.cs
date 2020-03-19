using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mass_DLM : MonoBehaviour
{
    public float massVal;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setMass(){
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        GetComponent<Rigidbody>().mass = (GetComponent<GraphicsLineRenderer>().vertCount/100);
        massVal = GetComponent<Rigidbody>().mass;
        print("Mass is " + massVal);
    }


}
