using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    public GameObject menuPanel;
    public bool menuEnabled = false;
    private GameObject canvas;
    public Button exitBtn;
    public Button continueBtn;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
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
        if (scene.name == "GameScene")
        {
            canvas = GameObject.Find("Canvas");
            menuPanel = canvas.transform.GetChild(0).gameObject;
            exitBtn = menuPanel.transform.GetChild(0).gameObject.GetComponent<Button>();
            continueBtn = menuPanel.transform.GetChild(1).gameObject.GetComponent<Button>();
            GameManager.instance.gameObject.GetComponent<FpsStatus>().enabled = true;
            if (exitBtn != null && continueBtn != null)
            {
                exitBtn.onClick.AddListener(Exitbtn);
                continueBtn.onClick.AddListener(Continue);
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "GameScene")
        {
            if (Input.GetKeyDown(KeyCode.Escape) && menuEnabled == false)
            {
                menuPanel.SetActive(true);
                menuEnabled = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && menuEnabled == true)
            {
                Continue();
            }
        }
    }

    public void Exitbtn()
    {
        menuEnabled = false;
        SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Single);
        DataManager.Instance.SaveGameData();
    }
    public void Continue()
    {
        menuPanel.SetActive(false);
        menuEnabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
