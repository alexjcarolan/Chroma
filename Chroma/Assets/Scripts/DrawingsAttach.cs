using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DrawingsAttach : MonoBehaviour

{    // Start is called before the first frame update
    void Start()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Drawing")

        {
            if (gameObject.GetComponent<DrawingStruct>().startTime > collision.gameObject.GetComponent<DrawingStruct>().startTime)
            {
                gameObject.transform.SetParent(collision.gameObject.transform);
                gameObject.GetComponent<DrawingStruct>().parent = collision.gameObject;
                collision.gameObject.GetComponent<DrawingStruct>().children.Add(gameObject);
            }
            else
            {
                collision.gameObject.transform.SetParent(gameObject.transform);
                collision.gameObject.GetComponent<DrawingStruct>().parent = gameObject;
                gameObject.GetComponent<DrawingStruct>().children.Add(collision.gameObject);
            }
        }
    }

    void Update()
    {


    }
}