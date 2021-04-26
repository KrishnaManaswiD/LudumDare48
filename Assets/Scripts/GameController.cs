using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // variables accessible by all scripts
    public static float forwardVelocityBooster = 1;
    public static float timeSpent = 0.0f;
    public static bool boostMode = false;

    public static float playerHealth = 100f;
    public static bool isGameOver = false;

    // variables specific to this script
    public GameObject road;
    private float roadSpawnPosition = 0;
    private readonly float roadPieceDepth = 2.0f;
    private readonly int numberOfRoadPiecesInfrontOfPlayer = 25;

    public GameObject ripple;
    public GameObject asteroid;
    public GameObject[] asteroidsList = new GameObject[4];
    public GameObject boost;

    public GameObject player;
    private Transform playerTransform;
    private Vector3 playerStartingPosition = new Vector3(0, 1.01f, 0);

    public GameObject blackhole;
    public GameObject bkg;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 100;
        isGameOver = false;
        roadSpawnPosition = 0;

        // spawn player
        player = Instantiate(player); // not good code. 
        player.transform.position = playerStartingPosition;

        // create initial road
        for (int i = 0; i < numberOfRoadPiecesInfrontOfPlayer; i++)
        {
            SpawnRoad();
        }

        // spawn blackhole
        //blackhole = Instantiate(blackhole); // not good code
        //blackhole.transform.position = new Vector3(0, 0, roadSpawnPosition);
        //blackhole.transform.position = player.transform.position + new Vector3(0,0,100f);

        bkg = Instantiate(bkg); // not good code
        bkg.transform.position = player.transform.position + new Vector3(0, 0, 120f);
    }

    // Update is called once per frame
    void Update()
    {
        // create road
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (playerTransform.position.z > (roadSpawnPosition - numberOfRoadPiecesInfrontOfPlayer * roadPieceDepth))
        {
            SpawnRoad();
            float randomNumber = Random.Range(1f, 10f);
            SpawnAsteroid(randomNumber);
            SpawnRipple(randomNumber);
            SpawnBoost(randomNumber);
            //blackhole.transform.position = new Vector3(0, 0, roadSpawnPosition + 50);
            bkg.transform.position = new Vector3(0, 0, roadSpawnPosition + 60);
        }

        // update time spent
        timeSpent += Time.deltaTime;

        if (playerHealth <= 0f)
        {
            isGameOver = true;
        }

        if (isGameOver)
        {
            HandleGameOver();
        }
    }

    private void SpawnRoad()
    {
        GameObject newRoad;
        newRoad = Instantiate(road) as GameObject;
        newRoad.transform.position = new Vector3(0, 0, roadSpawnPosition);
        roadSpawnPosition += roadPieceDepth;
    }

    private void SpawnAsteroid(float randomNum)
    {
        if (randomNum > 8)
        {
            float randomPosition = Random.Range(-5f, 5f);
            int randomAsteroid = Random.Range(0, 3);
            GameObject newAsteroid;
            newAsteroid = Instantiate(asteroidsList[randomAsteroid]) as GameObject;
            newAsteroid.transform.position = new Vector3(randomPosition, 1f, roadSpawnPosition);
        }
    }

    private void SpawnBoost(float randomNum)
    { 
        if (randomNum > 3f && randomNum < 3.2f)
        {
            float randomPosition = Random.Range(-5f, 5f);
            GameObject newBoost;
            newBoost = Instantiate(boost) as GameObject;
            newBoost.transform.position = new Vector3(randomPosition, 0f, roadSpawnPosition);
        }
    }

    private void SpawnRipple(float randomNum)
    {

        if (randomNum < 1.01f)
        {
            GameObject newRipple;
            newRipple = Instantiate(ripple) as GameObject;
            newRipple.transform.position = new Vector3(0, 1, roadSpawnPosition);
        }
        
    }

    private void HandleGameOver()
    {
        SceneManager.LoadSceneAsync("GameOver");
    }
}
