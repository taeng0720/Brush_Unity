using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinLauncher : MonoBehaviour
{
    [SerializeField] private GameObject pinObject;
    private Pin _currPin;
    // Start is called before the first frame update
    void Start()
    {
        PreparePin();
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && _currPin != null && !GameManager.instance.isGameOver)
        {
            _currPin.Lanunch();
            _currPin = null;
            Invoke(nameof(PreparePin),0.1f);
        }
    }
    void PreparePin()
    {
        if (!GameManager.instance.isGameOver)
        {
            GameObject pin = Instantiate(pinObject,transform.position,Quaternion.identity);
            _currPin = pin.GetComponent<Pin>();
        }
    }
}
