using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneToLoad;

    private void OnMouseDown()
    {
        LoadScene();
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
