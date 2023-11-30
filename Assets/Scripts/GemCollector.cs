using UnityEngine;

public class GemCollector : MonoBehaviour
{
    private HeartSystem heartSystem;

    private void Start()
    {
        // Find the HeartSystem in the scene.
        heartSystem = FindObjectOfType<HeartSystem>();

        if (heartSystem == null)
        {
            Debug.LogError("HeartSystem not found in the scene. Make sure it exists.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GEM"))
        {
            if (heartSystem != null)
            {
                heartSystem.points++; // Increment the points.

                // Update the UI text
                heartSystem.UpdatePointsText();

                Destroy(other.gameObject); // Destroy the gem.

                // Load the next stage if points threshold is met
                if (heartSystem.points >= heartSystem.pointsToWin)
                {
                    heartSystem.LoadNextStage();
                }
            }
            else
            {
                Debug.LogError("HeartSystem is null. Make sure it's assigned in the Inspector.");
            }
        }
    }
}
