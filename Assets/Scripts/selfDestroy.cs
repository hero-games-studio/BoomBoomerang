using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfDestroy : MonoBehaviour
{
    public float selfDestyorTimer=2f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("destroySelf", 5f);
    }
    private void destroySelf()
    {
        Destroy(gameObject);
    }
}
