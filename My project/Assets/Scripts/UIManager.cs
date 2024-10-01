using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> UIListMenu = new List<GameObject>();
    [SerializeField] private List<GameObject> UIListGame = new List<GameObject>();

    [SerializeField] private GameObject PauseDisplay;

    private void Start()
    {
        MenuConfigurator();
    }

    private void MenuConfigurator()
    {
        OnExitButtonPressed();
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
        for (int i = 0; i < UIListMenu.Count; i++)
        {
            UIListMenu[i].SetActive(true);
        }
        for (int i = 0; i < UIListGame.Count; i++)
        {
            UIListGame[i].SetActive(false);
        }
        PauseDisplay.SetActive(false);
        GlobalVaribles.gameStatus = false;
    }
    public void OnContinueButtonPressed()
    {
        PauseDisplay.SetActive(false);
        GlobalVaribles.gameStatus = true;
    }
}
