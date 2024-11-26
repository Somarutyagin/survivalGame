using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TestSuite : MonoBehaviour
{
    GameObject playerGO;
    GameObject spawnController;
    GameObject enemyPool;

    [SetUp]
    public void Setup()
    {
        if (playerGO != null)
            Destroy(playerGO);
        if (spawnController != null)
            Destroy(spawnController);
        if (enemyPool != null)
            Destroy(enemyPool);

        GameManager.Instance.ResetGame();
        GameManager.Instance.activeGameStatus = gameStatus.play;
    }
    [UnityTest]
    public IEnumerator GameOverOccursOnPlayerHpDropBelowOrEqualZero()
    {
        playerGO = new GameObject();
        playerConfig playerConfig_ = playerGO.AddComponent<playerConfig>();

        playerConfig_.hp = -1;

        yield return new WaitForSecondsRealtime(0.1f);

        Assert.AreEqual(gameStatus.pause, GameManager.Instance.activeGameStatus);
    }
    [UnityTest]
    public IEnumerator SpawnEnemyAndAwardedPointsWhenDestroyingEnemies()
    {
        // spawn
        spawnController = new GameObject();
        spawnManager spawnManager_ = spawnController.AddComponent<spawnManager>();
        enemyPool = new GameObject("enemyPool");
        spawnManager_.SpawnEnemy();

        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(1, enemyPool.transform.childCount);

        //AwardedPointsWhenDestroying
        int score = GameManager.Instance.score;
        enemyPool.transform.GetChild(0).GetComponent<enemyConfig>().hp = -1;

        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(score + 1, GameManager.Instance.score);
    }
}
