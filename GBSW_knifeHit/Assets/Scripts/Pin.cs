using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    private bool _isPinned = false;
    private bool _isLaunched = false;
    
    
    private void FixedUpdate()
    {
        if(!_isPinned && _isLaunched)
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        }
           
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _isPinned = true;
        if (other.gameObject.CompareTag("Target"))
        {
            GameObject childObject = transform.GetChild(0).gameObject;
            SpriteRenderer childSprite = childObject.GetComponent<SpriteRenderer>();
            childSprite.enabled = true;
            transform .SetParent(other.gameObject.transform);

            GameManager.instance.DecreaseGoal();
        }
        else if (other.gameObject.CompareTag("Pin"))
        {
            GameManager.instance.SetGameOver(false);
        }
    }

    public void Lanunch()
    {
        _isLaunched = true;
    }
}
