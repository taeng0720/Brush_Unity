using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Button startBtn;
    public Button exitBtn;
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
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            gameObject.GetComponent<FpsStatus>().enabled = false;
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainScene")
        {
            // 메인 씬에서 버튼 찾기
            startBtn = GameObject.Find("Startbtn").GetComponent<Button>();
            exitBtn = GameObject.Find("Exitbtn").GetComponent<Button>();
            gameObject.GetComponent<FpsStatus>().enabled = false;
            if (startBtn != null && exitBtn != null)
            {
                startBtn.onClick.AddListener(Startbtn);
                exitBtn.onClick.AddListener(GameExit);
            }
        }
    }

    public void Startbtn()
    {
        SceneManager.LoadScene("GameScene");
        DataManager.Instance.LoadGameData();
    }
    
    public void GameExit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
    
    private void OnApplicationQuit()
    {
        DataManager.Instance.SaveGameData();
    }
}
