using UnityEngine;

public class Rock : MonoBehaviour
{
    public float speed = 1;
    void Update()
    {
        transform.Translate(0, 0, -speed);

        if (transform.position.z < 0)
        {
            Destroy(gameObject);
        }
    }
}
