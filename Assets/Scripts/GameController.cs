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

    public GameObject asteroid;
    private float asteroidPosition = 0.0f;

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
            SpawnAsteroid();
        }

        SpawnBoost();
        
    }

    // Update is called once per frame
    void Update()
    {
        // create road
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (playerTransform.position.z > (roadSpawnPosition - numberOfRoadPiecesInfrontOfPlayer * 1))
        {
            Debug.Log("need road");
            SpawnRoad();
            SpawnAsteroid();
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
        asteroidPosition = Random.Range(-5, 5);
        // create asteroids
        if (Mathf.Abs(asteroidPosition % 2.0f) < 0.5)
        {
            GameObject newAsteroid;
            newAsteroid = Instantiate(asteroid) as GameObject;
            newAsteroid.transform.position = new Vector3(asteroidPosition, 1.5f, roadSpawnPosition);
        }
    }

    private void SpawnBoost()
    {
        GameObject newBoost;
        newBoost = Instantiate(boost) as GameObject;
        newBoost.transform.position = new Vector3(0, 0.7f, 20.0f);
    }
}
