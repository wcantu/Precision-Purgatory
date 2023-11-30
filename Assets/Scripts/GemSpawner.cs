using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    public GameObject gemPrefab;
    public float spawnRadius = 5f; // Adjusted spawn radius
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
            int attempts = 0; // To prevent infinite loops

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
        // Determine the visible bounds of the camera
        float cameraHeight = 2f * Camera.main.orthographicSize;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        // Define bounds to exclude the top-left part of the screen
        // Adjust these values based on how much of the top-left you want to exclude
        float excludeTopLeftX = cameraWidth * 0.25f; // Exclude 25% of the left part
        float excludeTopLeftY = cameraHeight * 0.25f; // Exclude 25% of the top part

        // Generate a random position within the adjusted bounds
        float randomX = Random.Range(-cameraWidth / 2, cameraWidth / 2 - excludeTopLeftX);
        float randomY = Random.Range(-cameraHeight / 2, cameraHeight / 2 - excludeTopLeftY);

        // Return the random position. Adjust Z if needed.
        return new Vector3(randomX, randomY, 0); // Use 0 for Z if the game is 2D
    }
}
