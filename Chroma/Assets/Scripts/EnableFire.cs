using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableFire : MonoBehaviour
{
    public GameObject bridge1;
    public GameObject bridge2;
    private bool flag1;
    private bool flag2;
    public float lightStep = 0.2f;

    List<GameObject> stage2 = new List<GameObject>();
    List<GameObject> stage3 = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        
        for(int i = 0; i < children.Length; i++)
        {
            if(children[i].gameObject.tag == "Stage2Stuff")
            {
                stage2.Add(children[i].gameObject);
                children[i].gameObject.SetActive(false);
            }
            if (children[i].gameObject.tag == "Stage3Stuff")
            {
                stage3.Add(children[i].gameObject);
                children[i].gameObject.SetActive(false);
            }
        }
        flag1 = true;
        flag2 = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(bridge1.GetComponent<Animator>().enabled == true && flag1)
        {
            flag1 = false;
            foreach(GameObject go in stage2)
            {   
                if(go.GetComponent<Light>() != null)
                {
                    go.SetActive(true);
                    go.GetComponent<Light>().intensity = 0;
                    go.AddComponent<BrightenLight>();
                }
                else
                {
                    go.SetActive(true);
                }
            }
        }
        if (bridge2.GetComponent<Animator>().enabled == true && flag2)
        {
            flag2 = false;
            foreach (GameObject go in stage3)
            {
                if (go.GetComponent<Light>() != null)
                {
                    go.SetActive(true);
                    go.GetComponent<Light>().intensity = 0;
                    go.AddComponent<BrightenLight>();
                }
                else
                {
                    go.SetActive(true);
                }
            }
        }
    }
}
