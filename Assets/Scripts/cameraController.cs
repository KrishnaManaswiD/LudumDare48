using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    private float forwardVelocity = 20.0f;
    private float horizontalVelocity = 0.0f;
    private float timeElapsed = 0f;

    private float boostWobbleFactor = 1.0f;

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
            horizontalVelocity = Random.Range(-1f, 1f) * boostWobbleFactor;
            if (timeElapsed < 1.3f)
            {
                forwardVelocity = 18.0f;
            }
            else
            {
                forwardVelocity = 33.0f;
            }
            timeElapsed += Time.deltaTime;
        }
        else
        {
            timeElapsed = 0f;
            forwardVelocity = 20.0f;
            horizontalVelocity = 0.0f;
            // camera doesnt exactly come back to same x, so we manually fix it
            Vector3 currentPosition = GetComponent<Rigidbody>().position;
            transform.position = new Vector3(0, currentPosition.y, currentPosition.z);
        }

    }
    /*
    IEnumerator Recover()
    {
        
        yield return new WaitForSeconds(1.4f);
        forwardVelocity = 48.0f;
    }

    IEnumerator StopBoost()
    {
        GameController.boostMode = false;
        yield return new WaitForSeconds(1.5f);
        forwardVelocity = 20.0f;
    }*/
}

