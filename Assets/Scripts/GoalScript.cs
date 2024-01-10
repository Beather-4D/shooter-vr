using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{

    public GameObject goalButton;
    public PlayerScript playerScript;
    public GameManager gameManager;


    private void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
    }

    public void FinishGame()
    {
        StartCoroutine(ButtonClicked());
        if(playerScript.GetMoney() >= 200){
            gameManager.EndGame(true);
        }
    }

    IEnumerator ButtonClicked()
    {
        goalButton.SetActive(false);
        yield return new WaitForSeconds(1);
        goalButton.SetActive(true);
    }
}
