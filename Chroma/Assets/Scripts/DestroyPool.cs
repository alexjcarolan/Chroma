using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPool : MonoBehaviour
{

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<GraphicsLineRenderer>() == null)
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 3f);
        }
    }
}
