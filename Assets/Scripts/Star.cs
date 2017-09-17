using UnityEngine;

public class Star : MonoBehaviour
{
    public float speed = 1;
    public float targetZScale = 2000;
    public float zScaleSmoothTime = 0.1f;
    public float currentZScale = 2000;
    void Update()
    {
        transform.Translate(0, 0, -speed * Time.deltaTime);

        if (transform.position.z < 0)
        {
            Destroy(gameObject);
        }

        float vel = 0;
        currentZScale = Mathf.SmoothDamp(currentZScale, targetZScale, ref vel, zScaleSmoothTime);
        transform.localScale.Set(transform.localScale.x, transform.localScale.y, currentZScale);
    }
}
