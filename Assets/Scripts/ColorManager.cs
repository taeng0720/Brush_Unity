using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorManager : MonoBehaviour
{
    public static ColorManager Instance { get; private set; }
    public GameObject[] colorObjects;
    public string colorKeyPrefix = "rock1_low";
    public Material defaultMaterial;

    private string currentSceneName;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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
        if (scene.name == "village")
        {
            currentSceneName = "village";
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
        colorObjects = GameObject.FindGameObjectsWithTag("rock1_low");
        for (int i = 0; i < colorObjects.Length; i++)
        {
            MeshRenderer renderer = colorObjects[i].GetComponent<MeshRenderer>();
            if (PlayerPrefs.HasKey(currentSceneName + "_" + colorKeyPrefix + i))
            {
                renderer.material.color = LoadColor(i);
            }
            else
            {
                renderer.material = defaultMaterial;
            }
        }
    }

    public void SaveAllColors()
    {
        foreach (var obj in colorObjects)
        {
            if (obj == null) continue;
            MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
            SaveColor(System.Array.IndexOf(colorObjects, obj), renderer.material.color);
        }
    }

    public void LoadAllColors()
    {
        for (int i = 0; i < colorObjects.Length; i++)
        {
            MeshRenderer renderer = colorObjects[i].GetComponent<MeshRenderer>();
            Color savedColor = LoadColor(i);
            renderer.material = savedColor.a == 0 ? defaultMaterial : renderer.material;
            renderer.material.color = savedColor.a == 0 ? defaultMaterial.color : savedColor;
        }
    }

    public void UpdateColorChanges()
    {
        for (int i = 0; i < colorObjects.Length; i++)
        {
            if (colorObjects[i] == null) continue;
            MeshRenderer renderer = colorObjects[i].GetComponent<MeshRenderer>();
            Color currentColor = renderer.material.color;
            Color savedColor = LoadColor(i);
            if (currentColor != savedColor) SaveColor(i, currentColor);
        }
    }

    private void SaveColor(int index, Color color)
    {
        PlayerPrefs.SetFloat($"{currentSceneName}_{colorKeyPrefix}{index}_r", color.r);
        PlayerPrefs.SetFloat($"{currentSceneName}_{colorKeyPrefix}{index}_g", color.g);
        PlayerPrefs.SetFloat($"{currentSceneName}_{colorKeyPrefix}{index}_b", color.b);
        PlayerPrefs.SetFloat($"{currentSceneName}_{colorKeyPrefix}{index}_a", color.a);
        PlayerPrefs.Save();
    }

    private Color LoadColor(int index)
    {
        float r = PlayerPrefs.GetFloat($"{currentSceneName}_{colorKeyPrefix}{index}_r", 1f);
        float g = PlayerPrefs.GetFloat($"{currentSceneName}_{colorKeyPrefix}{index}_g", 1f);
        float b = PlayerPrefs.GetFloat($"{currentSceneName}_{colorKeyPrefix}{index}_b", 1f);
        float a = PlayerPrefs.GetFloat($"{currentSceneName}_{colorKeyPrefix}{index}_a", 1f);
        return new Color(r, g, b, a);
    }

    private void OnApplicationQuit()
    {
        ResetColors();
    }

    private void ResetColors()
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
