﻿using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 0.1f;

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }

        var cam = Camera.main;
        var topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
        //var topLeft = cam.ViewportToWorldPoint(new Vector3(-1, 1, cam.nearClipPlane));
        var bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        //var bottomRight = cam.ViewportToWorldPoint(new Vector3(1, -1, cam.nearClipPlane));

        var xClipped = Mathf.Clamp(transform.position.x, bottomLeft.x, topRight.x);
        var yClipped = Mathf.Clamp(transform.position.y, bottomLeft.y, topRight.y);
        transform.position = new Vector3(xClipped, yClipped, transform.position.z);
    }
}
