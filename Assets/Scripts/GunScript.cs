using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript: MonoBehaviour
{
    public GameObject bulletSpawnPoint;

    public GameObject bulletPrefab;

    Vector3 bulletOffset = new Vector3(90, 0, 0);
    Quaternion bulletRotOffset;


    private void Start()
    {
        bulletRotOffset = Quaternion.Euler(bulletOffset);
    }

    public void ShootBullet()
    {
        Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
    }

}
