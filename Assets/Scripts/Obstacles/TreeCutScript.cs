using UnityEngine;

public class TreeCutScript : BaseObstacle
{
    [SerializeField]
    private ParticleSystem destructionParticle;
    private bool isDestroyed = false;

    void OnTriggerEnter(Collider other)
    {
        performAction(other.tag);
    }

    public override void performAction(string tag)
    {
        if (tag == "Weapon" || tag == "Bomb")
        {
            destructionParticle.Play();
            destructionParticle.transform.parent = null;
            if (!isDestroyed)
            {
                GameManagerScript.obstacleDestroyed();
                Destroy(gameObject);
                isDestroyed = true;
            }
        }
    }
}
