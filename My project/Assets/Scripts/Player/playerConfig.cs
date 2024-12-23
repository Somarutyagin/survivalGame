using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class playerConfig : MonoBehaviour
{
    [SerializeField] private Text hpTxt;

    private const float damageDefault = 20.0f;
    private const float speedDefault = 4.0f;
    private const float hpDefault = 100.0f;

    [HideInInspector] public float damage;
    [HideInInspector] public float speed;
    [HideInInspector] public float hp;

    [HideInInspector] public float maxHp;
    [HideInInspector] public float damageScaler = 1f;
    [HideInInspector] public float speedScaler = 1f;
    [HideInInspector] public float hpScaler = 1f;

    private void Awake()
    {
        Reset();
    }
    private void Update()
    {
        maxHp = hpScaler * hpDefault;
        damage = damageScaler * damageDefault;
        speed = speedScaler * speedDefault;

        if (hp > maxHp)
            hp = maxHp;

        if (hp <= 0)
            GameManager.Instance.Lose();

        if (hpTxt != null)
        {
            hpTxt.text = hp.ToString();

            hpTxt.color = new Color(1 - (hp / hpDefault), hp / hpDefault, 0, 1);
        }
    }
    public void Reset()
    {
        damageScaler = 1;
        speedScaler = 1;
        hpScaler = 1;

        maxHp = hpScaler * hpDefault;
        damage = damageScaler * damageDefault;
        speed = speedScaler * speedDefault;

        hp = maxHp;
    }
}
