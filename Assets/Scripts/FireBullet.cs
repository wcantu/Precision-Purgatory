
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
    private GameObject bulletPrefab; 

    [SerializeField]
    private int bulletsAmount = 10;

    [SerializeField]
    private float startAngle = 90f, endAngle = 270f;

    private float angle; 

    void Start()
    {
        switch (activeBoss)
        {
            case BossType.Boss1:
                InvokeRepeating("FireInArc", 0f, 3f);
                break;
            case BossType.Boss2:
                InvokeRepeating("FireContinuousSpiral", 0f, 0.033f); 
                break;
            case BossType.Boss3:
                
                InvokeRepeating("FireInArc", 0f, 2f);
                InvokeRepeating("FireContinuousSpiral", 0f, 0.4f); 
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
        
        FireBulletAtAngle(angle);
        angle += 10f; 
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