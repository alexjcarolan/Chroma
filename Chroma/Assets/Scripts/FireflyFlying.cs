using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))] //require rigid body

public class FireflyFlying : MonoBehaviour
{
    [SerializeField] float idleSpeed, turnSpeed, switchSeconds, ascentDescentLimit;
    [SerializeField] Vector2 changeTargetEveryFromTo;
    [SerializeField] Transform homeTarget, flyingTarget;
    [SerializeField] Vector2 radiusMinMax;
    [SerializeField] Vector2 yMinMax;
    [SerializeField] public bool returnToBase = false;
    [SerializeField] public float randomOffset = 2, delayStart = 0f;

    private Rigidbody body;
    [System.NonSerialized] float changeTarget, timeSinceTarget, prevSpeed, speed, zturn, prevz, turnSpeedBackup;
    private Vector3 rotateTarget, position, direction, velocity, randomizedBase, direction1;
    private Quaternion lookRotation;
    [System.NonSerialized] public float distanceFromBase, distanceFromTarget;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        turnSpeed = turnSpeedBackup;
        direction = Quaternion.Euler(transform.eulerAngles) * (Vector3.forward); //direction facing
        if (delayStart <= 0f) body.velocity = idleSpeed * direction;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (delayStart > 0f)
        {
            delayStart -= Time.fixedDeltaTime;
            return;
        }
        distanceFromBase = Vector3.Magnitude(randomizedBase - body.position);
        distanceFromTarget = Vector3.Magnitude(flyingTarget.position - body.position);

        if (returnToBase && distanceFromBase < 10f)
        {
            if (turnSpeed != 300f && body.velocity.magnitude != 0f)
            {
                turnSpeedBackup = turnSpeed;
                turnSpeed = 300f;
            }
            else if (distanceFromBase <= 1f)
            {
                body.velocity = Vector3.zero;
                turnSpeed = turnSpeedBackup;
                return;

            }
        }

        speed = idleSpeed;

        // new target position
        if (changeTarget <= 0f)
        {
            rotateTarget = ChangeDirection(body.transform.position);
            if (returnToBase) changeTarget = 0.2f; else changeTarget = Random.Range(changeTargetEveryFromTo.x, changeTargetEveryFromTo.y);
            timeSinceTarget = 0f;
        }
        // height limit turn
        if (body.transform.position.y < yMinMax.x + 10f || body.transform.position.y > yMinMax.y - 10f)
        {
            if (body.transform.position.y < yMinMax.x + 10f) rotateTarget.y = 1f; else rotateTarget.y = -1f;
        }

        zturn = Mathf.Clamp(Vector3.SignedAngle(rotateTarget, direction, Vector3.up), -45f, 45f);

        //update times
        changeTarget -= Time.fixedDeltaTime;
        timeSinceTarget += Time.fixedDeltaTime;

        // rotate torwards target
        if (rotateTarget != Vector3.zero) lookRotation = Quaternion.LookRotation(rotateTarget, Vector3.up);
        Vector3 rotation = Quaternion.RotateTowards(body.transform.rotation, lookRotation, turnSpeed * Time.fixedDeltaTime).eulerAngles;
        body.transform.eulerAngles = rotation;

        float temp = prevz;
        if (prevz < zturn) prevz += Mathf.Min(turnSpeed * Time.fixedDeltaTime, zturn - prevz);
        else if (prevz >= zturn) prevz -= Mathf.Min(turnSpeed * Time.fixedDeltaTime, prevz - zturn);
        prevz = Mathf.Clamp(prevz, -45f, 45f);
        body.transform.Rotate(0f, 0f, prevz - temp, Space.Self);

        // move
        direction1 = Quaternion.Euler(transform.eulerAngles) * Vector3.forward;
        direction = Quaternion.Euler(transform.eulerAngles) * rotateTarget;
        if (returnToBase && distanceFromBase < idleSpeed)
        {
            body.velocity = Mathf.Min(idleSpeed, distanceFromBase) * direction1;
        }

        if (returnToBase) body.velocity = Mathf.Lerp(prevSpeed, speed, Mathf.Clamp(switchSeconds, 0f, 1f)) * direction1;

        body.velocity = Mathf.Lerp(prevSpeed, speed, Mathf.Clamp(switchSeconds, 0f, 1f)) * direction;

        if (body.transform.position.y < yMinMax.x || body.transform.position.y > yMinMax.y)
        {
            position = body.transform.position;
            position.y = Mathf.Clamp(position.y, yMinMax.x, yMinMax.y);
            //position.x = Mathf.Clamp(position.x, yMinMax.x, yMinMax.y);
            body.transform.position = position;
        }

        prevSpeed = speed;
    }

    private Vector3 ChangeDirection(Vector3 currentPosition)
    {
        Vector3 newDir;
        if (returnToBase)
        {
            randomizedBase = homeTarget.position;
            randomizedBase.y += Random.Range(-randomOffset, randomOffset);
            newDir = randomizedBase - currentPosition;
        }
        else if (distanceFromTarget > radiusMinMax.y)
        {
            newDir = flyingTarget.position - currentPosition;
        }
        else if (distanceFromTarget < radiusMinMax.x)
        {
            newDir = currentPosition - flyingTarget.position;
        }
        else
        {
            float angleXZ = Random.Range(-Mathf.PI, Mathf.PI); //on horizontal plane
            float angleY = Random.Range(-Mathf.PI / ascentDescentLimit, Mathf.PI / ascentDescentLimit); // steepness of ascent and descent

            newDir = Mathf.Sin(angleXZ) * Vector3.forward + Mathf.Cos(angleXZ) * Vector3.right + Mathf.Sin(angleY) * Vector3.up;
        }




        return newDir.normalized;
    }
}
