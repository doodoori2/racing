using UnityEngine;

public class Star : MonoBehaviour
{
    public float speed = 1;
    public float slowTargetSpeed = 1;
    public float normalTargetSpeed = 1;

    public float slowTargetZScale = 60;
    public float normalTargetZScale = 2000;
    public float zScaleSmoothTime = 0.1f;
    public float speedSmoothTime = 0.1f;

    private float currentTargetZScale = 2000;
    private float currentZScale = 2000;
    private float currentTargetSpeed;
    private float currentSpeed;

    public LayerMask playerLayerMask;
    public bool hit;

    private float zDelta { get { return -currentSpeed * Time.deltaTime; } }
    
    void Update()
    {
        transform.Translate(0, 0, zDelta);

        if (transform.position.z < 0)
        {
            Destroy(gameObject);
        }

        float vel = 0;
        currentZScale = Mathf.SmoothDamp(currentZScale, currentTargetZScale, ref vel, zScaleSmoothTime);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, currentZScale);

        currentSpeed = Mathf.SmoothDamp(currentSpeed, currentTargetSpeed, ref vel, speedSmoothTime);
    }

    private void FixedUpdate()
    {
        if (Physics.BoxCast(transform.position, new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z), -Vector3.forward, Quaternion.identity, Mathf.Abs(zDelta), playerLayerMask))
        {
            if (!hit)
            {
                Debug.Log("Player hit");
                hit = true;
            }
        }
    }

    public void SetSlowModeImmediate(bool slow)
    {
        SetSlowMode(slow);
        currentZScale = currentTargetZScale;
        currentSpeed = currentTargetSpeed;
    }

    public void SetSlowMode(bool slow)
    {
        currentTargetZScale = slow ? slowTargetZScale : normalTargetZScale;
        currentTargetSpeed = slow ? slowTargetSpeed : normalTargetSpeed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z));
    }
}
