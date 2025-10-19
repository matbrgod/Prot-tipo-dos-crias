using UnityEngine;
using System.Collections;

// ...existing code...
public class Spawner_Ratinhos : MonoBehaviour
{
    public GameObject ratinhoPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 5f; // seconds between spawns

    void Start()
    {
        // ...existing code...
        if (ratinhoPrefab == null || spawnPoints == null || spawnPoints.Length == 0) return;
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnRandom();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnRandom()
    {
        if (ratinhoPrefab == null || spawnPoints == null || spawnPoints.Length == 0) return;
        int i = Random.Range(0, spawnPoints.Length);
        Instantiate(ratinhoPrefab, spawnPoints[i].position, spawnPoints[i].rotation);
    }

    void Update()
    {
        // ...existing code...
    }
}