using UnityEngine;
public class RockScript : MonoBehaviour
{

    void Awake()
    {
        Invoke("InvokeEnableThis", 0.2f);
    }

    private void InvokeEnableThis()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            Rigidbody weaponBody = collision.gameObject.GetComponent<Rigidbody>();
            WeaponScript.isCrashed = true;
            SceneManagerScript.incrementThrowCounter();
        }
    }
}
