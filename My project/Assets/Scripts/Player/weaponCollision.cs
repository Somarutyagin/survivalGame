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
    }
}
