using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GetColor : MonoBehaviour
{
    public static GetColor Instance;
    [SerializeField]
    GameObject canvas;

    [Header("Input")]
    public KeyCode getColorKey = KeyCode.R;
    public KeyCode applyColorKey = KeyCode.F;

    [Header("References")]
    public Color currentColor;
    public Color defaultColor = new Color(128 / 255f, 128 / 255f, 128 / 255f);
    public bool holdColor = false;
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private int selectColor = 1;
    public Image[] colorsUI;
    public ProgressManager progressManager;

    [Header("UI")]
    public TextMeshProUGUI messageText;
    public RectTransform messagePanel;

    public Material assignedMaterial;

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
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("ColorObject") && colorsUI[selectColor - 1].color == defaultColor)
            {
                MeshRenderer meshRenderer = hit.transform.gameObject.GetComponent<MeshRenderer>();
                currentColor = meshRenderer.material.color;
                meshRenderer.material = assignedMaterial;
                TryStoreColorInInventory(currentColor);
            }
        }
    }

    private void TryStoreColorInInventory(Color color)
    {
        if (colorsUI[selectColor - 1].color != defaultColor)
        {
            ShowWarningMessage($"슬롯 {selectColor}에 이미 색상이 존재합니다!");
            return;
        }
        colorsUI[selectColor - 1].color = color;
        holdColor = true;
    }

    private void SelectedColor()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) selectColor = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2)) selectColor = 2;
    }

    private void ApplyColor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("ApplyColorObject") &&
                colorsUI[selectColor - 1].color != defaultColor &&
                hit.transform.gameObject.GetComponent<MeshRenderer>().material.color != colorsUI[selectColor - 1].color)
            {
                hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = colorsUI[selectColor - 1].color;
                colorsUI[selectColor - 1].color = defaultColor;
                holdColor = false;

                progressManager.IncreaseProgress(5f);
                Debug.Log("Progress increased by 5.");
            }
        }
    }

    private void CheckInventoryStatus()
    {
        if (colorsUI[0].color == defaultColor)
        {
            ShowWarningMessage("1번이 비어있어! 1번으로 바꿔서 색을 가져오자!");
        }
        else if (colorsUI[1].color == defaultColor)
        {
            ShowWarningMessage("2번이 비어있어! 2번으로 바꿔서 색을 가져오자!");
        }
        else if (IsInventoryFull())
        {
            ShowWarningMessage("전부 다 차있어! 동굴로 가서 색을 넣고 다시 오자!");
        }
    }

    private bool IsInventoryFull()
    {
        foreach (var colorSlot in colorsUI)
        {
            if (colorSlot.color == defaultColor)
            {
                return false;
            }
        }
        return true;
    }

    private void ShowWarningMessage(string message)
    {
        messageText.text = message;
        messagePanel.DOKill();
        messagePanel.DOAnchorPosY(0, 0.5f).OnComplete(() =>
        {
            StartCoroutine(HideMessageAfterDelay(1f));
        });
    }

    private IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        messagePanel.DOAnchorPosY(200, 0.5f);
    }

    private void Start()
    {
        for (int i = 0; i < colorsUI.Length; i++)
        {
            colorsUI[i].color = defaultColor;
        }
        messagePanel.anchoredPosition = new Vector2(0, 200);
    }

    void Update()
    {
        SelectedColor();
        if (Input.GetKeyDown(getColorKey))
        {
            GetMaterial();
        }
        if (Input.GetKeyDown(applyColorKey))
        {
            ApplyColor();
        }
    }
}
