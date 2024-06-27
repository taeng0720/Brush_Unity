using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Setting : MonoBehaviour
{
    private GameObject canvas;
    private GameObject settingPanel;
    private Button exitBtn;
    public bool settingEnabled = false;
    private void Update()
    {
        if (gameObject.GetComponent<Setting>().enabled == true)
        {
            canvas = GameObject.Find("Canvas");
            settingPanel = canvas.transform.GetChild(0).gameObject;
            exitBtn = settingPanel.transform.GetChild(0).gameObject.GetComponent<Button>();
            exitBtn.onClick.AddListener(Exitbtn);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && settingEnabled==false)
        {
            settingPanel.SetActive(true);
            settingEnabled = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && settingEnabled==true)
        {
            settingPanel.SetActive(false);
            settingEnabled = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
   
    
    public void Exitbtn()
    {
        SceneManager.LoadScene("MainScene");
        gameObject.GetComponent<FpsStatus>().enabled = false;
        DataManager.Instance.SaveGameData();
    }
}
