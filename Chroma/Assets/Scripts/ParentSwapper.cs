using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParentSwapper : MonoBehaviour
{
    private GameObject highlevelParent;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        List<GameObject> copy = DrawLineManager.drawings;
        if (DrawLineManager.drawingCount > 0)
        {
            foreach (Transform t in transform)
            {
                if (t.gameObject.tag == "Drawing")
                {
                    if (t.gameObject.GetComponent<DrawingStruct>().parent == null)
                    {
                        print("GRABBED DRAWING" + t.gameObject.name + " " + "parent init is : " + "null");
                        //REMOVE BELOW FOR LEVEL 2
                        if (SceneManager.GetActiveScene().name == "secondRoom")
                        {
                            t.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                            print("reached second room");
                            if (t.gameObject.GetComponent<FreezeCheck>() == null)
                            {
                                t.gameObject.AddComponent<FreezeCheck>();
                            }
                        }
                        // TILL HERE
                    }
                    else print("GRABBED DRAWING" + t.gameObject.name + " " + "parent init is : " + t.gameObject.GetComponent<DrawingStruct>().parent.name);
                    if (t.gameObject.GetComponent<DrawingStruct>().parent != null)
                    {
                        print("YOU ARE NOT GRABBING PARENT");
                        for (int i = 0; i < copy.Count; i++)
                        {
                            if (copy[i].GetComponent<DrawingStruct>().parent == null)
                            {
                                highlevelParent = copy[i];
                                //highlevelParent
                            }
                        }
                        GameObject ob = t.gameObject.GetComponent<DrawingStruct>().parent;
                        ob.GetComponent<DrawingStruct>().children.Remove(t.gameObject);
                        t.gameObject.GetComponent<DrawingStruct>().parent = null;
                        t.gameObject.transform.SetParent(null);
                        highlevelParent.transform.SetParent(t.gameObject.transform);
                        t.gameObject.GetComponent<DrawingStruct>().children.Add(highlevelParent);
                        //ob.transform.SetParent(t.gameObject.transform);



                    }
                }
            }

        }
        //print($"Children count is = {children.Count}");
    }
}
