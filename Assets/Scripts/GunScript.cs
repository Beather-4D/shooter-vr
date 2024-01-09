using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GunScript: MonoBehaviour
{
    public GameObject bulletSpawnPoint;
    public GameObject bulletPrefab;
    public int ammoMax = 10;
    public int ammo = 10;  
    public TMP_Text ammoIndicator;
    public XRSocketInteractor socket;

    Vector3 bulletOffset = new Vector3(90, 0, 0);
    Quaternion bulletRotOffset;


    private void Start()
    {
        bulletRotOffset = Quaternion.Euler(bulletOffset);
    }

    public void ShootBullet()
    {
        if (ammo > 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
            bullet.GetComponent<BulletScript>().damage = 5;
            ammo -= 1;
            ammoIndicator.text = ammo.ToString();
            if(ammo == 3)
            {
                ammoIndicator.color = new Color(255, 165, 0); 
            }
            if (ammo == 0)
            {
                ammoIndicator.color = Color.red;
            }
        }
        
    }

    public void RechargeGun()
    {
        ammo = ammoMax;
        ammoIndicator.text = ammo.ToString();
        ammoIndicator.color = new Color(139, 255, 0);
        Destroy(socket.GetOldestInteractableSelected().transform.gameObject, 1);
        
    }
}
