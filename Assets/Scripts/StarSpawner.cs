using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    public float spawnInterval = 1;
    public float lastSpawnTime = 0;
    public GameObject rockPrefab;
    public float spawnXWidth = 100;
    public float spawnYWidth = 100;
    public float spawnZWidth = 100;
    public int spawnCount = 20;
    public float spawnIntervalMin = 0.5f;
    public float spawnIntervalMax = 1.0f;
    public Transform starGroupTransform;

    void Update()
    {
        var cam = Camera.main;
        var topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
        //var topLeft = cam.ViewportToWorldPoint(new Vector3(-1, 1, cam.nearClipPlane));
        var bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        //var bottomRight = cam.ViewportToWorldPoint(new Vector3(1, -1, cam.nearClipPlane));

        spawnXWidth = topRight.x * 2;
        spawnYWidth = topRight.y * 2;

        if (Time.time - lastSpawnTime > spawnInterval)
        {
            var r = new System.Random();

            for (int i = 0; i < spawnCount; i++)
            {
                var rock = Instantiate(rockPrefab);

                var x = Random.Range(bottomLeft.x, topRight.x);
                var y = Random.Range(bottomLeft.y, topRight.y);

                Debug.LogFormat("Star spawned at {0}, {1}", x, y);

                rock.transform.position = new Vector3(
                    x,
                    y,
                    gameObject.transform.position.z + Random.Range(-spawnZWidth / 2, spawnZWidth / 2));

                rock.transform.parent = starGroupTransform;

                lastSpawnTime = Time.time;
            }

            spawnInterval = Random.Range(spawnIntervalMin, spawnIntervalMax);
        }
    }
}
