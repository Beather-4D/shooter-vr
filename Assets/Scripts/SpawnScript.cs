using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject zombiePrefab;
    private List<GameObject> zombies;
    private int rdmIndex;


    void Start()
    {
        zombies = new List<GameObject>();
        StartCoroutine(SpawnZombies());
    }

    IEnumerator SpawnZombies()
    {
        while(true)
        {
            rdmIndex = Random.Range(0,spawnPoints.Length);
            GameObject zombie = Instantiate(zombiePrefab, spawnPoints[rdmIndex].transform, false);
            zombies.Add(zombie);
            
            yield return new WaitForSeconds(3);
        }
    }


}
