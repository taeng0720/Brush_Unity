using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Text diceNumberText;
    public int diceNumber;
    int currentScore = 0;
    public GameObject[] objectFactorys;
    public int poolSize = 20;
    public Transform spawnPoint;
    public List<GameObject> objectPool;
    public bool isGameOver;
    float currentTime;
    public float minTime = 0.5f;
    public float maxTime = 1.5f;
    public float createTime = 1;
    public float clearTime = 30;
    public bool isClear;
    public Text timeText;
    public Text currentScoreText;
    
    public bool isSpawn;

    
    public int enemyHp;
    public Text enemyHpText;
    public int EnemyHealth
    {
        get
        {
            return enemyHp;
        }
        set
        {
            enemyHp = value;
            enemyHpText.text = "HP: " + enemyHp;
        }
    }
    public int Score
    {
        get
        {
            return currentScore;
        }
        set
        {
            currentScore = value;
            currentScoreText.text = "점수 :" + currentScore;
        }
    }
    
    

    public float Timer
    {
        get
        {
            return currentTime;
        }
        set
        {
            currentTime = value;
            timeText.text = "" + clearTime;
            if (currentTime == 0 && !isClear)
            {
                isGameOver = true;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        createTime = Random.Range(minTime, maxTime);
        objectPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            int ranObject = Random.Range(0, objectFactorys.Length);
            GameObject gameObject = Instantiate(objectFactorys[ranObject], spawnPoint);
            objectPool.Add(gameObject);
            gameObject.SetActive(false);
        }

        currentScoreText.text = "점수 : " + currentScore;
    }
    void SpawnObject()
    {
        if (!isGameOver && !isSpawn)
        {
            if (objectPool.Count > 0)
            {
                GameObject gameObject = objectPool[0];
                objectPool.Remove(gameObject);
                gameObject.transform.position = spawnPoint.position;
                gameObject.SetActive(true);
                isSpawn = true;
            }
        }
    }
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (clearTime > 0)
        {
            clearTime -= Time.deltaTime;
        }
        if (currentTime > createTime)
        {
            SpawnObject();
            createTime = Random.Range(minTime, maxTime);
            currentTime = 0;
        }
        timeText.text = clearTime.ToString("F2");
        diceNumberText.text = diceNumber.ToString();
        enemyHpText.text = "HP: " + enemyHp;
    }
}
