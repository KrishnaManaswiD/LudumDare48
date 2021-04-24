using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class surferController : MonoBehaviour
{
    public float forwardVelocity = 4.0f;
    public float horizontalVelocity = 0.0f;
    public float verticalVelocity = 0.0f;
    public float angularVelocityAboutForward = 0.0f;
    public float angularVelocityAboutVertical = 0.0f;
    
    public int  currentLaneNumber = 3; // 1, 2, 3, 4, 5

    public string movementLock = "n";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(horizontalVelocity, verticalVelocity, forwardVelocity);
        GetComponent<Rigidbody>().angularVelocity = new Vector3(0, angularVelocityAboutVertical, angularVelocityAboutForward);

        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLaneNumber > 1 && movementLock == "n")
        {
            horizontalVelocity = -3.0f;
            verticalVelocity = currentLaneNumber > 3 ? -0.5f : 0.5f;
            angularVelocityAboutForward = -0.5f;
            StartCoroutine(stopSliding());
            currentLaneNumber -= 1;
            movementLock = "y";
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && currentLaneNumber < 5 && movementLock == "n")
        {
            horizontalVelocity = 3.0f;
            verticalVelocity = currentLaneNumber < 3 ? -0.5f : 0.5f;
            angularVelocityAboutForward = 0.5f;
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
        angularVelocityAboutForward = 0;
        movementLock = "n";
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "asteroid")
        {
            Destroy(other.gameObject);
            Debug.Log("collided with asteroid");
        }
    }
}
