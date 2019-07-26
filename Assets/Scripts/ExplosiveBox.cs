using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBox : BaseObstacle
{
    public float radius = 5;
    public ParticleSystem boomPlosion;
    public ParticleSystem boomFire;
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
            performAction(collision.tag);
        }
    }
    private Collider[] collided;
    private bool isExploded = false;
    public override void performAction(string tag)
    {
        if (!isExploded)
        {
            boomFire.Play();
            Invoke("InvokeDestroy", 1.5f);
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
        boomPlosion.transform.parent = null;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        foreach (Collider collider in collided)
        {
            if (collider != null)
            {
                BaseObstacle temp = collider.gameObject.GetComponent<BaseObstacle>();
                if (temp != null)
                {
                    temp.performAction(this.tag);
                }
            }
        }
        Destroy(gameObject);
    }
}

