using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GetColor : MonoBehaviour
{
    public KeyCode getColorKey = KeyCode.R;
    public KeyCode applyColorKey = KeyCode.F;
    
    public Color currentColor;
    public Color defaultColor = new Color(128, 128, 128);
    public bool holdColor = false;

    private int selectColor = 1;
    public Image[] colorsUI;
    private void GetMaterial()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit) && hit.transform.gameObject.GetComponent<MeshRenderer>().material.color != defaultColor)
        {
            currentColor = hit.transform.gameObject.GetComponent<MeshRenderer>().material.color;
            hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = defaultColor;
            ColorInventory();
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
        if (Physics.Raycast(ray, out hit) && colorsUI[selectColor-1].color != defaultColor)
        {
            hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = colorsUI[selectColor-1].color;
            colorsUI[selectColor - 1].color = defaultColor;
            holdColor=false;
        }
    }
    private void ColorInventory()
    {
        switch (selectColor)
        {
            case 1:
                colorsUI[0].color = currentColor;
                holdColor = true;
                break;
            case 2:
                colorsUI[1].color = currentColor;
                holdColor = true;
                break;
            case 3:
                colorsUI[2].color = currentColor;
                holdColor = true;
                break;
        }
        if (colorsUI[selectColor-1].color != defaultColor) holdColor = false;
    }
    
    private void Start()
    {
        for(int i=0; i<colorsUI.Length;i++)
        {
            colorsUI[i].color = defaultColor;
        }
    }
    void Update()
    {
        SelectedColor();
        if(Input.GetKeyDown(getColorKey))
        {
            GetMaterial();
        }
        if(Input.GetKeyDown(applyColorKey))
        {
            ApplyColor();
        }
    }
}
