using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponCollision : MonoBehaviour
{
    //private bool isDamageCooldown = false;
    private playerConfig config;
    [SerializeField] private UIManager manager;

    private void Awake()
    {
        config = transform.parent.parent.GetComponent<playerConfig>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            DealDamage(other);
        }
    }
    private void DealDamage(Collider2D collision)
    {
        collision.GetComponent<enemyConfig>().hp -= config.damage;

        if (collision.GetComponent<enemyConfig>().hp <= 0)
        {
            Destroy(collision.gameObject);
            manager.score++;
        }

        StartCoroutine(damageDealPush(collision));
    }
    private IEnumerator damageDealPush(Collider2D collision)
    {
        //отталкивание при уроне
        Vector3 weaponPos = collision.transform.position;

        int iterations = 100;
        float time = 0.2f;
        float pushPower = 6.3f;

        for (int i = 0; i < 50; i++)
        {
            Vector3 distance = new Vector3(0, 0, 0);
            if (collision != null)
                distance = weaponPos - collision.transform.position;

            yield return new WaitForSeconds(time / iterations);

            if (collision != null)
                collision.transform.position = collision.transform.position + new Vector3(distance.x / iterations * pushPower, distance.y / iterations * pushPower, 0);
        }
    }
}
