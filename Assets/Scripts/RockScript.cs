using UnityEngine;
public class RockScript : MonoBehaviour
{
    public ParticleSystem crashParticle;
    private BoxCollider bCollider;
    void Start(){
        bCollider=gameObject.GetComponent<BoxCollider>();
        Invoke("InvokeColliderEnabling",0.25f);
    }

    void InvokeColliderEnabling(){
        bCollider.enabled=true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            Debug.Log(gameObject.name);
            Debug.Log("crash");
            crashParticle.transform.position = collision.contacts[0].point;
            crashParticle.Play();
            Rigidbody weaponBody = collision.gameObject.GetComponent<Rigidbody>();
            WeaponScript.isCrashed = true;
            SceneManagerScript.incrementThrowCounter();
        }
    }
}
