using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject enemyPool;
    [HideInInspector] public bool isActiveSpawn = false;

    [SerializeField] private GameObject drop;
    [SerializeField] private GameObject dropPool;

    private Transform player;
    private float dropMaxCount = 20;
    private float dropSpawndelay = 10;

    private float enemyMaxCount = 100;
    private float enemySpawndelay = 1f;

    void Start()
    {
        player = GameObject.Find("Player")?.transform;
    }
    public void startSpawn()
    {
        isActiveSpawn = true;

        StartCoroutine(spawnEnemyCoroutine());
        StartCoroutine(spawnDropCoroutine());
    }
    private IEnumerator spawnEnemyCoroutine()
    {

        while (true)
        {
            yield return new WaitForSeconds(enemySpawndelay);

            if (enemyPool.transform.childCount < enemyMaxCount)
            {
                SpawnEnemy();
            }

            if (isActiveSpawn == false)
            {
                yield break;
            }
        }
    }
    public void SpawnEnemy()
    {
        if (enemyPool == null)
            enemyPool = GameObject.Find("enemyPool");
        if (enemy == null)
            enemy = Resources.Load<GameObject>("Prefabs/Enemy");

        Vector2 spawnPos = new Vector2(Random.Range(-1 * GameManager.Instance.border, GameManager.Instance.border + 1), Random.Range(-1 * GameManager.Instance.border, GameManager.Instance.border + 1));
        Instantiate(enemy, spawnPos, Quaternion.identity, enemyPool.transform);
    }

    private IEnumerator spawnDropCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(dropSpawndelay);

            if (dropPool.transform.childCount < dropMaxCount)
            {
                SpawnDrop();
            }

            if (isActiveSpawn == false)
            {
                yield break;
            }
        }
    }
    public void SpawnDrop()
    {
        Vector2 spawnPos = new Vector2(Random.Range(player.position.x - 30, player.position.x + 30), Random.Range(player.position.y - 30, player.position.y + 30));

        if (spawnPos.x > GameManager.Instance.border)
            spawnPos = new Vector2(GameManager.Instance.border, spawnPos.y);
        else if (spawnPos.x < -1 * GameManager.Instance.border)
            spawnPos = new Vector2(-1 * GameManager.Instance.border, spawnPos.y);

        if (spawnPos.y > GameManager.Instance.border)
            spawnPos = new Vector2(spawnPos.x, GameManager.Instance.border);
        else if (spawnPos.y < -1 * GameManager.Instance.border)
            spawnPos = new Vector2(spawnPos.x, GameManager.Instance.border);

        Instantiate(drop, spawnPos, Quaternion.identity, dropPool.transform);
    }
}
