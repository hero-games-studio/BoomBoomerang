using UnityEngine;
using UnityEngine.SceneManagement;

public class Impenetrable : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            Invoke("reloadLevel", 2f);
        }
    }

    private void reloadLevel()
    {
        Debug.Log("reloaded");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
