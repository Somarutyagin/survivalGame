using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum gameStatus
{
    play,
    pause
}

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
    private spawnManager spawnManager_;
    private UIManager UIManager_;
    private playerConfig playerConfig_;
    private PlayerCollision PlayerCollision_;
    private Transform enemyPool, dropPool, player;

    public float border;
    public gameStatus activeGameStatus;
    private int _valueScore;
    public int score
    {
        get
        {
            Init();
            return _valueScore;
        }
        set
        {
            _valueScore = value;
            if (score > record)
                record = score;
        }
    }
    private string keyRecord = "record";
    private int _valueRecord;
    public int record
    {
        get
        {
            Init();
            return _valueRecord;
        }
        set
        {
            PlayerPrefs.SetInt(keyRecord, value);
            _valueRecord = value;
        }
    }
    public void Init()
    {
        _valueRecord = PlayerPrefs.GetInt(keyRecord);
    }

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
        GameObject SpawnControllerObj = GameObject.Find("spawnController");
        spawnManager_ = SpawnControllerObj.GetComponent<spawnManager>();
        GameObject UImanagerObj = GameObject.Find("UIController");
        UIManager_ = UImanagerObj.GetComponent<UIManager>();

        enemyPool = GameObject.Find("enemyPool").transform;
        dropPool = GameObject.Find("dropPool").transform;
        player = GameObject.Find("Player").transform;
        playerConfig_ = player.GetComponent<playerConfig>();
        PlayerCollision_ = player.GetComponent<PlayerCollision>();

        GameObject map = GameObject.Find("Map");
        border = map.transform.GetChild(0).position.x - 1;
    }
    private void Update()
    {
        if (Instance.activeGameStatus == gameStatus.play)
        {
            Time.timeScale = 1f;

            if (spawnManager_.isActiveSpawn == false)
            {
                spawnManager_.startSpawn();
            }
        }
        else
        {
            Time.timeScale = 0f;

            spawnManager_.isActiveSpawn = false;
            spawnManager_.StopAllCoroutines();
        }
    }

    public void Lose()
    {
        UIManager_.OnExitButtonPressed();
        ResetGame();
    }
    public void ResetGame()
    {
        score = 0;

        for (int i = 0; i < enemyPool.childCount; i++)
        {
            Destroy(enemyPool.GetChild(i).gameObject);
        }
        for (int i = 0; i < dropPool.childCount; i++)
        {
            Destroy(dropPool.GetChild(i).gameObject);
        }

        player.position = new Vector3(0, 0, 0);
        PlayerCollision_.resetEffects();
        playerConfig_.Reset();
    }
}

