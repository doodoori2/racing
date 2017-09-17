using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    public float currentSpawnInterval = 1;
    public float lastSpawnTime = 0;
    public GameObject starPrefab;
    public float spawnXWidth = 100;
    public float spawnYWidth = 100;
    public float spawnZWidth = 100;
    public int spawnCount = 20;
    public float spawnIntervalMin = 0.5f;
    public float spawnIntervalMax = 1.0f;

    public float slowSpawnIntervalMin = 0.5f;
    public float slowSpawnIntervalMax = 0.5f;

    public float normalSpawnIntervalMin = 1.0f;
    public float normalSpawnIntervalMax = 1.0f;

    public float targetSpawnIntervalMin = 1.0f;
    public float targetSpawnIntervalMax = 1.0f;

    public float spawnIntervalMinSmoothTime = 0.1f;
    public float spawnIntervalMaxSmoothTime = 0.1f;

    public float currentTargetSpawnIntervalMin;
    public float currentTargetSpawnIntervalMax;

    public Transform starGroupTransform;
    private Player player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        var cam = Camera.main;
        var topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
        //var topLeft = cam.ViewportToWorldPoint(new Vector3(-1, 1, cam.nearClipPlane));
        var bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        //var bottomRight = cam.ViewportToWorldPoint(new Vector3(1, -1, cam.nearClipPlane));

        spawnXWidth = topRight.x * 2;
        spawnYWidth = topRight.y * 2;

        if (Time.time - lastSpawnTime > currentSpawnInterval)
        {
            var r = new System.Random();

            for (int i = 0; i < spawnCount; i++)
            {
                var star = Instantiate(starPrefab);

                var x = Random.Range(bottomLeft.x, topRight.x);
                var y = Random.Range(bottomLeft.y, topRight.y);

                Debug.LogFormat("Star spawned at {0}, {1}", x, y);

                star.transform.position = new Vector3(
                    x,
                    y,
                    gameObject.transform.position.z + Random.Range(-spawnZWidth / 2, spawnZWidth / 2));

                star.GetComponent<Star>().SetSlowModeImmediate(player.slowMode);

                star.transform.parent = starGroupTransform;

                lastSpawnTime = Time.time;
            }

            currentSpawnInterval = Random.Range(spawnIntervalMin, spawnIntervalMax);
        }

        float vel = 0;
        spawnIntervalMin = Mathf.SmoothDamp(spawnIntervalMin, targetSpawnIntervalMin, ref vel, spawnIntervalMinSmoothTime);
        spawnIntervalMax = Mathf.SmoothDamp(spawnIntervalMax, targetSpawnIntervalMax, ref vel, spawnIntervalMaxSmoothTime);
    }

    public void SetSlowModeImmediate(bool slow)
    {
        SetSlowMode(slow);
        spawnIntervalMin = currentTargetSpawnIntervalMin;
        spawnIntervalMax = currentTargetSpawnIntervalMax;
    }

    public void SetSlowMode(bool slow)
    {
        currentTargetSpawnIntervalMin = slow ? slowSpawnIntervalMin : normalSpawnIntervalMin;
        currentTargetSpawnIntervalMax = slow ? slowSpawnIntervalMax : normalSpawnIntervalMax;
    }
}
