using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Startbtn()
    {
        SceneManager.LoadScene("GameScene");
        gameObject.GetComponent<FpsStatus>().enabled = true;
        gameObject.GetComponent<Setting>().enabled = true;
        DataManager.Instance.LoadGameData();
    }
    
    public void Savebtn()
    {
        DataManager.Instance.SaveGameData();
    }
    public void Loadbtn()
    {
        DataManager.Instance.LoadGameData();
    }
    private void OnApplicationQuit()
    {
        DataManager.Instance.SaveGameData();
    }
}
