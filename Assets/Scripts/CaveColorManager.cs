using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaveColorManager : MonoBehaviour
{
    public static CaveColorManager Instance { get; private set; }
    public GameObject[] colorObjects;
    public string colorKeyPrefix = "caveObject_";
    private string currentSceneName;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        FindAndLoadColorObjects();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "cave")
        {
            currentSceneName = "cave";
            FindAndLoadColorObjects();
            LoadAllColors();
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void FindAndLoadColorObjects()
    {
        colorObjects = GameObject.FindGameObjectsWithTag("caveObject");
        for (int i = 0; i < colorObjects.Length; i++)
        {
            MeshRenderer renderer = colorObjects[i].GetComponent<MeshRenderer>();
            if (PlayerPrefs.HasKey(currentSceneName + "_" + colorKeyPrefix + i))
            {
                Color savedColor = LoadColor(i);
                renderer.material.color = savedColor;
            }
        }
    }

    public void SaveAllColors()
    {
        for (int i = 0; i < colorObjects.Length; i++)
        {
            MeshRenderer renderer = colorObjects[i].GetComponent<MeshRenderer>();
            SaveColor(i, renderer.material.color);
        }
    }

    public void LoadAllColors()
    {
        for (int i = 0; i < colorObjects.Length; i++)
        {
            MeshRenderer renderer = colorObjects[i].GetComponent<MeshRenderer>();
            renderer.material.color = LoadColor(i);
        }
    }

    public void UpdateColorChanges()
    {
        for (int i = 0; i < colorObjects.Length; i++)
        {
            GameObject obj = colorObjects[i];
            if (obj == null) continue;

            MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
            Color currentColor = renderer.material.color;
            Color savedColor = LoadColor(i);
            if (currentColor != savedColor)
            {
                SaveColor(i, currentColor);
            }
        }
    }

    private void SaveColor(int index, Color color)
    {
        PlayerPrefs.SetFloat(currentSceneName + "_" + colorKeyPrefix + index + "_r", color.r);
        PlayerPrefs.SetFloat(currentSceneName + "_" + colorKeyPrefix + index + "_g", color.g);
        PlayerPrefs.SetFloat(currentSceneName + "_" + colorKeyPrefix + index + "_b", color.b);
        PlayerPrefs.SetFloat(currentSceneName + "_" + colorKeyPrefix + index + "_a", color.a);
        PlayerPrefs.Save();
    }

    private Color LoadColor(int index)
    {
        float r = PlayerPrefs.GetFloat(currentSceneName + "_" + colorKeyPrefix + index + "_r", 1f);
        float g = PlayerPrefs.GetFloat(currentSceneName + "_" + colorKeyPrefix + index + "_g", 1f);
        float b = PlayerPrefs.GetFloat(currentSceneName + "_" + colorKeyPrefix + index + "_b", 1f);
        float a = PlayerPrefs.GetFloat(currentSceneName + "_" + colorKeyPrefix + index + "_a", 1f);
        return new Color(r, g, b, a);
    }

    private void OnApplicationQuit()
    {
        ResetColors();
    }

    public void ResetColors()
    {
        for (int i = 0; i < colorObjects.Length; i++)
        {
            PlayerPrefs.DeleteKey($"{currentSceneName}_{colorKeyPrefix}{i}_r");
            PlayerPrefs.DeleteKey($"{currentSceneName}_{colorKeyPrefix}{i}_g");
            PlayerPrefs.DeleteKey($"{currentSceneName}_{colorKeyPrefix}{i}_b");
            PlayerPrefs.DeleteKey($"{currentSceneName}_{colorKeyPrefix}{i}_a");
        }
        PlayerPrefs.Save();
    }
}
