using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBoundsX : MonoBehaviour
{
    private float leftLimit = -30;
    private float bottomLimit = -5;

    // Update is called once per frame
    void Update()
    {
        // Destroy dogs if x position goes past left limit
        if (transform.position.x < leftLimit)
        {
            Destroy(gameObject);
        }
        // Destroy balls if y position goes below bottom limit
        else if (transform.position.y < bottomLimit)
        {
            Destroy(gameObject);
        }
    }
}
