using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float bulletSpeed = 50.0f;
    public int damage = 0;
    public Transform tip;
    public Transform end;
    public LayerMask mask;

    private void Start()
    {
        Destroy(gameObject, 5);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * bulletSpeed);

        if (Physics.Raycast(tip.position, tip.forward, out RaycastHit hit, mask))
        {
            float distance = hit.distance;

            if (distance < 0.3f)
            {
                Debug.Log(hit.transform.name);

                ZombieScript zombie = hit.transform.GetComponent<ZombieScript>();

                if (zombie != null)
                {
                    zombie.TakeDamage(damage);
                    Destroy(gameObject);

                }
            }
            
        }
    }


}
