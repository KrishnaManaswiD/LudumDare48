using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidController : MonoBehaviour
{
    private int rotationDirection = 0;
    private void Start()
    {
        rotationDirection = Random.Range(-1, 1);
    }

    private void Update()
    {
        float randomRotationVelocity = rotationDirection * Random.Range(0f, 3f); 
        GetComponent<Rigidbody>().angularVelocity = new Vector3(randomRotationVelocity, randomRotationVelocity, randomRotationVelocity);
    }

    // destroy this game object if it goes off camera
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
