using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;
using Unity.XR.CoreUtils;

public class GameManager : MonoBehaviour
{
    public InputActionReference menuAction; 
    public Canvas pauseMenu;
    private bool isPaused = false;
    private bool isEnded = false;
    public GameObject endMenu;
    public Light light;

    private void OnEnable()
    {
        menuAction.action.performed += PauseManager;
        menuAction.action.Enable();
    }

    private void OnDisable()
    {

        menuAction.action.performed -= PauseManager;
        menuAction.action.Disable();
    }

    public void PauseManager(InputAction.CallbackContext obj)
    {
        if (isPaused){
            ResumeGame();
        } else {
            PauseGame();
        }
    }
    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }

    public void EndGame(bool success)
    {
        isEnded = true;
        Time.timeScale = 0;
        endMenu.GetComponent<Canvas>().enabled = true;
        if (!success)
        {
            //TODO Menu text + score
            endMenu.transform.Find("Score").GetComponent<TMP_Text>().text = "Test";
            light.color = Color.red;
        } else
        {
            //TODO Menu text + score
            light.color = Color.yellow;
        }
    }

    public void PauseGame()
    {
        if (!isEnded)
        {
            light.color = Color.gray;
            Debug.Log("Pause");
            Time.timeScale = 0;
            isPaused = !isPaused;
            pauseMenu.enabled = true;
        }
    }
    public void ResumeGame()
    {
        if (!isEnded)
        {
            light.color = Color.white;
            Debug.Log("Resume");
            Time.timeScale = 1;
            isPaused = !isPaused;
            pauseMenu.enabled = false;
        }
    }

    public void ReturnToLobby()
    {
        SceneManager.LoadScene("Demarrer");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
