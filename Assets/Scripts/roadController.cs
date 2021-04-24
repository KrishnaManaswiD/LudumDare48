using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roadController : MonoBehaviour
{
    // destroy this game object if it goes off camera
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
