using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject enemyPool;
    [HideInInspector] public bool isActiveSpawnEnemy = false;

    private float enemyMaxCount = 100;
    private float enemySpawndelay = 1f;

    public IEnumerator spawnEnemy()
    {
        isActiveSpawnEnemy = true;

        while (true)
        {
            yield return new WaitForSeconds(enemySpawndelay);

            //ограничение в спавне для избежания потенциальных проблем с производительностью
            if (enemyPool.transform.childCount < enemyMaxCount)
            {
                Vector2 spawnPos = new Vector2(Random.Range(-1 * GlobalVaribles.border, GlobalVaribles.border + 1), Random.Range(-1 * GlobalVaribles.border, GlobalVaribles.border + 1));
                Instantiate(enemy, spawnPos, Quaternion.identity, enemyPool.transform);
            }

            if (isActiveSpawnEnemy == false)
            {
                //выход из корутины
                yield break;
            }
        }
    }
}
