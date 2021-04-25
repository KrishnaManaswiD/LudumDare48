using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidController : MonoBehaviour
{

    private void Update()
    {
        float randomRotationVelocity = Random.Range(1f, 3f); 
        GetComponent<Rigidbody>().angularVelocity = new Vector3(randomRotationVelocity, randomRotationVelocity, randomRotationVelocity);
    }

    // destroy this game object if it goes off camera
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
