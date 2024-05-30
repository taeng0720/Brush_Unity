using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    Animator animator;
    public string enemyType;
    public int enemyHealth;
    public DiceController DiceController;
    GameObject dice;
    

    void OnEnable()
    {
        dice = GameObject.Find("dice");
        DiceController=dice.GetComponent<DiceController>();
        
        animator = GetComponent<Animator>();
        GameManager.Instance.enemyHp = enemyHealth;
        switch (enemyType)
        {
            case "A":
                enemyHealth = 8;
                break;
            case "B":
                enemyHealth = 6;
                break;
            case "C":
                enemyHealth = 10;
                break;
        }
        
    }
    
    void EnemyDefeated()
    {
        gameObject.SetActive(false);
        GameManager.Instance.objectPool.Add(gameObject);
        GameManager.Instance.isSpawn = false;
        GameManager.Instance.Score++;
    }
    void Update()
    {        
        if (GameManager.Instance.isSpawn && DiceController.isDroped)
        {
            enemyHealth -= GameManager.Instance.diceNumber;

            if (enemyHealth <= 0)
            {
                EnemyDefeated();
            }
            
        }
        if (GameManager.Instance.clearTime <= 0 && GameManager.Instance.isSpawn)
        {
            animator.SetBool("Atk",true);

            GameManager.Instance.isGameOver = true;
        }
    }
    

}
