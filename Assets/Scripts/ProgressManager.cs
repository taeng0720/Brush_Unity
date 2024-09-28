using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager Instance { get; private set; }
    public float progress { get; private set; }
    public float maxProgress = 100f;
    [SerializeField]
    private GameObject Managers;

    [Header("Post-processing Settings")]
    public PostProcessVolume postProcessVolume;
    private ColorGrading colorGradingLayer;

    [Header("Inspector Progress Display")]
    public float inspectorProgress;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Managers);
        }
        else
        {
            Destroy(Managers);
        }
    }

    private void Start()
    {
        inspectorProgress = progress;
    }

    private void Update()
    {
        if (inspectorProgress != progress)
        {
            progress = inspectorProgress;
        }
    }

    public void IncreaseProgress(float amount)
    {
        progress += amount;
        if (progress > maxProgress)
        {
            progress = maxProgress;
        }
        Debug.Log($"Progress increased to: {progress}");
    }

    public void ResetProgress()
    {
        progress = 0f;
    }

    public float GetProgress()
    {
        return progress;
    }
}
