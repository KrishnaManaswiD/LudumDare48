using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform road;
    private float roadSpawnPosition = 0;

    public Transform asteroid;

    // Start is called before the first frame update
    void Start()
    {
        

        // create asteroids
        Instantiate(asteroid, new Vector3(1.0f, 1.5f, 10.0f), asteroid.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        // create road
        if (roadSpawnPosition < 50)
        {
            Instantiate(road, new Vector3(0, 0, roadSpawnPosition), road.rotation);
            roadSpawnPosition += 2;
        }


    }
}
