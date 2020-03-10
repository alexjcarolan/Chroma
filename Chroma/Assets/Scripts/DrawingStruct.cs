using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingStruct : MonoBehaviour
{
    public GameObject parent = null;
    public HashSet<GameObject> children = new HashSet<GameObject>();
    public float startTime = 0;
}