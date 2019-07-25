using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RockScript : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            Rigidbody weaponBody = collision.gameObject.GetComponent<Rigidbody>();
            WeaponScript.isCrashed = true;
            Invoke("returnToHand", 1f);
        }
    }

    private void returnToHand(Collision collision)
    {
        SceneManagerScript.incrementThrowCounter();
    }
}
