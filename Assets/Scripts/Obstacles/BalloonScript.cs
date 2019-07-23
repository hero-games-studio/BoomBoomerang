using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BalloonScript : BaseObstacle
{
    private MeshRenderer mr;
    [SerializeField]
    private ParticleSystem particleEffect;
    private bool isHit = false;

    void Start()
    {
        mr = gameObject.GetComponent<MeshRenderer>();
        Debug.Log(transform.childCount);
        float r = Random.Range(0.3f, 1f);
        float g = Random.Range(0.3f, 1f);
        float b = Random.Range(0.3f, 1f);
        mr.material.SetVector("_color", new Vector4(r, g, b, 1f));
        this.enabled = false;
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Weapon")
        {
            if (particleEffect != null)
                particleEffect.Play();
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            GameManagerScript.obstacleDestroyed();
            gameObject.AddComponent<selfDestroy>();
            isHit = true;
            this.enabled = true;
        }
    }

    void FixedUpdate()
    {
        transform.position = transform.position + new Vector3(0, 0.1f, 0);
    }

}
