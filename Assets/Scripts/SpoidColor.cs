using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpoidColor : MonoBehaviour
{
    public GameObject colorObject;
    Vector3 mpos;

    void Update()
    {
        mpos = Input.mousePosition;
        if (Input.GetKeyDown(KeyCode.F))
            StartCoroutine(ScreenShotAndSpoid());

    }
    IEnumerator ScreenShotAndSpoid()
    {
        //스크린샷 후 그것을 Texture2D로 반환시키고 그곳에서 색을 추출한다.
        Texture2D tex = new Texture2D(Screen.width,Screen.height, TextureFormat.RGB24,false);
        yield return new WaitForEndOfFrame();
        tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        tex.Apply();

        Color color = tex.GetPixel((int)mpos.x, (int)mpos.y);
        colorObject.GetComponent<Renderer>().material.color = color;
    }
}
