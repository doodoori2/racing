using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    public float spawnInterval = 1;
    public float lastSpawnTime = 0;
    public GameObject rockPrefab;
    public float spawnXWidth = 100;
    public float spawnYWidth = 100;
    public float spawnZWidth = 100;
    public int spawnCount = 20;
    public Transform rockGroupTransform;

    void Update()
    {
        if (Time.time - lastSpawnTime > spawnInterval)
        {
            var r = new System.Random();

            for (int i = 0; i < spawnCount; i++)
            {
                var rock = Instantiate(rockPrefab);

                rock.transform.position = new Vector3(
                    -spawnXWidth / 2 + spawnYWidth * (float)r.NextDouble(),
                    -spawnYWidth / 2 + spawnYWidth * (float)r.NextDouble(),
                    gameObject.transform.position.z - spawnZWidth / 2 + spawnZWidth * (float)r.NextDouble());

                rock.transform.parent = rockGroupTransform;

                lastSpawnTime = Time.time;
            }
        }
    }
}
