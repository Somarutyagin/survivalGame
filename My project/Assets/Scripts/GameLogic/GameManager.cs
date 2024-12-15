using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    private registration registration_;
    private spawnManager spawnManager_;
    private UIManager UIManager_;
    private playerConfig playerConfig_;
    private PlayerCollision PlayerCollision_;
    private Transform enemyPool, dropPool, player;
    private GameObject nearRecordNotif;

    public int activeRecord;
    public int nearRecord;
    public string nearRecordPlayer;
    [HideInInspector] public float border { get; private set; }
    public gameStatus activeGameStatus;
    private int _valueScore;
    public int score
    {
        get
        {
            return _valueScore;
        }
        set
        {
            _valueScore = value;
            if (score > record)
            {
                record = score;
                registration_.SaveRegistrationInfo(true);
            }
            if (score > activeRecord && activeGameStatus == gameStatus.play)
            {
                if (activeRecord != 0)
                {
                    notificationManager.Instance.NewRecordNotifLifetime();
                    activeRecord = 0;
                }
            }
            if (score < nearRecord && score >= nearRecord - 20 && activeGameStatus == gameStatus.play)
            {
                if (nearRecordNotif == null)
                {
                    nearRecordNotif = notificationManager.Instance.CreateNotification(notificationManager.Instance.nearRecordNotif);
                    nearRecordNotif.transform.GetChild(0).GetComponent<Text>().text = nearRecordPlayer;
                    nearRecordNotif.transform.GetChild(1).GetComponent<Text>().text = nearRecord.ToString();
                }
            }
            else if (score >= nearRecord && activeGameStatus == gameStatus.play)
            {
                if (nearRecordNotif != null)
                    notificationManager.Instance.DestroyNotification(nearRecordNotif);
            }

            if (value % 50 == 0 && value != 0)
                spawnManager_.SpawnEnemy();
        }
    }
    private int _valueRecord;
    public int record
    {
        get
        {
            return _valueRecord;
        }
        set
        {
            //PlayerPrefs.SetInt(keyRecord, value);
            _valueRecord = value;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        GameObject SpawnControllerObj = GameObject.Find("spawnController");
        GameObject AuthControllerObj = GameObject.Find("AuthController");
        spawnManager_ = SpawnControllerObj?.GetComponent<spawnManager>();
        registration_ = AuthControllerObj?.GetComponent<registration>();
        GameObject UImanagerObj = GameObject.Find("UIController");
        UIManager_ = UImanagerObj?.GetComponent<UIManager>();

        enemyPool = GameObject.Find("enemyPool")?.transform;
        dropPool = GameObject.Find("dropPool")?.transform;
        player = GameObject.Find("Player")?.transform;
        playerConfig_ = player?.GetComponent<playerConfig>();
        PlayerCollision_ = player?.GetComponent<PlayerCollision>();

        GameObject map = GameObject.Find("Map");
        if (map != null)
            border = map.transform.GetChild(0).position.x - 1;
    }
    private void Update()
    {
        if (Instance.activeGameStatus == gameStatus.play)
        {
            Time.timeScale = 1f;

            if (spawnManager_?.isActiveSpawn == false)
            {
                spawnManager_?.startSpawn();
            }
        }
        else
        {
            Time.timeScale = 0f;
            if (spawnManager_ != null)
                spawnManager_.isActiveSpawn = false;
            spawnManager_?.StopAllCoroutines();
            notificationManager.Instance.StopAllCoroutines();
            if (nearRecordNotif != null)
                Destroy(nearRecordNotif);
            if (notificationManager.Instance.newRecordNotifObj != null)
                Destroy(notificationManager.Instance.newRecordNotifObj);
        }
    }

    public void Lose()
    {
        activeGameStatus = gameStatus.pause;
        ResetGame();
    }
    public void ResetGame()
    {
        score = 0;

        for (int i = 0; i < enemyPool?.childCount; i++)
        {
            Destroy(enemyPool?.GetChild(i).gameObject);
        }
        for (int i = 0; i < dropPool?.childCount; i++)
        {
            Destroy(dropPool?.GetChild(i).gameObject);
        }

        if (player != null)
            player.position = new Vector3(0, 0, 0);
        PlayerCollision_?.resetEffects();
        PlayerCollision_?.StopAllCoroutines();
        if (PlayerCollision_ != null)
            PlayerCollision_.isDamageTakeCooldown = false;
        playerConfig_?.Reset();
        UIManager_?.MenuConfigurator();
    }
}

