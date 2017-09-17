using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    public float currentSpawnInterval = 1;
    public float slowSpawnInterval = 1;
    public float normalSpawnInterval = 1;
    public float currentTargetSpawnInterval = 1;

    public float lastSpawnTime = 0;
    public GameObject rockPrefab;
    public float spawnXWidth = 100;
    public float spawnYWidth = 100;
    public float spawnZWidth = 100;
    public int spawnCount = 20;
    public Transform rockGroupTransform;
    private Player player;

    public float spawnIntervalSmoothTime = 0.1f;

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
                var rock = Instantiate(rockPrefab);

                rock.transform.position = new Vector3(
                    -spawnXWidth / 2 + spawnYWidth * (float)r.NextDouble(),
                    -spawnYWidth / 2 + spawnYWidth * (float)r.NextDouble(),
                    gameObject.transform.position.z - spawnZWidth / 2 + spawnZWidth * (float)r.NextDouble());

                rock.GetComponent<Rock>().SetSlowModeImmediate(player.slowMode);

                rock.transform.parent = rockGroupTransform;

                lastSpawnTime = Time.time;
            }
        }

        float vel = 0;
        currentSpawnInterval = Mathf.SmoothDamp(currentSpawnInterval, currentTargetSpawnInterval, ref vel, spawnIntervalSmoothTime);
    }

    public void SetSlowModeImmediate(bool slow)
    {
        SetSlowMode(slow);
        currentSpawnInterval = currentTargetSpawnInterval;
    }

    public void SetSlowMode(bool slow)
    {
        currentTargetSpawnInterval = slow ? slowSpawnInterval : normalSpawnInterval;
        lastSpawnTime = Time.time;
    }
}
