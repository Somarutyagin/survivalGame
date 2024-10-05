using System.Collections;
using UnityEngine;

public class dropAI : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(destroy());
    }
    private IEnumerator destroy()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
