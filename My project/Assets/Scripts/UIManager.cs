using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> UIListMenu = new List<GameObject>();
    [SerializeField] private List<GameObject> UIListGame = new List<GameObject>();

    [SerializeField] private GameObject PauseDisplay;
    [SerializeField] private Text scoreTxt;
    [SerializeField] private Text recordTxt;
    /*Score
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
    
    //Record
    private const string keyRecord = "record";
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
   
    private void Init()
    {
        _valueRecord = PlayerPrefs.GetInt(keyRecord);
    }
    */

    private void Start()
    {
        GameManager.Instance.Init();
        GameManager.Instance.activeGameStatus = gameStatus.pause;
        MenuConfigurator();
    }

    private void MenuConfigurator()
    {
        for (int i = 0; i < UIListMenu.Count; i++)
        {
            UIListMenu[i].SetActive(true);
        }
        for (int i = 0; i < UIListGame.Count; i++)
        {
            UIListGame[i].SetActive(false);
        }

        PauseDisplay.SetActive(false);
        recordTxt.text = GameManager.Instance.record.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && GameManager.Instance.activeGameStatus == gameStatus.play)
        {
            PauseDisplay.SetActive(true);
            GameManager.Instance.activeGameStatus = gameStatus.pause;
        }
        else if (Input.GetKeyUp(KeyCode.Escape) && GameManager.Instance.activeGameStatus == gameStatus.pause && PauseDisplay.activeSelf)
        {
            OnContinueButtonPressed();
        }

        if (GameManager.Instance.activeGameStatus == gameStatus.play)
        {
            scoreTxt.text = GameManager.Instance.score.ToString();
        }
    }

    public void OnPlayButtonPressed()
    {
        for (int i = 0; i < UIListMenu.Count; i++)
        {
            UIListMenu[i].SetActive(false);
        }
        for (int i = 0; i < UIListGame.Count; i++)
        {
            UIListGame[i].SetActive(true);
        }
        GameManager.Instance.activeGameStatus = gameStatus.play;
    }
    public void OnExitButtonPressed()
    {
        GameManager.Instance.activeGameStatus = gameStatus.pause;
        GameManager.Instance.ResetGame();

        MenuConfigurator();
    }
    public void OnContinueButtonPressed()
    {
        PauseDisplay.SetActive(false);
        GameManager.Instance.activeGameStatus = gameStatus.play;
    }
}
