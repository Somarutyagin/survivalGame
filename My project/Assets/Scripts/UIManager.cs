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

    private void Start()
    {
        GlobalVaribles.gameStatus = false;
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
        recordTxt.text = GlobalVaribles.record.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && GlobalVaribles.gameStatus == true)
        {
            PauseDisplay.SetActive(true);
            GlobalVaribles.gameStatus = false;
        }
        else if (Input.GetKeyUp(KeyCode.Escape) && GlobalVaribles.gameStatus == false && PauseDisplay.activeSelf)
        {
            OnContinueButtonPressed();
        }

        if (GlobalVaribles.gameStatus == true)
        {
            scoreTxt.text = GlobalVaribles.score.ToString();
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
        GlobalVaribles.gameStatus = true;
    }
    public void OnExitButtonPressed()
    {
        GlobalVaribles.gameStatus = false;
        GameManager.Instance.ResetGame();

        MenuConfigurator();
    }
    public void OnContinueButtonPressed()
    {
        PauseDisplay.SetActive(false);
        GlobalVaribles.gameStatus = true;
    }
}
