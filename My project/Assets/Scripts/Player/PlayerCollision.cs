using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
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
    }

    private void TakeDamage(Collider2D collision)
    {
        config.hp -= collision.GetComponent<enemyConfig>().damage;
        clip.SetTrigger("damage");

        if (config.hp <= 0)
        {
            GameManager.Instance.Lose();
        }

        StartCoroutine(damageTakePush(collision));
    }

    private IEnumerator damageTakePush(Collider2D collision)
    {
        //������������ ��� �����
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
}
