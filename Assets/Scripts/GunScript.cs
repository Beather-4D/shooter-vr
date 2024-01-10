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
    private AudioSource audioSource;
    public AudioClip[] reloadSounds;
    public AudioClip[] fireSounds;
    public AudioClip emptyAmmoSound;

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
        audioSource = GetComponent<AudioSource>();
        bulletRotOffset = Quaternion.Euler(bulletOffset);
    }

    public void ShootBullet()
    {
        if (ammo > 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
            PlayShootAudio();

            bullet.GetComponent<BulletScript>().damage = 5;
            ammo -= 1;
            ammoIndicator.text = ammo.ToString();
            if(ammo == 3)
            {
                ammoIndicator.color = new Color(139, 255, 0); 
            }
            if (ammo == 0)
            {
                ammoIndicator.color = Color.red;
            }
        }
        else
        {
            PlayEmptyAmmoAudio();
        }
    }

    public void RechargeGun()
    {
        ammo = ammoMax;
        PlayReloadAudio();
        ammoIndicator.text = ammo.ToString();
        ammoIndicator.color = Color.green;
        Destroy(socket.GetOldestInteractableSelected().transform.gameObject, 1);
        
    }

    private void PlayShootAudio()
    {
        audioSource.clip = fireSounds[UnityEngine.Random.Range(0, fireSounds.Length)];
        SetupOtherParamsAndPlay(.25f, .2f);
    }

    private void PlayReloadAudio()
    {
        audioSource.clip = reloadSounds[UnityEngine.Random.Range(0, reloadSounds.Length)];
        SetupOtherParamsAndPlay(.25f, .2f);
    }

    private void PlayEmptyAmmoAudio()
    {
        audioSource.clip = emptyAmmoSound;
        SetupOtherParamsAndPlay(.25f, .2f);
    }

    private void SetupOtherParamsAndPlay(float volumeRange, float pitchRange)
    {
        audioSource.volume = UnityEngine.Random.Range(1 - volumeRange, 1);
        audioSource.pitch = UnityEngine.Random.Range(1 - pitchRange, 1 + pitchRange);
        audioSource.PlayOneShot(audioSource.clip);
    }
}
