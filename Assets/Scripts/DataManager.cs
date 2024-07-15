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
        // 데이터를 불러올 경로 지정
        string path = Path.Combine(Application.dataPath, "Data.json");
        // 파일의 텍스트를 string으로 저장
        string jsonData = File.ReadAllText(path);
        // 이 Json데이터를 역직렬화하여 playerData에 넣어줌
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
        // ToJson을 사용하면 JSON형태로 포멧팅된 문자열이 생성된다  
        string jsonData = JsonUtility.ToJson(data,true);
        // 데이터를 저장할 경로 지정
        string path = Path.Combine(Application.dataPath, "Data.json");
        // 파일 생성 및 저장
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

