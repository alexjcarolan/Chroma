using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateBridge1 : MonoBehaviour
{

    public AnimationClip animClip;

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
        print("check!");
    }
}
