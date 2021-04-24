using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // variables accessible by all scripts
    public static float forwardVelocityBooster = 1;
    public static float timeSpent = 0.0f;
    public static bool boostMode = false;

    // variables specific to this script
    public GameObject road;
    private float roadSpawnPosition = 0;

    public GameObject ripple;
    public GameObject asteroid;
    public GameObject boost;

    public GameObject player;
    private Transform playerTransform;
    private Vector3 playerStartingPosition = new Vector3(0, 1.01f, 0);

    private int numberOfRoadPiecesInfrontOfPlayer = 25;
    

    // Start is called before the first frame update
    void Start()
    {
        // spawn player
        Instantiate(player);
        player.transform.position = playerStartingPosition;

        // create initial road
        for (int i = 0; i < numberOfRoadPiecesInfrontOfPlayer; i++)
        {
            SpawnRoad();
            //SpawnAsteroid();
            //SpawnRipple();
            //SpawnBoost();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // create road
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (playerTransform.position.z > (roadSpawnPosition - numberOfRoadPiecesInfrontOfPlayer * 1))
        {
            SpawnRoad();
            SpawnAsteroid();
            SpawnRipple();
        }

        // update time spent
        timeSpent += Time.deltaTime;
    }

    private void SpawnRoad()
    {
        GameObject newRoad;
        newRoad = Instantiate(road) as GameObject;
        newRoad.transform.position = new Vector3(0, 0, roadSpawnPosition);
        roadSpawnPosition += 2;
    }

    private void SpawnAsteroid()
    {
        float randomNumber = Random.Range(1f, 10f);
        if (randomNumber > 5)
        {
            float randomPosition = Random.Range(-5f, 5f);
            GameObject newAsteroid;
            newAsteroid = Instantiate(asteroid) as GameObject;
            newAsteroid.transform.position = new Vector3(randomPosition, 1.5f, roadSpawnPosition);
        }
    }

    private void SpawnBoost()
    {
        float randomNumber = Random.Range(1f, 10f);
        if (randomNumber > 3f && randomNumber < 3.4f)
        {
            float randomPosition = Random.Range(-5f, 5f);
            GameObject newBoost;
            newBoost = Instantiate(boost) as GameObject;
            newBoost.transform.position = new Vector3(randomPosition, 0.7f, roadSpawnPosition);
        }
    }

    private void SpawnRipple()
    {
        float randomNumber = Random.Range(1f, 10f);
        Debug.Log(randomNumber);
        if (randomNumber < 1.1f)
        {
            GameObject newRipple;
            newRipple = Instantiate(ripple) as GameObject;
            newRipple.transform.position = new Vector3(0, 1, roadSpawnPosition);
        }
        
    }
}
