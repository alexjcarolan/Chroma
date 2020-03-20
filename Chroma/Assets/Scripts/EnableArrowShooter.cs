using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableArrowShooter : MonoBehaviour
{
    public GameObject cameraPos;
    private Vector3 teleportArea3 = new Vector3(4f, 0f, 5.75f);
    private float distance;
    private bool flag;

    // Start is called before the first frame update
    void Start()
    {
        flag = true;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Distance(cameraPos.transform.position, teleportArea3);
        if(distance <= 1.5f && flag)
        {
            GetComponent<ArrowShooter>().enabled = true;
            flag = false;
        }
    }

    float Distance(Vector3 a, Vector3 b)
    {
        float xDist = Mathf.Pow((a.x - b.x), 2);
        float zDist = Mathf.Pow((a.z - b.z), 2);
        float dist = Mathf.Sqrt(xDist + zDist);
        return dist;
    }
}
