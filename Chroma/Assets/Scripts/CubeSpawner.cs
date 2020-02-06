using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public float timeSpan;
    public float velocity;
    public float launchAngle;
    public GameObject ball;
    public GameObject bridge;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shoot());
    }

    // Function to shoot the arrows
    private IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeSpan);
            Vector3 launchDirection = GetLaunchDirection();
            Quaternion q = Quaternion.Euler(launchDirection);

            GameObject spawned = Instantiate(ball, transform.position, q) as GameObject;
            ball.name = "Ball";
            spawned.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 1) * velocity);
            Destroy(spawned, 6f);
            //arrow.GetComponent<Rigidbody>().velocity = arrowVelocity;
            
            if(bridge.GetComponent<Animator>().enabled == true)
            {   
                break;
            }

        }
    }

    // Function to get the direction of launch
    // Essentially so that the arrows are not always coming in a straight line, adding some randomness to the shooting
    private Vector3 GetLaunchDirection()
    {
        // Rotate around the Y axis by 180, a random number around the Z axis between 0 and 180, around the x axis by an angle that we specify.
        return new Vector3(Random.Range(-launchAngle, launchAngle), 0, 0);
    }
}
