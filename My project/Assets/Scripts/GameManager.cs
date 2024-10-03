using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private enemySpawn enemySpawn_;
    private dropSpawn dropSpawn_;

    private void Awake()
    {
        GlobalVaribles.Init();
        enemySpawn_ = gameObject.GetComponent<enemySpawn>();
        dropSpawn_ = gameObject.GetComponent<dropSpawn>();
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
}

