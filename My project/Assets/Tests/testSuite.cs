using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;

public class TestSuite
{
    [UnityTest]
    public IEnumerator GameOverOccursOnPlayerHpDropBelowOrEqualZero()
    {
        GameObject player = GameObject.Find("Player");

        player.GetComponent<playerConfig>().hp = -1;

        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(gameStatus.pause, GameManager.Instance.activeGameStatus);
    }
    [UnityTest]
    public IEnumerator AwardedPointsWhenDestroyingEnemies()
    {
        enemyConfig config = new enemyConfig();
        int score = GameManager.Instance.score;

        config.hp = -1;

        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(score + 1, GameManager.Instance.score);
    }
}
