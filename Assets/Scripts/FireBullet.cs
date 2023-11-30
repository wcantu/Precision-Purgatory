using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    [SerializeField]
    private int bulletsAmount = 10;

    [SerializeField]
    private float startAngle = 90f, endAngle = 270f;

    void Start()
    {
        InvokeRepeating("Fire", 0f, 2f);
    }

    private void Fire()
    {
        float angleStep = (endAngle - startAngle) / bulletsAmount;
        float angle = startAngle;

        for (int i = 0; i < bulletsAmount; i++)
        {
            // Calculate direction vector
            float bulDirX = Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = Mathf.Cos((angle * Mathf.PI) / 180f) * -1; // Invert Y direction
            Vector2 bulDir = new Vector2(bulDirX, bulDirY);

            GameObject bul = ObjectPool.bulletPoolInstance.GetBullet();
            if (bul != null)
            {
                bul.transform.position = transform.position;
                bul.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 90)); // Adjust rotation if needed
                bul.SetActive(true);
                bul.GetComponent<BulletScript>().SetMoveDirection(bulDir);
            }

            angle += angleStep;
        }
    }
}
