using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.AI;

public class ZombieScript : MonoBehaviour
{
    public Animator zombieAnimator;
    public GameObject player;
    public PlayerScript playerScript;

    public int zombieHealth = 10;
    public int zombieDamage = 10;
    public float zombieSpeed = 0;
    public float zombieAcceleration = 0.03f;
    public float zombieMaxSpeed = 1.5f;
    private bool isBiting = false;
    
    private NavMeshAgent agent;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        agent.SetDestination(player.transform.position);

        if (Vector3.Distance(player.transform.position, transform.position) < 0.3f)
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
        yield return new WaitForSeconds(3);
        isBiting = false;
    }

    public void TakeDamage(int damage)
    {
        zombieHealth -= damage;

        if (zombieHealth <= 0)
        {
            Destroy(gameObject);
            playerScript.AddMoney(10);
        }
    }

}
