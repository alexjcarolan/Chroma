using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFlame : MonoBehaviour
{
    public GameObject flame1;
    public GameObject flame2;
    public GameObject flame3;
    // Start is called before the first frame update
    void Start()
    {
        flame1.transform.gameObject.SetActive(false);
        flame2.transform.gameObject.SetActive(false);
        flame3.transform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        flame1.transform.gameObject.SetActive(false);
        flame2.transform.gameObject.SetActive(false);
        flame3.transform.gameObject.SetActive(false);
        if (ArrowShooter.deflections >= 1)
        {
            flame1.transform.gameObject.SetActive(true);
            //animations can be set permanently in the flame object
        }
        if (ArrowShooter.deflections >= 2)
        {
            flame2.transform.gameObject.SetActive(true);
        }
        if (ArrowShooter.deflections >= 3)
        {
            flame3.transform.gameObject.SetActive(true);
        }
    }
}
