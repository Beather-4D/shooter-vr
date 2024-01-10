using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    private int playerHealth = 50;
    private int playerMoney = 0;
    public TMP_Text hpIndicator;
    public TMP_Text moneyIndicator;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        hpIndicator.text = playerHealth.ToString();
        moneyIndicator.text = playerMoney.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DealDamage(int damage)
    {
        playerHealth -= damage;
        hpIndicator.text = playerHealth.ToString();
        if (playerHealth <= 0)
        {
            gameManager.EndGame(false);
        }
    }

    public void UseHeal(int healValue)
    {
        Debug.Log("playerHealth before heal : " + playerHealth.ToString());
        Debug.Log("heal value : " + healValue.ToString());
        playerHealth += healValue;
        playerHealth = Mathf.Min(playerHealth, 50);
        hpIndicator.text = playerHealth.ToString();

        Debug.Log("playerHealth after heal : " + playerHealth.ToString());
    }


    public int GetMoney()
    {
        return playerMoney;
    }

    public void AddMoney(int money)
    {
        playerMoney += money;
        moneyIndicator.text = playerMoney.ToString();
    }

    public void RemoveMoney(int money) {
        playerMoney -= money;
        moneyIndicator.text = playerMoney.ToString();
    }

    
}
