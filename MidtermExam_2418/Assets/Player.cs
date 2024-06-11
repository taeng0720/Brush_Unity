using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public GameObject Berrel;
    public GameObject Head;
    public float power;
    public float gaugeMax = 10f;
    public Transform BulletSp;
    public Slider PowerSlider;
    public GameObject Bullet;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateHead();
        Fire();
    }
    private void RotateHead()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            Head.transform.Rotate(new Vector3(0,0,0.1f));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Head.transform.Rotate(new Vector3(0, 0, -0.1f));
        }
    }
    public void setPower(float power)
    {
        PowerSlider.maxValue = gaugeMax;
        PowerSlider.value = power;
        if (Input.GetKey(KeyCode.Space))
        {
            PowerSlider.value++;
        }
    }
    private void Fire()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            GameObject myBullet = Instantiate(Bullet, BulletSp);
            myBullet.GetComponent<Rigidbody>().AddForce(transform.forward * power,ForceMode.Impulse);
        }
        
    }
}
