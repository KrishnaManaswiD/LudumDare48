using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class surferController : MonoBehaviour
{
    public float horizontalVelocity = 0;
    public float verticalVelocity = 0;
    public float rotationAboutForward = 0;
    public float rotationAboutVertical = 0;
    
    public int  currentLaneNumber = 3; // 1, 2, 3, 4, 5

    public string movementLock = "n";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(horizontalVelocity, verticalVelocity, 4);
        GetComponent<Rigidbody>().angularVelocity = new Vector3(0, rotationAboutVertical, rotationAboutForward);

        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLaneNumber > 1 && movementLock == "n")
        {
            horizontalVelocity = -3.0f;
            verticalVelocity = currentLaneNumber > 3 ? -0.5f : 0.5f;
            rotationAboutForward = -0.5f;
            StartCoroutine(stopSliding());
            currentLaneNumber -= 1;
            movementLock = "y";
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && currentLaneNumber < 5 && movementLock == "n")
        {
            horizontalVelocity = 3.0f;
            verticalVelocity = currentLaneNumber < 3 ? -0.5f : 0.5f;
            rotationAboutForward = 0.5f;
            StartCoroutine(stopSliding());
            currentLaneNumber += 1;
            movementLock = "y";
        }
    }

    // stops player movement after a certain amount of time
    IEnumerator stopSliding()
    {
        yield return new WaitForSeconds(0.5f);
        horizontalVelocity = 0.0f;
        verticalVelocity = 0.0f;
        rotationAboutForward = 0;
        movementLock = "n";
    }
}
