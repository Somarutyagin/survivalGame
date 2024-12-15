using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class leaderBoard : MonoBehaviour
{
    [SerializeField] private registration registration_;
    [SerializeField] private Transform playerCellPool;
    [SerializeField] private GameObject playerCell;
    public List<string> players = new List<string>();
    public List<int> records = new List<int>();

    public void LeaderBoardUpdate()
    {
        for (int i = 0; i < playerCellPool.childCount; i++)
        {
            Destroy(playerCellPool.GetChild(i).gameObject);
        }

        for (int i = 0; i < records.Count - 1; i++)
        {
            for (int j = 0; j < records.Count - 1 - i; j++)
            {
                if (records[j] < records[j + 1])
                {
                    int tempR = records[j];
                    records[j] = records[j + 1];
                    records[j + 1] = tempR;

                    string tempP = players[j];
                    players[j] = players[j + 1];
                    players[j + 1] = tempP;
                }
            }
        }

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] == registration_.activeUser && i != 0)
            {
                GameManager.Instance.nearRecord = records[i - 1];
                GameManager.Instance.nearRecordPlayer = players[i - 1];
            }
        }

        for (int i = 0; i < players.Count; i++)
        {
            GameObject playerCell_ = Instantiate(playerCell, playerCellPool);
            playerCell_.transform.GetChild(1).GetComponent<Text>().text = players[i];
            playerCell_.transform.GetChild(2).GetComponent<Text>().text = records[i].ToString();
        }
    }
}
