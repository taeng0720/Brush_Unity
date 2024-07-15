using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    static GameObject container;

    private GetColor gc;

    static DataManager instance;
    public static DataManager Instance
    {
        get
        {
            if (!instance)
            {
                container = new GameObject();
                container.name = "DataManager";
                instance = container.AddComponent(typeof(DataManager)) as DataManager;
                DontDestroyOnLoad(container);
            }
            return instance;
        }
    }

    public Data data;

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
            gc = GameObject.Find("Player").GetComponent<GetColor>();
            for (int i = 0; gc.colorsUI.Length > i; i++)
            {
                gc.colorsUI[i].color = data.colors[i];
            }
        }
    }
    public void LoadGameData()
    {
        // �����͸� �ҷ��� ��� ����
        string path = Path.Combine(Application.dataPath, "Data.json");
        // ������ �ؽ�Ʈ�� string���� ����
        string jsonData = File.ReadAllText(path);
        // �� Json�����͸� ������ȭ�Ͽ� playerData�� �־���
        data = JsonUtility.FromJson<Data>(jsonData);
        
    }
    
    public void SaveGameData()
    {
        for (int i = 0; gc.colorsUI.Length > i; i++)
        {
            data.colors[i] = gc.colorsUI[i].color;
        }
        
        // ToJson�� ����ϸ� JSON���·� �����õ� ���ڿ��� �����ȴ�  
        string jsonData = JsonUtility.ToJson(data,true);
        // �����͸� ������ ��� ����
        string path = Path.Combine(Application.dataPath, "Data.json");
        // ���� ���� �� ����
        File.WriteAllText(path, jsonData);
    }
}

[Serializable]
public class Data
{
    public Color[] colors = new Color[] { Color.white, Color.white, Color.white};
}

