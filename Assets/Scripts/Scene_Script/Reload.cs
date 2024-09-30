using UnityEngine;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("village");
        }
    }
}
