using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatePivot : MonoBehaviour
{
    Vector3 pivot = new Vector3(-0.0193842f, 0.8905377f, 0.03f);
    
    float angle;
    float rightPlateMass = 100f;
    // Start is called before the first frame update
    void Start()
    {
        Transform transform = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float diff = rightPlateMass - leftPlate.mass;
        // update angle

        transform.Rotate(pivot, angle);
    }
}
