using UnityEngine;

public class Anim_Prograss : MonoBehaviour
{
    public GameObject[] Postit;

    private ProgressManager progressManager;

    void Start()
    {
        progressManager = FindObjectOfType<ProgressManager>();
        if (progressManager == null)
        {
            Debug.LogError("ProgressManager not found!");
            return;
        }

        float progressFloat = progressManager.progress;

        foreach (GameObject postit in Postit)
        {
            postit.SetActive(false);
        }

        for (int i = 0; i < Postit.Length; i++)
        {
            if (progressFloat >= (i + 1) * 20)
            {
                Postit[i].SetActive(true);
            }
        }
    }
}
