using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class DestroyPool : MonoBehaviour
{
    public static float time = 0f;
    public bool start = true;

    private void OnCollisionEnter(Collision other)
    {
        if (time > 200* Time.fixedDeltaTime || start)
        {
          //FindObjectOfType<AudioManager>().Play("pool_crash");
          time  = 0f;
          start = false;
        }
        if (other.gameObject.GetComponent<GraphicsLineRenderer>() == null && other.gameObject.GetComponent<DestroyPool>() == null)
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 3f);
        }
    }
}
