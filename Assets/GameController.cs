using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform road;
    public float roadPosition = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (roadPosition < 50)
        {
            Instantiate(road, new Vector3(0, 0, roadPosition), road.rotation);
            roadPosition += 2;
        }
    }
}
