using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
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

        if (currentProgress % 10 == 0 && currentProgress != 0)
        {
            LoadSceneWithAnimation("Animation");
            Animation_Scene.Scene_Name = targetScene;
        }
        else
        {
            LoadScene(targetScene);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "cave")
        {
            MoveToVillageOrCave("cave");
        }
        else if (other.gameObject.tag == "Village")
        {
            MoveToVillageOrCave("Village");
        }

    }
}
