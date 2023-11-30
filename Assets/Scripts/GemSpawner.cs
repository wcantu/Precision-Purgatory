using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    public GameObject gemPrefab; // Assign your GEM prefab in the inspector.
    public float spawnRadius = 10f; // The radius within which GEMs will spawn.
    public int gemsToSpawn = 3; // The number of GEMs to spawn.
    public float minGemSeparation = 2f; // Minimum separation distance between gems.

    void Start()
    {
        SpawnGems();
    }

    void SpawnGems()
    {
        for (int i = 0; i < gemsToSpawn; i++)
        {
            Vector3 spawnPosition = GetRandomPosition();
            // Check if the new gem is too close to existing gems.
            bool isTooClose = IsGemTooClose(spawnPosition);
            while (isTooClose)
            {
                spawnPosition = GetRandomPosition();
                isTooClose = IsGemTooClose(spawnPosition);
            }

            var spawnedGem = Instantiate(gemPrefab, spawnPosition, Quaternion.identity);
        }
    }

    bool IsGemTooClose(Vector3 newPosition)
    {
        // Iterate through existing gems to check their distances.
        GameObject[] existingGems = GameObject.FindGameObjectsWithTag("GEM");
        foreach (var gem in existingGems)
        {
            float distance = Vector3.Distance(newPosition, gem.transform.position);
            if (distance < minGemSeparation)
            {
                return true; // The new gem is too close to an existing gem.
            }
        }
        return false; // The new gem is not too close to any existing gems.
    }

    Vector3 GetRandomPosition()
    {
        // This will create a random Vector3 within the spawnRadius from the position of the spawner GameObject.
        Vector3 randomPosition = transform.position + Random.insideUnitSphere * spawnRadius;
        randomPosition.y = 0; // Set this to the desired height at which the GEMs should spawn.
        return randomPosition;
    }
}
