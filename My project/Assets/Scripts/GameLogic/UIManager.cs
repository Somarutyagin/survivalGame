using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> UIListMenu = new List<GameObject>();
    [SerializeField] private List<GameObject> UIListGame = new List<GameObject>();

    [SerializeField] private GameObject leaderBoardDisplay;
    [SerializeField] private GameObject PauseDisplay;
    [SerializeField] private GameObject SettingsDisplay;
    [SerializeField] private Text scoreTxt;
    [SerializeField] private Text recordTxt;
    private void Start()
    {
        GameManager.Instance.activeGameStatus = gameStatus.pause;
        MenuConfigurator();
    }

    public void MenuConfigurator()
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
        SettingsDisplay.SetActive(false);
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
        else if (Input.GetKeyUp(KeyCode.Escape) && GameManager.Instance.activeGameStatus == gameStatus.pause && !PauseDisplay.activeSelf)
        {
            if (!SettingsDisplay.activeSelf)
                SettingsDisplay.SetActive(true);
            else
                SettingsDisplay.SetActive(false);
        }
        else if (Input.GetKeyUp(KeyCode.Escape) && leaderBoardDisplay.activeSelf)
        {
            leaderBoardDisplay.SetActive(false);
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
        GameManager.Instance.Lose();

        MenuConfigurator();
    }
    public void OnContinueButtonPressed()
    {
        PauseDisplay.SetActive(false);
        GameManager.Instance.activeGameStatus = gameStatus.play;
    }

    public void OnLeaderBoardButtonPressed()
    {
        leaderBoardDisplay.SetActive(true);
    }
    public void OnExitLeaderBoardButtonPressed()
    {
        leaderBoardDisplay.SetActive(false);
    }

}
