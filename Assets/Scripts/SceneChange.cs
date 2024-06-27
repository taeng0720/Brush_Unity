using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public bool isCave=false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "warpZone" && isCave == false)
        {
            CaveScene();
        }

        if (other.gameObject.tag == "warpZone" && isCave == true)
        {
            GameScene();
        }

    }
    private void CaveScene()
    {
        gameObject.transform.position = new Vector3 (0, 1, 0);
        SceneManager.LoadScene(1);
        isCave = true;
    }
    private void GameScene()
    {
        gameObject.transform.position = new Vector3 (0,1,0);
        SceneManager.LoadScene(0);
        isCave = false;
    }
    
}
