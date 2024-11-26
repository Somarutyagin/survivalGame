using UnityEngine;

public class enemyAI : MonoBehaviour
{
    private Transform player;
    private float speed = 2f;

    void Start()
    {
        player = GameObject.Find("Player")?.transform;
    }

    void Update()
    {
        if (player != null)
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
}
