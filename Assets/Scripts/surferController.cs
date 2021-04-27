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

    private float initialJumpVelocity = 6.0f;
    private float timeSinceJump = 0.0f;

    private int  currentLaneNumber = 3; // 1, 2, 3, 4, 5

    private bool movementLock = false;
    private bool jumpLock = false;

    public GameObject bullet;

    public AudioSource asteroidDestroySound;
    public AudioSource jumpSound;
    public AudioSource groundRumbleSound;
    public AudioSource boostSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(horizontalVelocity, verticalVelocity, forwardVelocity * GameController.forwardVelocityBooster);
        GetComponent<Rigidbody>().angularVelocity = new Vector3(0, angularVelocityAboutVertical, angularVelocityAboutForward);

        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLaneNumber > 1 && movementLock == false)
        {
            horizontalVelocity = -7.0f;
            verticalVelocity = currentLaneNumber > 3 ? -0.2f : 0.2f;
            angularVelocityAboutForward = -1.0f;
            StartCoroutine(StopSliding());
            currentLaneNumber -= 1;
            movementLock = true;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && currentLaneNumber < 5 && movementLock == false)
        {
            horizontalVelocity = 7.0f;
            verticalVelocity = currentLaneNumber < 3 ? -0.2f : 0.2f;
            angularVelocityAboutForward = 1.0f;
            StartCoroutine(StopSliding());
            currentLaneNumber += 1;
            movementLock = true;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpLock == false)
        {
            jumpLock = true;
            StartCoroutine(LandBack());
            jumpSound.Play();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireBullet();
        }


        if (jumpLock == true)
        {
            timeSinceJump += Time.deltaTime;

            if (timeSinceJump <= 0.5f)
            {
                verticalVelocity = initialJumpVelocity - (12 * timeSinceJump);
            }
            else if (timeSinceJump > 0.5f && timeSinceJump < 0.9f)
            {
                verticalVelocity = -(float)(12 * (timeSinceJump - 0.5));
            }
            else
            {
                verticalVelocity = 0.0f;
                timeSinceJump = 0.0f;
                Vector3 currentPosition = GetComponent<Rigidbody>().position;
                if (currentLaneNumber == 3)
                    transform.position = new Vector3(currentPosition.x, 1.01f, currentPosition.z);
                else if (currentLaneNumber == 2 || currentLaneNumber == 4)
                    transform.position = new Vector3(currentPosition.x, 1.05f, currentPosition.z);
                else
                    transform.position = new Vector3(currentPosition.x, 1.09f, currentPosition.z);
                jumpLock = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "asteroid")
        {
            asteroidDestroySound.Play();
            Destroy(other.gameObject);
            GameController.playerHealth -= 20;
        }

        if (other.gameObject.tag == "boost")
        {
            boostSound.Play();
            Destroy(other.gameObject);
            GameController.forwardVelocityBooster = 2.0f;
            GameController.boostMode = true;
            StartCoroutine(StopBoost());
        }

        if (other.gameObject.tag == "ripple")
        {
            groundRumbleSound.Play();
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
        movementLock = false;
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

    IEnumerator LandBack()
    {
        yield return new WaitForSeconds(1.0f);
        jumpLock = false;
    }

    private void FireBullet()
    {
        Vector3 bulletPosition = transform.position + new Vector3(0, 0, 1.0f);
        Instantiate(bullet, bulletPosition, bullet.transform.rotation);
    }

}
