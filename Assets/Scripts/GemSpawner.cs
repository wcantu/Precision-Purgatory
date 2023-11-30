using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    public GameObject gemPrefab;
    public float spawnRadius = 5f; 
    public int gemsToSpawn = 3;
    public float minGemSeparation = 2f;

    void Start()
    {
        SpawnGems();
    }

    void SpawnGems()
    {
        for (int i = 0; i < gemsToSpawn; i++)
        {
            Vector3 spawnPosition = GetRandomPosition();
            bool isTooClose = IsGemTooClose(spawnPosition);
            int attempts = 0; 

            while (isTooClose && attempts < 10) // Try a limited number of times
            {
                spawnPosition = GetRandomPosition();
                isTooClose = IsGemTooClose(spawnPosition);
                attempts++;
            }

            if (!isTooClose)
            {
                Instantiate(gemPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    bool IsGemTooClose(Vector3 newPosition)
    {
        GameObject[] existingGems = GameObject.FindGameObjectsWithTag("GEM");
        foreach (var gem in existingGems)
        {
            if (Vector3.Distance(newPosition, gem.transform.position) < minGemSeparation)
            {
                return true;
            }
        }
        return false;
    }

    Vector3 GetRandomPosition()
    {
        
        float cameraHeight = 2f * Camera.main.orthographicSize;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        // Define bounds to exclude the top-left part of the screen
        float excludeTopLeftX = cameraWidth * 0.25f; 
        float excludeTopLeftY = cameraHeight * 0.25f;

        // Generate a random position within the adjusted bounds
        float randomX = Random.Range(-cameraWidth / 2, cameraWidth / 2 - excludeTopLeftX);
        float randomY = Random.Range(-cameraHeight / 2, cameraHeight / 2 - excludeTopLeftY);

        
        return new Vector3(randomX, randomY, 0); 
    }
}
