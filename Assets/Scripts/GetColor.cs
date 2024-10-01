using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GetColor : MonoBehaviour
{
    public static GetColor Instance;
    [SerializeField] GameObject canvas;
    public KeyCode getColorKey = KeyCode.R;
    public KeyCode applyColorKey = KeyCode.F;
    public Color currentColor;
    public Color defaultColor = new Color(128 / 255f, 128 / 255f, 128 / 255f);
    public bool holdColor = false;
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private int selectColor = 1;
    public Image[] colorsUI;
    public ProgressManager progressManager;
    public Material assignedMaterial;
    public Material forbiddenMaterial;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(canvas);
        }
        else
        {
            Destroy(gameObject);
            Destroy(canvas);
        }
    }

    private void GetMaterial()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("ColorObject") && colorsUI[selectColor - 1].color == defaultColor)
            {
                MeshRenderer meshRenderer = hit.transform.gameObject.GetComponent<MeshRenderer>();
                if (meshRenderer.material != forbiddenMaterial)
                {
                    currentColor = meshRenderer.material.color;
                    meshRenderer.material = assignedMaterial;
                    TryStoreColorInInventory(currentColor);
                    TutorialManager.Instance.isGetColor = true;
                }
                else
                {
                    Debug.Log("이 매터리얼은 추출할 수 없습니다!");
                }
            }
        }
    }

    private void TryStoreColorInInventory(Color color)
    {
        if (colorsUI[selectColor - 1].color == defaultColor)
        {
            colorsUI[selectColor - 1].color = color;
            holdColor = true;
        }
        else
        {
            Debug.Log($"슬롯 {selectColor}에 이미 색상이 존재합니다!");
        }
    }

    private void SelectedColor()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) selectColor = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2)) selectColor = 2;
    }

    private void ApplyColor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("ApplyColorObject") && colorsUI[selectColor - 1].color != defaultColor)
            {
                MeshRenderer meshRenderer = hit.transform.gameObject.GetComponent<MeshRenderer>();
                if (meshRenderer.material.color != colorsUI[selectColor - 1].color)
                {
                    TutorialManager.Instance.isApplyColor = true;
                    meshRenderer.material.color = colorsUI[selectColor - 1].color;
                    colorsUI[selectColor - 1].color = defaultColor;
                    holdColor = false;
                    progressManager.IncreaseProgress(5f);
                    Debug.Log("Progress increased by 5.");
                }
            }
        }
    }

    private void Start()
    {
        for (int i = 0; i < colorsUI.Length; i++)
        {
            colorsUI[i].color = defaultColor;
        }
    }

    void Update()
    {
        SelectedColor();
        if (Input.GetKeyDown(getColorKey)) GetMaterial();
        if (Input.GetKeyDown(applyColorKey)) ApplyColor();
    }
}
