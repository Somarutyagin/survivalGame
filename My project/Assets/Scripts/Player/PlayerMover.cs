using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private Camera camera_;
    private Vector3 cameraPosStart;

    private void Start()
    {
        camera_ = Camera.main;
        camera_.transform.position = cameraPosStart;
    }

    void Update()
    {
        if (GlobalVaribles.gameStatus == true)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(0, 0.1f, 0);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(0, -0.1f, 0);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(-0.1f, 0, 0);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(0.1f, 0, 0);
            }
        }

        camera_.transform.position = new Vector3(cameraPosStart.x + transform.position.x, cameraPosStart.y + transform.position.y, -10);
    }
}
