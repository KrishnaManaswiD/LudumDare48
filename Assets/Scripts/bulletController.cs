using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    public float forwardVelocity = 50.0f;
    private Vector3 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = GetComponent<Rigidbody>().position;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, forwardVelocity);
        
        Vector3 currentPosition = GetComponent<Rigidbody>().position;

        if ((currentPosition - spawnPosition).z > 100)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "asteroid")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
