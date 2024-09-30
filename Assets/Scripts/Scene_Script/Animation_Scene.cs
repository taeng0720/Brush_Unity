using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Animation_Scene : MonoBehaviour
{
    public static string Scene_Name;
    public static Animation_Scene Instance { get; private set; }
    public bool isMove;

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
    void Update()
    {
        Scene_Move();
    }

    public void Scene_Move()
    {
        if (SceneManager.GetActiveScene().name == "Animation") // 현재 씬이 "Animation"인지 확인
        {
            StartCoroutine(SceneMoveAfterDelay(2f)); // 2초 뒤에 씬 전환

        }
    }

    private IEnumerator SceneMoveAfterDelay(float delay)
    {
        Debug.Log("왜 안 움직임2");
        yield return new WaitForSeconds(delay); // 대기 시간
        SceneManager.LoadScene(Scene_Name); // 씬 전환
    }
}
