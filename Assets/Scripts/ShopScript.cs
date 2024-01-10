using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    public GameObject soldItem;
    public PlayerScript playerScript;
    public int price;
    public GameObject itemSpawnPosition;
    public TMP_Text priceIndicator;
    public GameObject button;

    private AudioSource audioSource;
    public AudioClip releaseItemSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        priceIndicator.text = price.ToString() + " $";
    }

    public void BuyItem()
    {
        int playerMoney = playerScript.GetMoney();
        StartCoroutine(ButtonClicked());
        if (playerMoney >= price)
        {
            playerScript.RemoveMoney(price);
            Instantiate(soldItem, itemSpawnPosition.transform);
            PlayReleaseItemAudio();
        }
    }

    private void PlayReleaseItemAudio()
    {
        audioSource.clip = releaseItemSound;
        audioSource.volume = UnityEngine.Random.Range(1 - .25f, 1);
        audioSource.pitch = UnityEngine.Random.Range(1 - .2f, 1 + .2f);
        audioSource.PlayOneShot(audioSource.clip);
    }

    IEnumerator ButtonClicked()
    {
        button.SetActive(false);
        yield return new WaitForSeconds(1);
        button.SetActive(true);
    }
}
