using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BalloonScript : BaseObstacle
{
    [SerializeField]
    private GameObject basePlane;

    [SerializeField]
    private ParticleSystem particleEffect;
    [SerializeField]
    private ParticleSystem explosionEffect;
    private bool isHit = false;

    void Start()
    {
        this.enabled = false;
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Weapon")
        {
            performAction();
        }
    }

    public override void performAction()
    {
        basePlane.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        if (particleEffect != null)
        {
            particleEffect.Play();
        }
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        GameManagerScript.obstacleDestroyed();
        Invoke("playDestructionParticle", 1.4f);
        Invoke("destroySelf", 2f);

        isHit = true;
        this.enabled = true;
    }

    private void playDestructionParticle()
    {

        gameObject.GetComponent<MeshRenderer>().enabled=false;
        explosionEffect.Play();
    }
    private void destroySelf()
    {
     //   Destroy(gameObject);
    }

    void FixedUpdate()
    {
        transform.position = transform.position + new Vector3(0, 0.1f, 0);
    }

}
