using System.Collections;
using UnityEngine;
using TMPro;

public class dropLogic: MonoBehaviour
{
    //private bool isBonusEffectActive = true;

    //private int bonusEffectTime = 10;
    public string type;

    void Start()
    {
        init();
        StartCoroutine(destroyTimer());
    }

    private void init()
    {
        int randType = Random.Range(0, 4);

        switch (randType)
        {
            case 0:
                type = "hp";
                break;
            case 1:
                type = "shield";
                break;
            case 2:
                type = "doubleDamage";
                break;
            case 3:
                type = "speedBoost";
                break;
        }

        transform.GetChild(0).GetComponent<TextMeshPro>().text = type;
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (type == "hp")
                collision.GetComponent<playerConfig>().hp += 50;
            else
                StartCoroutine(bonusEffectTimer(type, collision));

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            transform.GetChild(0).GetComponent<TextMeshPro>().text = string.Empty;
        }
    }
    
    private IEnumerator bonusEffectTimer(string type_, Collider2D collision)
    {
        isBonusEffectActive = true;

        switch (type_)
        {
            case "shield":
                collision.GetComponent<PlayerCollision>().isActiveShield = true;
                collision.transform.GetChild(3).gameObject.SetActive(true);
                break;
            case "doubleDamage":
                collision.GetComponent<playerConfig>().damage *= 2;
                collision.transform.GetChild(4).gameObject.SetActive(true);
                break;
            case "speedBoost":
                collision.GetComponent<playerConfig>().speed *= 2;
                collision.transform.GetChild(5).gameObject.SetActive(true);
                break;
        }

        yield return new WaitForSeconds(bonusEffectTime);

        switch (type_)
        {
            case "shield":
                collision.GetComponent<PlayerCollision>().isActiveShield = false;
                collision.transform.GetChild(3).gameObject.SetActive(false);
                break;
            case "doubleDamage":
                collision.GetComponent<playerConfig>().damage /= 2;
                collision.transform.GetChild(4).gameObject.SetActive(false);
                break;
            case "speedBoost":
                collision.GetComponent<playerConfig>().speed /= 2;
                collision.transform.GetChild(5).gameObject.SetActive(false);
                break;
        }
        Destroy(gameObject);
    }
    */
    private IEnumerator destroyTimer()
    {
        yield return new WaitForSeconds(10f);
        //if (!isBonusEffectActive)
            Destroy(gameObject);
    }
}
