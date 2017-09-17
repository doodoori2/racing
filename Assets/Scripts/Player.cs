using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 0.1f;
    public bool slowMode = false;
    public Transform starGroupTransform;
    public Transform rockGroupTransform;
    public Slider hpSlider;
    public int hp = 10;

    private void Awake()
    {
        hpSlider.maxValue = hp;
    }

    // Update is called once per frame
    void Update()
    {
        var s = false;

        hpSlider.value = hp;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            s = true;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            s = true;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            s = true;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            s = true;
        }

        var cam = Camera.main;
        var topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
        //var topLeft = cam.ViewportToWorldPoint(new Vector3(-1, 1, cam.nearClipPlane));
        var bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        //var bottomRight = cam.ViewportToWorldPoint(new Vector3(1, -1, cam.nearClipPlane));

        var xClipped = Mathf.Clamp(transform.position.x, bottomLeft.x, topRight.x);
        var yClipped = Mathf.Clamp(transform.position.y, bottomLeft.y, topRight.y);
        transform.position = new Vector3(xClipped, yClipped, transform.position.z);

        if (s != slowMode)
        {
            //SetSlowMode(s);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetSlowMode(!slowMode);
        }
    }

    internal void AddDamage(int v)
    {
        if (hp > 0)
        {
            hp--;
        }
    }

    private void SetSlowMode(bool s)
    {
        slowMode = s;

        //Debug.LogFormat("Slow Mode: {0}", slowMode);

        for (int i = 0; i < starGroupTransform.childCount; i++)
        {
            var star = starGroupTransform.GetChild(i).GetComponent<Star>();
            star.SetSlowMode(slowMode);
        }

        for (int i = 0; i < rockGroupTransform.childCount; i++)
        {
            var rock = rockGroupTransform.GetChild(i).GetComponent<Rock>();
            rock.SetSlowMode(slowMode);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.LogFormat("Collided with {0}", other.name);
    }
}
