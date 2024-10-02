using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private Camera camera_;
    private Vector3 cameraPosStart;

    private float speed = 4f;

    private void Start()
    {
        camera_ = Camera.main;
        camera_.transform.position = cameraPosStart;
    }

    void Update()
    {
        if (GlobalVaribles.gameStatus == true)
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                transform.Translate(0, speed * Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                transform.Translate(0, -1 * speed * Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                transform.Translate(-1 * speed * Time.deltaTime, 0, 0);
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                transform.Translate(speed * Time.deltaTime, 0, 0);
            }
        }

        camera_.transform.position = new Vector3(cameraPosStart.x + transform.position.x, cameraPosStart.y + transform.position.y, -10);
    }
}
