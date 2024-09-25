using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

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

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneWithAnimation(string animationSceneName)
    {
        SceneManager.LoadScene(animationSceneName);
    }

    public void MoveToVillageOrCave(string targetScene)
    {
        float currentProgress = ProgressManager.Instance.progress;

        if (currentProgress >= 10 && currentProgress % 10 == 0)
        {
            LoadSceneWithAnimation("Animation");
        }
        else
        {
            LoadScene(targetScene);
        }
    }
}
