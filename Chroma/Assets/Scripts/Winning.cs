using System.Collections;

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Valve.VR.InteractionSystem;
using UnityEngine.SceneManagement;

public class Winning : MonoBehaviour
{

    public GameObject teleportCylinder;
    public string sceneName;
    public float threshold;
    Vector3 cylinderPos;
    public bool satisfied = false;
    public bool onlyonce = false;
    public AsyncOperation async;
    public Vector3 cameraPos;
    public static bool loaded = false;

    // Start is called before the first frame update
    void Start()
    {
        if (loaded == false)
        {
            cylinderPos = teleportCylinder.transform.position;
            StartCoroutine("NewScene");
            loaded = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        cameraPos = transform.position;
        //print($"Distance = {Distance(cameraPos, cylinderPos)}");
        if ((Distance(cameraPos, cylinderPos) < threshold) && satisfied == true && onlyonce == false)
        {
            print("HERE ONCE !");
            onlyonce = true;
            async.allowSceneActivation = true;
            //SteamVR_Fade.Start(Color.black, 1, false);
        }
    }

    float Distance(Vector3 a, Vector3 b)
    {
        float xDist = Mathf.Pow((a.x - b.x), 2);
        float zDist = Mathf.Pow((a.z - b.z), 2);
        float dist = Mathf.Sqrt(xDist + zDist);
        return dist;
    }

    private IEnumerator NewScene()
    {
        print("Inside func");
        async = SceneManager.LoadSceneAsync("centralRoom", LoadSceneMode.Single);
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            print("Loading progress " + async.progress * 100);

            if (async.progress >= 0.9f)
            {
                satisfied = true;
            }
            yield return null;
        }
    }
}