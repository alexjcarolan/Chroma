using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDeflections : MonoBehaviour
{   

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
        if(other.gameObject.GetComponent<GraphicsLineRenderer>() != null)
        {
            ArrowShooter.deflections += 1;
        }
    }
}
