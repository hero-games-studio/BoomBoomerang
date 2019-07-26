using UnityEngine;

public class DominoScript : BaseObstacle
{
    private Rigidbody[] rigidbodies;
    private MeshRenderer[] meshRenderers;
    private BoxCollider boxCollider;
    void Awake()
    {
        meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        boxCollider = gameObject.GetComponent<BoxCollider>();
        rigidbodies = gameObject.GetComponentsInChildren<Rigidbody>();
    }
    void OnTriggerEnter(Collider collision)
    {
        collisionPosition=collision.gameObject.transform.position;
        performAction(collision.tag);
    }

    private Vector3 collisionPosition;

    public override void performAction(string tag)
    {

        if (tag == "Weapon")
        {
            boxCollider.enabled = false;

            for (int i = 0; i < rigidbodies.Length; i++)
            {
                rigidbodies[i].useGravity = true;
                meshRenderers[i].material.SetVector("_color", new Vector4(0.8f, 0.8f, 0.8f, 1f));
            }
            GameManagerScript.obstacleDestroyed();
        }
        else if (tag == "Bomb")
        {
            boxCollider.enabled = false;

            for (int i = 0; i < rigidbodies.Length; i++)
            {
                rigidbodies[i].useGravity = true;
                rigidbodies[i].AddExplosionForce(500f,collisionPosition,1500f);
                meshRenderers[i].material.SetVector("_color", new Vector4(0.8f, 0.8f, 0.8f, 1f));
            }
            GameManagerScript.obstacleDestroyed();
        }
    }
}
