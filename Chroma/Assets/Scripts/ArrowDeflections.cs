using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDeflections : MonoBehaviour
{   
    public static float time = 0f;
    public bool start = true;
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
      if (time > 7*Time.fixedDeltaTime || start)
      {
        FindObjectOfType<AudioManager>().Play("arrow_collision");
        time = 0f;
        start = false;
      }
      time += Time.fixedDeltaTime;

        if(other.gameObject.GetComponent<GraphicsLineRenderer>() != null)
        {
            ArrowShooter.deflections += 1;
        }
    }
}
