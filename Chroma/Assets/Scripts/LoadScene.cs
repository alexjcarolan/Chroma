using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NewScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerable NewScene()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync("MainRoom", LoadSceneMode.Single);
        async.allowSceneActivation = false;
        while(async.progress <= 0.89f)
        {
            print(async.progress.ToString());
            yield return null;
        }
        async.allowSceneActivation = true;
    }
}
