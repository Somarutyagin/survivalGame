using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public bool isDamageTakeCooldown;
    public bool isActiveShield;
    public bool isActiveSpeedBoost;
    public bool isActiveDoubleDamage;
    public string effect;
    private int bonusEffectTime = 10;

    private playerConfig config;
    private Animator clip;

    private void Awake()
    {
        config = GetComponent<playerConfig>();
        clip = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            TakeDamage(collision);
        }
        else if (collision.tag == "Drop")
        {
            effect = collision.GetComponent<dropLogic>().type;

            if (effect == "hp")
                config.hp += 50;
            else
                bonusEffectTimer();

            Destroy(collision.gameObject);
        }
    }
    private void bonusEffectTimer()
    {
        switch (effect)
        {
            case "shield":
                if (!isActiveShield)
                {
                    isActiveShield = true;
                    transform.GetChild(3).gameObject.SetActive(true);
                }

                StartCoroutine(shieldTimer());
                break;
            case "doubleDamage":
                if (!isActiveDoubleDamage)
                {
                    isActiveDoubleDamage = true;
                    config.damageScaler *= 2;
                    transform.GetChild(4).gameObject.SetActive(true);
                }

                StartCoroutine(doubleDamageTimer());
                break;
            case "speedBoost":
                if (!isActiveSpeedBoost)
                {
                    isActiveSpeedBoost = true;
                    config.speedScaler *= 2;
                    transform.GetChild(5).gameObject.SetActive(true);
                }

                StartCoroutine(speedBoostTimer());
                break;
        }
    }
    private IEnumerator shieldTimer()
    {
        yield return new WaitForSeconds(bonusEffectTime);
        resetEffects();
    }
    private IEnumerator doubleDamageTimer()
    {
        yield return new WaitForSeconds(bonusEffectTime);
        resetEffects();
    }
    private IEnumerator speedBoostTimer()
    {
        yield return new WaitForSeconds(bonusEffectTime);
        resetEffects();
    }
    public void resetEffects()
    {
        if (isActiveShield || isActiveDoubleDamage || isActiveSpeedBoost)
        {
            if (isActiveShield)
            {
                isActiveShield = false;
                transform.GetChild(3).gameObject.SetActive(false);
            }
            if (isActiveDoubleDamage)
            {
                isActiveDoubleDamage = false;
                config.damageScaler /= 2;
                transform.GetChild(4).gameObject.SetActive(false);
            }
            if (isActiveSpeedBoost)
            {
                isActiveSpeedBoost = false;
                config.speedScaler /= 2;
                transform.GetChild(5).gameObject.SetActive(false);
            }
            effect = string.Empty;
        }
    }
    private void TakeDamage(Collider2D collision)
    {
        if (!isDamageTakeCooldown && !isActiveShield)
        {
            StartCoroutine(damageTakeCooldown());

            config.hp -= collision.GetComponent<enemyConfig>().damage;
            clip.SetTrigger("damage");

            StartCoroutine(damageTakePush(collision));
        }
    }

    private IEnumerator damageTakePush(Collider2D collision)
    {
        Vector3 enemyPos = collision.transform.position;

        int iterations = 100;
        float time = 0.2f;
        float pushPower = 2.1f;

        for (int i = 0; i < 50; i++)
        {
            Vector3 distance = transform.position - enemyPos;

            yield return new WaitForSeconds(time / iterations);
            transform.position = transform.position + new Vector3(distance.x / iterations * pushPower, distance.y / iterations * pushPower, 0);
        }
    }
    private IEnumerator damageTakeCooldown()
    {
        isDamageTakeCooldown = true;
        yield return new WaitForSeconds(1.5f);

        isDamageTakeCooldown = false;
    }
}
