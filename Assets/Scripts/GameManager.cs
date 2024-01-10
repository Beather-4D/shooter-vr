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
    public GameObject lightGameObj;
    private Light light;

    private AudioSource audioSource;
    public AudioClip[] playerDeathSounds;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        light = lightGameObj.GetComponent<Light>();
    }


    //MAPPING
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

    //GAME STATUS
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
        PlayerScript playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
        if (!success)
        {
            //TODO Menu text + score
            endMenu.transform.Find("Score").GetComponent<TMP_Text>().text = "You Died\nScore : " + playerScript.GetMoney().ToString();
            endMenu.transform.Find("Score").GetComponent<TMP_Text>().color = Color.red;
            light.color = Color.red;
            PlayPlayerDeathAudio();
        } else
        {
            //TODO Menu text + score
            int score = playerScript.GetMoney() + 100;  
            endMenu.transform.Find("Score").GetComponent<TMP_Text>().text = "You Escaped\nScore : " + score.ToString();
            endMenu.transform.Find("Score").GetComponent<TMP_Text>().color = Color.yellow;
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

    private void PlayPlayerDeathAudio()
    {
        audioSource.clip = playerDeathSounds[UnityEngine.Random.Range(0, playerDeathSounds.Length)];
        audioSource.volume = UnityEngine.Random.Range(1 - .25f, 1);
        audioSource.pitch = UnityEngine.Random.Range(1 - .2f, 1 + .2f);

        audioSource.PlayOneShot(audioSource.clip);
    }

}
