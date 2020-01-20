using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform muzzleExitPoint;
    public Projectile bullet;
    public float msBetweenShots = 1000;
    public float exitVelocity = 35;

    float nextShotTime;

    public void Shoot()
    {

        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + msBetweenShots / 1000;
            Projectile newBullet = Instantiate(bullet, muzzleExitPoint.position, muzzleExitPoint.rotation) as Projectile;
            newBullet.setSpeed(exitVelocity);
        }

    }
}
