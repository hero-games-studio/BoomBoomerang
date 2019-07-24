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
            Rigidbody weaponBody =collision.gameObject.GetComponent<Rigidbody>();
            weaponBody.isKinematic=true;
            weaponBody.useGravity=true;
            WeaponScript.isCrashed = true;
            Invoke("reloadLevel", 2f);
        }
    }

    private void reloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
