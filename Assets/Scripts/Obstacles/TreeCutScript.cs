using UnityEngine;

public class TreeCutScript : BaseObstacle
{
    [SerializeField]
    private GameObject destructionParticle;
    [SerializeField]
    public GameObject breakable;

    private GameObject instanced;

    public GameObject cam;
    private bool isDestroyed = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Weapon")
        {
            instanced = Instantiate(breakable, transform.position, Quaternion.identity);
            Rigidbody[] rbs = instanced.GetComponentsInChildren<Rigidbody>();
            Vector3 spawnPoint = cam.transform.position - gameObject.transform.position;
            spawnPoint = spawnPoint / 10 + transform.position;
            Instantiate(destructionParticle, spawnPoint, Quaternion.identity);
            foreach (Rigidbody rb in rbs)
            {
                rb.AddExplosionForce(60, transform.position, 30);
            }
            if (!isDestroyed)
            {
                GameManagerScript.obstacleDestroyed();
                Destroy(gameObject);
                isDestroyed = true;
            }
        }
    }
}
