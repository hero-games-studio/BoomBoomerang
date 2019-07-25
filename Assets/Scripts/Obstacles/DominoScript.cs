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
        if (collision.tag == "Weapon")
        {
            performAction();
        }
    }

    public override void performAction()
    {

        boxCollider.enabled = false;

        for (int i = 0; i < rigidbodies.Length; i++)
        {
            rigidbodies[i].useGravity = true;
            meshRenderers[i].material.SetVector("_color", new Vector4(0.8f, 0.8f, 0.8f, 1f));
        }
        GameManagerScript.obstacleDestroyed();
    }
}
