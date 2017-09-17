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
        if (Time.time - lastSpawnTime > currentSpawnInterval)
        {
            var r = new System.Random();

            for (int i = 0; i < spawnCount; i++)
            {
                var star = Instantiate(starPrefab);
                
                star.transform.position = new Vector3(
                    -spawnXWidth / 2 + spawnXWidth * (float)r.NextDouble(),
                    -spawnYWidth / 2 + spawnYWidth * (float)r.NextDouble(),
                    gameObject.transform.position.z + Random.Range(-spawnZWidth / 2, spawnZWidth / 2));

                star.GetComponent<Star>().SetSlowModeImmediate(player.slowMode);

                star.transform.parent = starGroupTransform;

                if(i == 0)
                {
                    var slide = GameObject.FindObjectOfType<Slide>();
                    if(slide != null)
                    {
                        slide.AddObject(star);
                    }
                }

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
