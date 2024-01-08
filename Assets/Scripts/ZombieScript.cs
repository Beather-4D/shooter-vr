using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    public Animator zombieAnimator;
    public GameObject player;
    public int zombieHealth = 10;
    public float zombieSpeed = 0.5f;
    public bool isBiting = false;
    private void Update()
    {
        transform.LookAt(player.transform);
        transform.Translate(Vector3.forward * Time.deltaTime * zombieSpeed);
        zombieAnimator.SetFloat("speed", zombieSpeed);
    }

}
