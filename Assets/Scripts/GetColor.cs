using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetColor : MonoBehaviour
{
    [Header("Input")]
    public KeyCode getColorKey = KeyCode.R;
    public KeyCode applyColorKey = KeyCode.F;

    [Header("References")]
    public Color currentColor;
    public Color defaultColor = new Color(128 / 255f, 128 / 255f, 128 / 255f);
    public bool holdColor = false;
    [SerializeField] private float maxDistance = 10f;
    private int selectColor = 1;
    public Image[] colorsUI;
    public ProgressManager progressManager;

    private void GetMaterial()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("ColorObject") &&
                hit.transform.gameObject.GetComponent<MeshRenderer>().material.color != defaultColor)
            {
                currentColor = hit.transform.gameObject.GetComponent<MeshRenderer>().material.color;
                hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = defaultColor;
                ColorInventory();
            }
        }
    }

    private void SelectedColor()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) selectColor = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2)) selectColor = 2;
        if (Input.GetKeyDown(KeyCode.Alpha3)) selectColor = 3;
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

                progressManager.IncreaseProgress(1.5f);
                Debug.Log("Progress increased by 1.5");
            }
        }
    }

    private void ColorInventory()
    {
        if (selectColor >= 1 && selectColor <= colorsUI.Length)
        {
            colorsUI[selectColor - 1].color = currentColor;
            holdColor = true;
        }

        if (colorsUI[selectColor - 1].color != defaultColor)
        {
            holdColor = false;
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
