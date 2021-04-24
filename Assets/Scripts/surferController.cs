using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class surferController : MonoBehaviour
{
    private float forwardVelocity = 20.0f;
    private float horizontalVelocity = 0.0f;
    private float verticalVelocity = 0.0f;
    private float angularVelocityAboutForward = 0.0f;
    private float angularVelocityAboutVertical = 0.0f;

    private int  currentLaneNumber = 3; // 1, 2, 3, 4, 5

    private string movementLock = "n";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(horizontalVelocity, verticalVelocity, forwardVelocity * GameController.forwardVelocityBooster);
        GetComponent<Rigidbody>().angularVelocity = new Vector3(0, angularVelocityAboutVertical, angularVelocityAboutForward);

        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLaneNumber > 1 && movementLock == "n")
        {
            horizontalVelocity = -7.0f;
            verticalVelocity = currentLaneNumber > 3 ? -0.2f : 0.2f;
            angularVelocityAboutForward = -1.0f;
            StartCoroutine(StopSliding());
            currentLaneNumber -= 1;
            movementLock = "y";
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && currentLaneNumber < 5 && movementLock == "n")
        {
            horizontalVelocity = 7.0f;
            verticalVelocity = currentLaneNumber < 3 ? -0.2f : 0.2f;
            angularVelocityAboutForward = 1.0f;
            StartCoroutine(StopSliding());
            currentLaneNumber += 1;
            movementLock = "y";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "asteroid")
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "boost")
        {
            GameController.forwardVelocityBooster = 2.0f;
            GameController.boostMode = true;
            StartCoroutine(StopBoost());
        }

        if (other.gameObject.tag == "ripple")
        {
            GameController.forwardVelocityBooster = 0.5f;
            StartCoroutine(StopSlowdown());
        }
    }

    // stops player movement after a certain amount of time
    IEnumerator StopSliding()
    {
        yield return new WaitForSeconds(0.2f);
        horizontalVelocity = 0.0f;
        verticalVelocity = 0.0f;
        angularVelocityAboutForward = 0;
        movementLock = "n";
    }

    // stops boost in forward velocity
    IEnumerator StopBoost()
    {
        yield return new WaitForSeconds(1.5f);
        GameController.forwardVelocityBooster = 1.0f;
        GameController.boostMode = false;
    }

    IEnumerator StopSlowdown()
    {
        yield return new WaitForSeconds(1.0f);
        GameController.forwardVelocityBooster = 1.0f;
    }


}
