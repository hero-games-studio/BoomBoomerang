using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBox : BaseObstacle
{
    public float radius = 5;
    public ParticleSystem boomPlosion;

    private GameObject basePlane;
    void Awake()
    {
        basePlane=gameObject.transform.GetChild(0).gameObject;
        basePlane.transform.localScale = new Vector3(radius, 0.2f, radius);
        basePlane.transform.parent=null;
    }
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.gameObject.name);
        gameObject.GetComponent<SphereCollider>().enabled = false;
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Weapon" || collision.gameObject.tag == "Bomb")
        {
            performAction();
        }
    }

    private bool isExploded = false;
    public override void performAction()
    {
        if (!isExploded)
        {
            Invoke("InvokeDestroy",0.25f);
            isExploded = true;
            this.enabled = false;
            Debug.Log("actionPerformed");
            Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, radius);
            foreach (Collider collider in hitColliders)
            {
                BaseObstacle temp = collider.gameObject.GetComponent<BaseObstacle>();
                if (temp != null)
                {
                    temp.performAction();
                }
            }
        }
    }

    private void InvokeDestroy(){
        GameManagerScript.obstacleDestroyed();
        basePlane.GetComponent<MeshRenderer>().enabled=false;
        boomPlosion.Play();
        gameObject.GetComponent<MeshRenderer>().enabled=false;
    }
}

