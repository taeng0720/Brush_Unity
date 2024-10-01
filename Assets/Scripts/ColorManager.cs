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
    public Color takenColor;  // 색이 뺏긴 경우로 설정할 색상

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
        LoadAllColors();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "village")
        {
            currentSceneName = "village";
            FindAndLoadColorObjects();
            LoadAllColors();
            Debug.Log("로드중");
        }
    }

    void OnDestroy()
    {
        // 씬이 변경되기 전에 색상 저장
        SaveAllColors();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void FindAndLoadColorObjects()
    {
        colorObjects = GameObject.FindGameObjectsWithTag("rock1_low");
        for (int i = 0; i < colorObjects.Length; i++)
        {
            MeshRenderer renderer = colorObjects[i].GetComponent<MeshRenderer>();

            // 색이 뺏겼는지 확인하는 불값 체크
            if (PlayerPrefs.HasKey($"{currentSceneName}_{colorKeyPrefix}{i}_isTaken"))
            {
                bool isColorTaken = PlayerPrefs.GetInt($"{currentSceneName}_{colorKeyPrefix}{i}_isTaken") == 1;

                if (isColorTaken)
                {
                    renderer.material.color = takenColor; // 색이 뺏겼다면 지정된 색으로 변경
                    Debug.Log(i + "번째 오브젝트는 색이 뺏겨서 지정된 색으로 설정됨");
                }
                else
                {
                    // 저장된 색이 있으면 불러옴
                    if (PlayerPrefs.HasKey($"{currentSceneName}_{colorKeyPrefix}{i}_r"))
                    {
                        Color savedColor = LoadColor(i);
                        renderer.material.color = savedColor;
                        Debug.Log(i + "번째 색상 로드 시도 중");
                    }
                }
            }
        }
    }

    public void SaveAllColors()
    {
        foreach (var obj in colorObjects)
        {
            if (obj == null) continue;
            MeshRenderer renderer = obj.GetComponent<MeshRenderer>();

            int index = System.Array.IndexOf(colorObjects, obj);
            SaveColor(index, renderer.material.color);

            // 현재 색상이 '뺏긴 색'인지 검사해서 저장
            bool isColorTaken = renderer.material.color == takenColor;
            PlayerPrefs.SetInt($"{currentSceneName}_{colorKeyPrefix}{index}_isTaken", isColorTaken ? 1 : 0);
        }
    }

    public void LoadAllColors()
    {
        for (int i = 0; i < colorObjects.Length; i++)
        {
            MeshRenderer renderer = colorObjects[i].GetComponent<MeshRenderer>();

            // 저장된 값이 있는지 확인하고, 없으면 패스
            if (PlayerPrefs.HasKey($"{currentSceneName}_{colorKeyPrefix}{i}_r"))
            {
                Color savedColor = LoadColor(i);
                if (savedColor != Color.clear) // 기본값이 아니면 로드
                {
                    Debug.Log(i + "번째 색상 로드 시도 중");
                    renderer.material.color = savedColor;
                }
            }
            else
            {
                Debug.Log(i + "저장된 값 없음");
            }
        }
    }

    private void SaveColor(int index, Color color)
    {
        Debug.Log(index + "번째 색상 저장 시도 중");
        PlayerPrefs.SetFloat($"{currentSceneName}_{colorKeyPrefix}{index}_r", color.r);
        PlayerPrefs.SetFloat($"{currentSceneName}_{colorKeyPrefix}{index}_g", color.g);
        PlayerPrefs.SetFloat($"{currentSceneName}_{colorKeyPrefix}{index}_b", color.b);
        PlayerPrefs.SetFloat($"{currentSceneName}_{colorKeyPrefix}{index}_a", color.a);
        PlayerPrefs.Save();
    }

    private Color LoadColor(int index)
    {
        Debug.Log(index + "번째 색상 로드 시도 중");

        // PlayerPrefs에 값이 없으면 Color.clear 반환
        if (!PlayerPrefs.HasKey($"{currentSceneName}_{colorKeyPrefix}{index}_r"))
        {
            Debug.Log("저장된 색상이 없음, 기본값으로 넘어감");
            return Color.clear;
        }

        // 색상 로드
        float r = PlayerPrefs.GetFloat($"{currentSceneName}_{colorKeyPrefix}{index}_r", 1f);
        float g = PlayerPrefs.GetFloat($"{currentSceneName}_{colorKeyPrefix}{index}_g", 1f);
        float b = PlayerPrefs.GetFloat($"{currentSceneName}_{colorKeyPrefix}{index}_b", 1f);
        float a = PlayerPrefs.GetFloat($"{currentSceneName}_{colorKeyPrefix}{index}_a", 1f);

        Debug.Log(index + "번째 색상 로드 완료");
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
            PlayerPrefs.DeleteKey($"{currentSceneName}_{colorKeyPrefix}{i}_isTaken");
        }
        PlayerPrefs.Save();
    }
}
