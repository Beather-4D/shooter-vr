using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class HealScript : MonoBehaviour
{
    private PlayerScript playerScript;
    private int healAmount = 20;

    private void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
    }
    public void healPlayer()
    {
        playerScript.UseHeal(healAmount);
        Destroy(gameObject);
    }
}
