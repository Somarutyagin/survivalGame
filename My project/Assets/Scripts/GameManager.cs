using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private enemySpawn enemySpawn_;

    private void Awake()
    {
        GlobalVaribles.Init();
        enemySpawn_ = gameObject.GetComponent<enemySpawn>();
    }
    private void Update()
    {
        if (GlobalVaribles.gameStatus == true)
        {
            Time.timeScale = 1f;

            //старт спавнеров
            if (enemySpawn_.isActiveSpawnEnemy == false)
            {
                StartCoroutine(enemySpawn_.spawnEnemy());
            }
        }
        else
        {
            Time.timeScale = 0f;

            //остановка спавнеров
            enemySpawn_.isActiveSpawnEnemy = false;
            enemySpawn_.StopAllCoroutines();
        }
    }
}

