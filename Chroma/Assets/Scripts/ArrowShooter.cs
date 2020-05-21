using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    public float timeSpan;
    public float velocity;
    public float launchAngle;
    public GameObject arrow;
    public GameObject bridge2;
    public GameObject totemFire;
    public GameObject totemPole;

    public static int deflections = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shoot());   
    }

    void Update()
    {
        
    }

    // Function to shoot the arrows
    private IEnumerator Shoot()
    {
        while(true)
        {
            FindObjectOfType<AudioManager>().Play("arrow_shoot");
            yield return new WaitForSeconds(timeSpan);
            Vector3 launchDirection = GetLaunchDirection();
            Quaternion q = Quaternion.Euler(launchDirection);
            float randomness = Random.Range(velocity, velocity + 1000);

            GameObject spawned = Instantiate(arrow, transform.position, q) as GameObject;
            arrow.name = "Arrow";
            spawned.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 1) * randomness);
            Destroy(spawned, 2f);
            //arrow.GetComponent<Rigidbody>().velocity = arrowVelocity;
            if(deflections >= 3)
            {
                    bridge2.GetComponent<Animator>().enabled = true;
                totemFire.SetActive(true);
                totemPole.GetComponent<Animator>().enabled = true;

                break;
            }
        }
    }

    // Function to get the direction of launch
    // Essentially so that the arrows are not always coming in a straight line, adding some randomness to the shooting
    private Vector3 GetLaunchDirection()
    {   
        // Rotate around the Y axis by 180, a random number around the Z axis between 0 and 180, around the x axis by an angle that we specify.
        return new Vector3(Random.Range(-launchAngle, launchAngle), 0, Random.Range(0f, 100f));
    }
}
