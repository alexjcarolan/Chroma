using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightenLight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Light>().intensity <= 1)
        {
            GetComponent<Light>().intensity += 0.15f * Time.deltaTime;
        }
    }
}
