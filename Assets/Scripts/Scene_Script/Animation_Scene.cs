using UnityEngine;
using UnityEngine.SceneManagement;
public class Animation_Scene : MonoBehaviour
{
    public static string Scene_Name;
    public static Animation_Scene Instance { get; private set; }
    private void Awake()
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
    public void Scene_Move()
    {
        SceneManager.LoadScene(Scene_Name);
    }
}