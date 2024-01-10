using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.AI;

public class ZombieScript : MonoBehaviour
{
    public Animator zombieAnimator;
    private GameObject player;
    private PlayerScript playerScript;

    public int zombieHealth = 10;
    public int zombieDamage = 10;
    public float zombieSpeed = 0;
    public float zombieAcceleration = 0.03f;
    public float zombieMaxSpeed = 1.5f;
    private bool isBiting = false;
    
    private NavMeshAgent agent;

    private AudioSource audioSource;
    public AudioClip[] deathSounds;
    public AudioClip[] bloodSplashSounds;
    public AudioClip[] biteSounds;
    public AudioClip[] eatSounds;
    public AudioClip[] footstepsSounds;
    public AudioClip[] shotSounds;


    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();
        agent = GetComponent<NavMeshAgent>();

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        agent.SetDestination(player.transform.position);

        if (Vector3.Distance(player.transform.position, transform.position) < 0.9f)
        {
            zombieSpeed = 0;
            if (!isBiting){
                StartCoroutine(BitePlayer());
            }
            
           
        }
        else if (zombieSpeed < zombieMaxSpeed)
        {
            zombieSpeed += zombieAcceleration * Time.deltaTime;
            zombieAnimator.SetFloat("speed", zombieSpeed);
        }
        agent.speed = zombieSpeed;
        
    }

    IEnumerator BitePlayer()
    {
        isBiting = true;
        zombieAnimator.SetTrigger("bite");
        playerScript.DealDamage(zombieDamage);

        PlayBiteAudio();
        PlayEatAudio();

        yield return new WaitForSeconds(3);
        isBiting = false;
    }

    public void TakeDamage(int damage)
    {
        zombieHealth -= damage;

        if (zombieHealth <= 0)
        {
            PlayDeathShotAudio();
            PlayBloadSplashAudio();

            SkinnedMeshRenderer mesh = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
            mesh.enabled = false;
            Collider collider = gameObject.GetComponentInChildren<Collider>();
            collider.enabled = false;

            playerScript.AddMoney(10);
            StartCoroutine(DeleteZombie());
        }
        else
        {
            PlayShotAudio();
        }
    }

    IEnumerator DeleteZombie()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    private void PlayDeathShotAudio()
    {
        audioSource.clip = deathSounds[UnityEngine.Random.Range(0, deathSounds.Length)];
        SetupOtherParamsAndPlay();
    }

    private void PlayBloadSplashAudio()
    {
        audioSource.clip = bloodSplashSounds[UnityEngine.Random.Range(0, bloodSplashSounds.Length)];
        SetupOtherParamsAndPlay();
    }
    private void PlayShotAudio()
    {
        audioSource.clip = shotSounds[UnityEngine.Random.Range(0, shotSounds.Length)];
        SetupOtherParamsAndPlay();
    }

    private void PlayFootStepsAudio()
    {
        audioSource.clip = footstepsSounds[UnityEngine.Random.Range(0, footstepsSounds.Length)];
        SetupOtherParamsAndPlay();
    }

    private void PlayEatAudio()
    {
        audioSource.clip = eatSounds[UnityEngine.Random.Range(0, eatSounds.Length)];
        SetupOtherParamsAndPlay();
    }

    private void PlayBiteAudio()
    {
        audioSource.clip = biteSounds[UnityEngine.Random.Range(0, biteSounds.Length)];
        SetupOtherParamsAndPlay();
    }

    private void SetupOtherParamsAndPlay()
    {
        audioSource.volume = UnityEngine.Random.Range(1 - .25f, 1);
        audioSource.pitch = UnityEngine.Random.Range(1 - .2f, 1 + .2f);
        audioSource.PlayOneShot(audioSource.clip);
    }

}
