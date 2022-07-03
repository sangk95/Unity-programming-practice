using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLauncher : MonoBehaviour
{
    [SerializeField]
    Bullet bulletPrefab;
    Bullet bullet;
    [SerializeField]
    Transform firePosition;
    Factory bulletFactory;
    [SerializeField]
    float fireDelay = 0.5f;
    float elapsedFireTime;
    bool canShoot = true; 
    void Start()
    {
        bulletFactory = new Factory(bulletPrefab);
    }
    public void OnFireButtonPressed(Vector3 position)
    {
        if(!canShoot)
            return;

        bullet = bulletFactory.Get() as Bullet;
        bullet.Activate(firePosition.position, position);
        bullet.Destroyed += OnBulletDestroyed;
        canShoot = false;
    }
    void OnBulletDestroyed(Bullet usedBullet) 
    {
        usedBullet.Destroyed -= OnBulletDestroyed;
        bulletFactory.Restore(usedBullet);
    }
    void Update()
    {
        if(!canShoot)
        {
            elapsedFireTime += Time.deltaTime;
            if(elapsedFireTime >= fireDelay)
            {
                canShoot = true;
                elapsedFireTime = 0f;
            }
        }
    }
}
