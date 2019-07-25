using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBox : BaseObstacle
{
    public float radius = 5;
    public ParticleSystem boomPlosion;
    public GameObject indicator;

    private GameObject basePlane;
    void Awake()
    {
        basePlane = gameObject.transform.GetChild(0).gameObject;
        basePlane.transform.localScale = new Vector3(radius, 0.2f, radius);
        basePlane.transform.parent = null;
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Weapon" || collision.gameObject.tag == "Bomb")
        {
            performAction();
        }
    }
    private Collider[] collided;
    private bool isExploded = false;
    public override void performAction()
    {
        if (!isExploded)
        {
            Invoke("InvokeDestroy", 2f);
            isExploded = true;
            this.enabled = false;
            collided = Physics.OverlapSphere(gameObject.transform.position, radius);
        }
    }

    private void InvokeDestroy()
    {
        indicator.GetComponent<MeshRenderer>().enabled = false;
        GameManagerScript.obstacleDestroyed();
        basePlane.GetComponent<MeshRenderer>().enabled = false;
        boomPlosion.Play();
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        foreach (Collider collider in collided)
        {
            BaseObstacle temp = collider.gameObject.GetComponent<BaseObstacle>();
            if (temp != null)
            {
                temp.performAction();
            }
        }
    }
}

