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

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 100;
        isGameOver = false;

        // spawn player
        Instantiate(player);
        player.transform.position = playerStartingPosition;

        // create initial road
        for (int i = 0; i < numberOfRoadPiecesInfrontOfPlayer; i++)
        {
            SpawnRoad();
        }

        // spawn blackhole
        Instantiate(blackhole);
        Debug.Log(roadSpawnPosition);
        blackhole.transform.position = new Vector3(0, 0, roadSpawnPosition);
    }

    // Update is called once per frame
    void Update()
    {
        // create road
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (playerTransform.position.z > (roadSpawnPosition - numberOfRoadPiecesInfrontOfPlayer * 1))
        {
            SpawnRoad();
            float randomNumber = Random.Range(1f, 10f);
            SpawnAsteroid(randomNumber);
            SpawnRipple(randomNumber);
            SpawnBoost(randomNumber);
        }

        // update position of blackhole
        

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
            newBoost.transform.position = new Vector3(randomPosition, 1f, roadSpawnPosition);
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
