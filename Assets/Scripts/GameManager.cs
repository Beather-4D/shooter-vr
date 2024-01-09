using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void EndGame(bool success)
    {
        Time.timeScale = 0;
    }
}
