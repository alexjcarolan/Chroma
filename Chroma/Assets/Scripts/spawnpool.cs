using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnpool : MonoBehaviour
{
    private Vector3 planepos;
    public GameObject prefab;
    public static bool survive = false;
    private int waittime;
    private int waitedtime;
    private float timer;
    private int counter;
    private int noOfRocks = 25;
    public GameObject cameraPos;
    public GameObject teleportArea3;
    private float distance;
    public GameObject bridge3;


    // Start is called before the first frame update
    void Start()
    {
        planepos = this.transform.position;
        planepos.y += 0;
        objectspawn();
        waittime = 600;
        waitedtime = 0;

    }


    // Update is called once per frame
    void Update()
    {
        distance = Distance(cameraPos.transform.position, teleportArea3.transform.position);


        objectspawn();
    }

    void objectspawn()
    {

        if (waitedtime == waittime)
        {
            noOfRocks = (noOfRocks + 25) % 101;
            if (noOfRocks == 24)
                noOfRocks = 25;
            for (int j = 0; j < noOfRocks; j++)
            {
                GameObject t = Instantiate(prefab, planepos, Quaternion.identity) as GameObject;
                FindObjectOfType<AudioManager>().Play("pool_spawned");
                t.name = "Arrow(Clone)";
                t.transform.position = planepos + Random.insideUnitSphere * 5f;
                t.GetComponent<Transform>().rotation = Random.rotation;
                t.transform.localScale = Vector3.one * Random.Range(0.1f, 1f);
                Rigidbody rb = t.GetComponent<Rigidbody>();
                rb.AddForce(t.transform.localScale, ForceMode.Impulse);
                rb.velocity *= 0.7f;
                physics();
                waitedtime = 0;
                FindObjectOfType<AudioManager>().Play("pool_crash");
            }
            if (distance <= 1.5)
            {
                counter += 1;
                //print($"dist = {distance}");
                print($"counter : {counter}");
            }
            if (counter >= 4)
            {
                bridge3.GetComponent<Animator>().enabled = true;
                this.GetComponent<spawnpool>().enabled = false;
            }

            //print("counter is " + counter.ToString());
        }
        else
        {
            waitedtime += 1;
        }
    }

    void physics()
    {
        if (Physics.autoSimulation) return;

        timer += Time.deltaTime;

        while (timer >= Time.fixedDeltaTime)
        {
            timer -= Time.fixedDeltaTime;
            Physics.Simulate(Time.fixedDeltaTime);
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
