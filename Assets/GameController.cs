using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform road;
    private float roadSpawnPosition = 0;

    public Transform asteroid;
    private float asteroidPosition = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        

        
    }

    // Update is called once per frame
    void Update()
    {
        // create road
        if (roadSpawnPosition < 50)
        {
            // create road
            Instantiate(road, new Vector3(0, 0, roadSpawnPosition), road.rotation);

            asteroidPosition = Random.Range(-5, 5);
            // create asteroids
            if (Mathf.Abs(asteroidPosition % 2.0f) < 0.5 )
            {
                Instantiate(asteroid, new Vector3(asteroidPosition, 1.5f, roadSpawnPosition), asteroid.rotation);
            }
            

            roadSpawnPosition += 2;
        }


    }
}
