using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScript : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("scene loaded");
        SaveSystem.load();
    }
}
