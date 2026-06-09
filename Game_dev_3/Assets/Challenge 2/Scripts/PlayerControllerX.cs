using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;

    private float cooldownTime = 1.0f;      // seconds between spawns
    private float nextSpawnTime = 0f;       // tracks when player can spawn next

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextSpawnTime)
        {
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
            nextSpawnTime = Time.time + cooldownTime;  // set next allowed spawn time
        }
    }
}