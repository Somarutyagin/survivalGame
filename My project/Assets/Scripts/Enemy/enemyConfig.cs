using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class enemyConfig : MonoBehaviour
{
    [SerializeField] private TextMeshPro hpTxt;

    private const float damageDefault = 20.0f;
    private const float speedDefault = 2.0f;
    private const float hpDefault = 100.0f;

    [HideInInspector] public float damage;
    [HideInInspector] public float speed;
    [HideInInspector] public float hp;

    private void Awake()
    {
        damage = damageDefault;
        speed = speedDefault;
        hp = hpDefault + GameManager.Instance.score; 
    }
    private void Update()
    {
        hpTxt.text = hp.ToString();

        hpTxt.color = new Color(1 - (hp / hpDefault), hp / hpDefault, 0, 1);

        if (hp <= 0)
        {
            GameManager.Instance.score++;
            Destroy(gameObject);
        }
    }
}
