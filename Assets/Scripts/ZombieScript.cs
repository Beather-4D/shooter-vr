using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.AI;

public class ZombieScript : MonoBehaviour
{
    public Animator zombieAnimator;
    public GameObject player;

    public int zombieHealth = 10;
    public float zombieSpeed = 0;
    public float zombieAcceleration = 0.03f;
    public float zombieMaxSpeed = 1.5f;
    public bool isBiting = false;
    
    private NavMeshAgent agent;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    /*
    private void Update()
    {
        transform.LookAt(player.transform);
        if (Vector3.Distance(player.transform.position, transform.position) < 0.3f) 
        {
            zombieSpeed = 0;
            zombieAnimator.SetTrigger("bite");
            player.GetComponent<PlayerScript>().dealDamage(10);
        }
        else if (zombieSpeed < zombieMaxSpeed)
        {
            zombieSpeed += zombieAcceleration * Time.deltaTime;
            zombieAnimator.SetFloat("speed", zombieSpeed);
        }
        transform.Translate(Vector3.forward * Time.deltaTime * zombieSpeed);
    }
    */

    private void Update()
    {
        agent.SetDestination(player.transform.position);

        if (Vector3.Distance(player.transform.position, transform.position) < 0.3f)
        {
            zombieSpeed = 0;
            zombieAnimator.SetTrigger("bite");
            player.GetComponent<PlayerScript>().dealDamage(10);
        }
        else if (zombieSpeed < zombieMaxSpeed)
        {
            zombieSpeed += zombieAcceleration * Time.deltaTime;
            zombieAnimator.SetFloat("speed", zombieSpeed);
        }
        agent.speed = zombieSpeed;
        
    }


    public void TakeDamage(int damage)
    {
        zombieHealth -= damage;

        if (zombieHealth <= 0)
        {
            Destroy(gameObject);
            player.GetComponent<PlayerScript>().addMoney(10);
        }
    }

}
