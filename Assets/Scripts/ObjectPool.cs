using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // Singleton instance for easy access to the object pool.
    public static ObjectPool bulletPoolInstance;

    [SerializeField]
    private GameObject pooledBullet; // The bullet prefab that will be pooled.

    private bool notEnoughBulletsInPool = true; // Flag to indicate if more bullets need to be added to the pool.

    // List to hold all pooled bullet objects.
    private List<GameObject> bullets;

    private void Awake()
    {
        // Initialize the singleton instance.
        bulletPoolInstance = this;
    }

    void Start()
    {
        // Initialize the list of pooled objects.
        bullets = new List<GameObject>();
    }

    // Method to get a bullet from the pool.
    public GameObject GetBullet()
    {
        // Iterate through the existing bullets in the pool.
        foreach (var bullet in bullets)
        {
            // Check if the bullet is not active (i.e., available for use).
            if (!bullet.activeInHierarchy)
            {
                // Return the inactive bullet to be reused.
                return bullet;
            }
        }

        // If no inactive bullets are found and the pool can expand:
        if (notEnoughBulletsInPool)
        {
            // Create a new bullet, deactivate it, and add it to the pool.
            GameObject bul = Instantiate(pooledBullet);
            bul.SetActive(false);
            bullets.Add(bul);
            return bul;
        }

        // If no bullets are available and the pool cannot expand, return null.
        return null;
    }
}

