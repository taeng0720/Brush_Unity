using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetColor : MonoBehaviour
{
    public KeyCode getColorKey = KeyCode.R;
    public KeyCode applyColorKey = KeyCode.F;
    
    public Color currentColor;
    public Color defaultColor = new Color(128, 128, 128);
    public bool holdColor = false;
    private void GetMaterial()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit))
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
        if (Physics.Raycast(ray, out hit) && holdColor==true)
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
