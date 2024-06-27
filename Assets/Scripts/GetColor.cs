using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetColor : MonoBehaviour
{
    [Header("Input")]
    public KeyCode getColorKey = KeyCode.R;
    public KeyCode applyColorKey = KeyCode.F;

    [Header("References")]
    [SerializeField] private float maxDistance = 10f;
    public Color currentColor;
    public Color defaultColor = new Color(128, 128, 128);
    public bool holdColor = false;
<<<<<<< HEAD
    private int selectColor = 1;
    public Image[] colorsUI;
=======
>>>>>>> parent of c42339e9 (Update)
    private void GetMaterial()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
<<<<<<< HEAD
        if(Physics.Raycast(ray,out hit,maxDistance) && hit.transform.gameObject.GetComponent<MeshRenderer>().material.color != defaultColor)
=======
        if(Physics.Raycast(ray,out hit))
>>>>>>> parent of c42339e9 (Update)
        {
            currentColor = hit.transform.gameObject.GetComponent<MeshRenderer>().material.color;
            hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = defaultColor;
            holdColor = true;
        }
    }
    private void ApplyColor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
<<<<<<< HEAD
        if (Physics.Raycast(ray, out hit,maxDistance) && colorsUI[selectColor-1].color != defaultColor)
=======
        if (Physics.Raycast(ray, out hit) && holdColor==true)
>>>>>>> parent of c42339e9 (Update)
        {
            hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = currentColor;
            holdColor=false;
        }
    }
    // Update is called once per frame
    void Update()
    {
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
