using UnityEngine;
using System.Collections;
using System;

public class ScreenCapture : MonoBehaviour
{
    public RenderTexture overviewTexture;
    public GameObject OVcamera;
    public string path = "";

    void Start()
    {

        InvokeRepeating("TakeScreenShot", 2f, 10f);
        //OVcamera = GameObject.FindGameObjectWithTag("DetectorCamera");
    }

    void LateUpdate()
    {
       if (CircleDetector.flag == true)
        {
            GetComponent<ScreenCapture>().enabled = false;
        }
    }

    // return file name
    string fileName(int width, int height)
    {
        return string.Format("screenshot.png",
                              width, height,
                              System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    public void TakeScreenShot()
    {
        //yield return new WaitForEndOfFrame();
        print("Called");

        Camera camOV = OVcamera.GetComponent<Camera>();
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = camOV.targetTexture;
        camOV.Render();
        Texture2D imageOverview = new Texture2D(camOV.targetTexture.width, camOV.targetTexture.height/2, TextureFormat.RGB24, false);
        imageOverview.ReadPixels(new Rect(0, 0, camOV.targetTexture.width, camOV.targetTexture.height/2), 0, 0);
        imageOverview.Apply();
        RenderTexture.active = currentRT;

        // Encode texture into PNG
        byte[] bytes = imageOverview.EncodeToPNG();

        // save in memory
        string filename = fileName(Convert.ToInt32(imageOverview.width), Convert.ToInt32(imageOverview.height));
        //path = Application.persistentDataPath + "/Snapshots/" + filename;
        path = "C:/Users/Chroma/CHROMA/Snapshots/" + filename;
        System.IO.File.WriteAllBytes(path, bytes);

        CircleDetector.CallFunction();
    }
}