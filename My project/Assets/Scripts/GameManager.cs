using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("GameManager").AddComponent<GameManager>();
            }
            return _instance;
        }
    }
    private enemySpawn enemySpawn_;
    private dropSpawn dropSpawn_;
    private UIManager UIManager_;
    private Transform enemyPool, dropPool, player;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        GlobalVaribles.Init();
        GameObject SpawnControllerObj = GameObject.Find("spawnController");
        enemySpawn_ = SpawnControllerObj.GetComponent<enemySpawn>();
        dropSpawn_ = SpawnControllerObj.GetComponent<dropSpawn>();
        GameObject UImanagerObj = GameObject.Find("UIController");
        UIManager_ = UImanagerObj.GetComponent<UIManager>();

        enemyPool = GameObject.Find("enemyPool").transform;
        dropPool = GameObject.Find("dropPool").transform;
        player = GameObject.Find("Player").transform;
    }
    private void Update()
    {
        if (GlobalVaribles.gameStatus == true)
        {
            Time.timeScale = 1f;

            //����� ���������
            if (enemySpawn_.isActiveSpawnEnemy == false && dropSpawn_.isActiveSpawnDrop == false)
            {
                StartCoroutine(enemySpawn_.spawnEnemy());
                StartCoroutine(dropSpawn_.spawnDrop());
            }
        }
        else
        {
            Time.timeScale = 0f;

            //��������� ���������
            enemySpawn_.isActiveSpawnEnemy = false;
            enemySpawn_.StopAllCoroutines();
            dropSpawn_.isActiveSpawnDrop = false;
            dropSpawn_.StopAllCoroutines();
        }
    }

    public void Lose()
    {
        UIManager_.OnExitButtonPressed();
        ResetGame();
    }
    public void ResetGame()
    {
        GlobalVaribles.score = 0;

        for (int i = 0; i < enemyPool.childCount; i++)
        {
            Destroy(enemyPool.GetChild(i).gameObject);
        }
        for (int i = 0; i < dropPool.childCount; i++)
        {
            Destroy(dropPool.GetChild(i).gameObject);
        }

        player.position = new Vector3(0, 0, 0);
    }
}

