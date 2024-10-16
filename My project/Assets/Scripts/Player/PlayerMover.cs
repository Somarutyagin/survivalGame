using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private playerConfig config;
    private Transform weapon;
    private Camera camera_;
    private Vector3 cameraPosStart;
    private Vector2 playerPos;

    private float rotationSpeed = 1f;

    private void Start()
    {
        camera_ = Camera.main;
        camera_.transform.position = cameraPosStart;
        weapon = transform.GetChild(1);
        config = gameObject.GetComponent<playerConfig>();
    }

    private void Update()
    {
        if (GameManager.Instance.activeGameStatus == gameStatus.play)
        {
            //проверка на выход из границ арены
            //движение игрока
            if (transform.position.x < GameManager.Instance.border && transform.position.x > -1 * GameManager.Instance.border && transform.position.y < GameManager.Instance.border && transform.position.y > -1 * GameManager.Instance.border)
            {
                playerPos = transform.position;

                if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                {
                    transform.Translate(0, config.speed * Time.deltaTime, 0);
                }
                if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                {
                    transform.Translate(0, -1 * config.speed * Time.deltaTime, 0);
                }
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                {
                    transform.Translate(-1 * config.speed * Time.deltaTime, 0, 0);
                }
                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                {
                    transform.Translate(config.speed * Time.deltaTime, 0, 0);
                }
            }
            else
            {
                transform.position = playerPos;
            }
            //движение оружия
            weapon.Rotate(0, 0, rotationSpeed);
        }

        camera_.transform.position = new Vector3(cameraPosStart.x + transform.position.x, cameraPosStart.y + transform.position.y, -10);
    }
}
