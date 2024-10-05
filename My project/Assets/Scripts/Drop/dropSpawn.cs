using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropSpawn : MonoBehaviour
{
    [SerializeField] private GameObject drop;
    [SerializeField] private GameObject dropPool;
    [HideInInspector] public bool isActiveSpawnDrop = false;

    private Transform player;
    private float dropMaxCount = 20;
    private float dropSpawndelay = 10;
    private const float border = 99.0f;

    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    public IEnumerator spawnDrop()
    {
        isActiveSpawnDrop = true;

        while (true)
        {
            yield return new WaitForSeconds(dropSpawndelay);

            if (dropPool.transform.childCount < dropMaxCount)
            {
                Vector2 spawnPos = new Vector2(Random.Range(player.position.x-30, player.position.x+30), Random.Range(player.position.y-30, player.position.y+30));

                //защита от спавна дропа вне границы карты
                if (spawnPos.x > GlobalVaribles.border)
                    spawnPos = new Vector2(GlobalVaribles.border, spawnPos.y);
                else if (spawnPos.x < -1 * GlobalVaribles.border)
                    spawnPos = new Vector2(-1 * GlobalVaribles.border, spawnPos.y);

                if (spawnPos.y > GlobalVaribles.border)
                    spawnPos = new Vector2(spawnPos.x, GlobalVaribles.border);
                else if (spawnPos.y < -1 * GlobalVaribles.border)
                    spawnPos = new Vector2(spawnPos.x, GlobalVaribles.border);

                Instantiate(drop, spawnPos, Quaternion.identity, dropPool.transform);
            }

            if (isActiveSpawnDrop == false)
            {
                yield break;
            }
        }
    }
}
