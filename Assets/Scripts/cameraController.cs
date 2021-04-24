using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    private float forwardVelocity = 20.0f;
    private float horizontalVelocity = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(horizontalVelocity, 0, forwardVelocity * GameController.forwardVelocityBooster);

        if(GameController.boostMode)
        {
            horizontalVelocity = Random.Range(-2f, 2f);
            forwardVelocity = 18.0f;
            StartCoroutine(StopBoost());
        }
        else
        {
            horizontalVelocity = 0;
            forwardVelocity = 20.0f;
        }
    }

    IEnumerator StopBoost()
    {
        yield return new WaitForSeconds(1.4f);
        forwardVelocity = 48.0f;
    }
}

