using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Codice.Client.BaseCommands.Differences;

public class enemyConfig : MonoBehaviour
{
    private enemyType enemyType_;
    [SerializeField] private TextMeshPro hpTxt;

    private const float damageDefault = 20.0f;
    private const float speedDefault = 2.0f;
    private const float hpDefault = 100.0f;
    private const float sizeDefault = 1.0f;

    [HideInInspector] public float damage;
    [HideInInspector] public float speed;
    [HideInInspector] public float hp;
    [HideInInspector] public float size;

    [HideInInspector] public float hpScaler = 1f;
    [HideInInspector] public float damageScaler = 1f;
    [HideInInspector] public float speedScaler = 1f;
    [HideInInspector] public float sizeScaler = 1f;

    private void Awake()
    {
        Setup();
    }
    private void Update()
    {
        damage = damageScaler * damageDefault;
        speed = speedScaler * speedDefault;
        size = sizeScaler * sizeDefault;

        transform.localScale = new Vector3(size, size, 1.0f);
        hpTxt.text = hp.ToString();

        hpTxt.color = new Color(1 - (hp / hpDefault), hp / hpDefault, 0, 1);

        if (hp <= 0)
        {
            GameManager.Instance.score++;
            Destroy(gameObject);
        }
    }

    private void Setup()
    {
        int score = GameManager.Instance.score;
        int range = 0;

        if (score % 50 != 0 || score == 0)
        {
            if (score <= 15)
            {
                range = 1;

            }
            else if (score > 15 && score <= 30)
            {
                //big enemy add
                range = 2;

            }
            else if (score > 30)
            {
                //small enemy add
                range = 3;

            }

            int type = Random.Range(0, range);

            if (type == 0)
                enemyType_ = enemyType.defaultEnemy;
            else if (type == 1)
                enemyType_ = enemyType.bigEnemy;
            else
                enemyType_ = enemyType.smallEnemy;
        }
        else
        {
            //boss every 50 points
            enemyType_ = enemyType.boss;
        }

        switch (enemyType_)
        {
            case enemyType.defaultEnemy:
                damageScaler = 1.0f;
                speedScaler = 1.0f;
                sizeScaler = 1.0f;
                hpScaler = 1.0f;
                break;
            case enemyType.bigEnemy:
                damageScaler = 2.0f;
                speedScaler = 0.5f;
                sizeScaler = 1.5f;
                hpScaler = 1.5f;
                break;
            case enemyType.smallEnemy:
                damageScaler = 0.5f;
                speedScaler = 1.75f;
                sizeScaler = 0.65f;
                hpScaler = 0.65f;
                break;
            case enemyType.boss:
                damageScaler = 3.0f;
                speedScaler = 1.25f;
                sizeScaler = 4.0f;
                hpScaler = 2.75f;
                break;
        }
        hp = (int)(hpDefault * hpScaler + (GameManager.Instance.score / 5 * hpScaler));
    }
}
