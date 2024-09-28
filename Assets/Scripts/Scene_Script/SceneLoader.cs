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

        if (currentProgress % 20 == 0 && currentProgress != 0)
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
        float currentProgress = ProgressManager.Instance.progress;
        if (currentProgress != 100)
        {
            if (other.gameObject.tag == "cave")
            {
                MoveToVillageOrCave("cave");
            }
            else if (other.gameObject.tag == "village")
            {
                MoveToVillageOrCave("village");
            }
        }
        else
        {
            //엔딩 로직 작성해야함
            MoveToVillageOrCave("ending");
        }

    }
}
