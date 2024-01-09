using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int health = 50;
    public int money = 0;
    private int[] inventoryConsummable = {0,0};
    private int[] inventoryAmmo = {0,0};
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DealDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            gameManager.EndGame(false);
        }
    }

    public void AddMoney(int money)
    {
        money += money;
    }
}
