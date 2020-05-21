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
            if (!FindObjectOfType<AudioManager>().flame1)
            {
              FindObjectOfType<AudioManager>().Play("flame_lit");
              FindObjectOfType<AudioManager>().flame1 = true;
            }

            flame1.transform.gameObject.SetActive(true);
            //animations can be set permanently in the flame object
        }
        if (ArrowShooter.deflections >= 2)
        {
            if (!FindObjectOfType<AudioManager>().flame2)
            {
              FindObjectOfType<AudioManager>().Play("flame_lit");
              FindObjectOfType<AudioManager>().flame2 = true;
            }

            FindObjectOfType<AudioManager>().Play("flame_lit");
            flame2.transform.gameObject.SetActive(true);
        }
        if (ArrowShooter.deflections >= 3)
        {
            if (!FindObjectOfType<AudioManager>().flame3)
            {
              FindObjectOfType<AudioManager>().Play("flame_lit");
              FindObjectOfType<AudioManager>().flame3 = true;
            }

            FindObjectOfType<AudioManager>().Play("flame_lit");
            flame3.transform.gameObject.SetActive(true);
        }
    }
}
