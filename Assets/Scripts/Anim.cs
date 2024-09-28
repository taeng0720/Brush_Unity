using UnityEngine;

public class Anim : MonoBehaviour
{
    [SerializeField]
    private GameObject[][] objectsToActivate;

    void Awake()
    {
        float currentProgress = ProgressManager.Instance.progress;

        for (int i = 0; i < objectsToActivate.Length; i++)
        {
            if (currentProgress >= (i + 1) * 20f)
            {
                ActivateObjects(i);
            }
        }
    }

    private void ActivateObjects(int index)
    {
        for (int i = 0; i <= index; i++)
        {
            foreach (GameObject obj in objectsToActivate[i])
            {
                obj.SetActive(true);
            }
        }
    }
}
