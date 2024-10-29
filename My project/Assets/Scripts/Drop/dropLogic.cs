using System.Collections;
using UnityEngine;
using TMPro;

public class dropLogic: MonoBehaviour
{
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
    private IEnumerator destroyTimer()
    {
        yield return new WaitForSeconds(10f);
        //if (!isBonusEffectActive)
            Destroy(gameObject);
    }
}
