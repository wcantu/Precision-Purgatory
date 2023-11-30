
using System.Collections;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public enum BossType
    {
        Boss1,
        Boss2,
        Boss3
    }

    [SerializeField]
    private BossType activeBoss;

    [SerializeField]
    private GameObject bulletPrefab; // Assign the bullet prefab in the inspector

    [SerializeField]
    private int bulletsAmount = 10;

    [SerializeField]
    private float startAngle = 90f, endAngle = 270f;

    private float angle; // Used for the spiral pattern

    void Start()
    {
        switch (activeBoss)
        {
            case BossType.Boss1:
                InvokeRepeating("FireInArc", 0f, 3f);
                break;
            case BossType.Boss2:
                InvokeRepeating("FireContinuousSpiral", 0f, 0.033f); // Faster continuous spiral firing
                break;
            case BossType.Boss3:
                // Combines both patterns
                InvokeRepeating("FireInArc", 0f, 2f); // Arc pattern
                InvokeRepeating("FireContinuousSpiral", 0f, 0.4f); // Spiral pattern
                break;

        }
    }

    private void FireInArc()
    {
        float angleStep = (endAngle - startAngle) / bulletsAmount;
        float firingAngle = startAngle;

        for (int i = 0; i < bulletsAmount; i++)
        {
            FireBulletAtAngle(firingAngle);
            firingAngle += angleStep;
        }
    }


    private void FireContinuousSpiral()
    {
        // New Boss 2 pattern
        FireBulletAtAngle(angle);
        angle += 10f; // Incrementing angle by 10 degrees for each bullet
    }

    private void FireBulletAtAngle(float firingAngle)
    {
        float bulDirX = Mathf.Sin((firingAngle * Mathf.PI) / 180f);
        float bulDirY = Mathf.Cos((firingAngle * Mathf.PI) / 180f) * -1;
        Vector2 bulDir = new Vector2(bulDirX, bulDirY);

        GameObject bul = ObjectPool.bulletPoolInstance.GetBullet();
        if (bul != null)
        {
            bul.transform.position = transform.position;
            bul.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, firingAngle - 90));
            bul.SetActive(true);
            bul.GetComponent<BulletScript>().SetMoveDirection(bulDir);
        }
    }
}