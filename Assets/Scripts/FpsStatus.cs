using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsStatus : MonoBehaviour
{
    private float deltaTime = 0f;
    [Range(10, 150)]
    public int fontSize = 30;
    public Color color = new Color(.0f, .0f, .0f, 1.0f);
    public float width, height;

    private void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.01f;
    }
    void OnGUI()
    {
        Rect position = new Rect(width, height, Screen.width, Screen.height);

        float fps = 1.0f / deltaTime;
        float ms = deltaTime * 1000f;
        string text = string.Format("{0:0.} FPS ({1:0.0}ms)", fps, ms);

        GUIStyle style = new GUIStyle();

        style.fontSize = fontSize;
        style.normal.textColor = color;

        GUI.Label(position, text, style);
    }
}
