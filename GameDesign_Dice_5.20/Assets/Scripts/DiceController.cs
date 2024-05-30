using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    // Start is called before the first frame update
    private static Rigidbody rb;
    private static AudioSource source;
    public Vector3 diceVelocity;
    public bool isDroped;
    private DiceCheckZone checkZone;

    void Start()
    {
        checkZone = FindObjectOfType<DiceCheckZone>();
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        diceVelocity = rb.velocity;
        if(Input.GetKeyDown(KeyCode.Space) && isDroped)
        {
            GameManager.Instance.diceNumber = 0;
            float dirX = Random.Range(0, 500);
            float dirY = Random.Range(0, 500);
            float dirZ = Random.Range(0, 500);

            isDroped = false;
            source.Play();
            transform.position = new Vector3(0, 2, 0);
            transform.rotation = Quaternion.identity;
            rb.AddForce(Vector3.up * 400);
            rb.AddTorque(dirX, dirY, dirZ);

            checkZone.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
