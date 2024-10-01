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
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && menuEnabled == false)
        {
            if (SceneManager.GetActiveScene().name == "Main")
                return;
            menuPanel.SetActive(true);
            menuEnabled = true;
            Time.timeScale = 0;
            Cursor.visible = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && menuEnabled == true)
        {
            if (SceneManager.GetActiveScene().name == "Main")
                return;
            Continue();
        }
        if (menuEnabled)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void Exitbtn()
    {
        menuEnabled = false;
        CaveColorManager.Instance.ResetColors();
        ColorManager.Instance.ResetColors();
        Continue();
        DeleteAllObjects();
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Main");

    }
    public void Continue()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        menuPanel.SetActive(false);
        menuEnabled = false;
        Cursor.visible = false;
    }
    void DeleteAllObjects()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            Destroy(obj);
        }
    }
}
