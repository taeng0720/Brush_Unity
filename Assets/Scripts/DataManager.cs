using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DataManager : MonoBehaviour
{
    static GameObject container;

    private GetColor gc;

    static DataManager instance;
    public static DataManager Instance;
    //{
    //    get
    //    {
    //        if (!instance)
    //        {
    //            container = new GameObject();
    //            container.name = "DataManager";
    //            instance = container.AddComponent(typeof(DataManager)) as DataManager;
    //            DontDestroyOnLoad(container);
    //        }
    //        return instance;
    //    }
    //}
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        

    }

    public Data data;

<<<<<<< HEAD
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
            gc = GameObject.FindWithTag("Player").GetComponent<GetColor>();
            LoadGameData();
        }
    }
=======
>>>>>>> parent of 91ba0372 (Update)
    public void LoadGameData()
    {
        // �����͸� �ҷ��� ��� ����
        string path = Path.Combine(Application.dataPath, "Data.json");
        // ������ �ؽ�Ʈ�� string���� ����
        string jsonData = File.ReadAllText(path);
        // �� Json�����͸� ������ȭ�Ͽ� playerData�� �־���
        data = JsonUtility.FromJson<Data>(jsonData);
        for (int i = 0; data.colors.Length > i; i++)
        {
            data.colors[i] = gc.colorsUI[i].color;
        }
    }

    public void SaveGameData()
    {
<<<<<<< HEAD
        //for (int i = 0; gc.colorsUI.Length > i; i++)
        //{
        //    data.colors[i] = gc.colorsUI[i].color;

        //}
        Debug.Log(gc);

=======
        for (int i = 0; gc.colorsUI.Length > i; i++)
        {
            data.colors[i] = gc.colorsUI[i].color.ToString();
        }
        
>>>>>>> parent of 91ba0372 (Update)
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
<<<<<<< HEAD
    public Color[] colors = new Color[] { Color.gray, Color.gray, Color.gray };
=======
    public string[] colors;
>>>>>>> parent of 91ba0372 (Update)
}

