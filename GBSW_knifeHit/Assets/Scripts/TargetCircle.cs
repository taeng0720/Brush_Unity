using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCircle : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = -30f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if(!GameManager.instance.isGameOver)
        {
            transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        }
        
    }
}
