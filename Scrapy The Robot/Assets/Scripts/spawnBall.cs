using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnBall : MonoBehaviour
{

    public Transform ballSpawnPoint;
    public Transform ballSpawnPoint2;
    public GameObject ballPrefab;
    public bool booler = true;

    // Start is called before the first frame update
    public float timeRemaining = 6;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        } else
        {
            if (booler)
            {
                var ball = Instantiate(ballPrefab, ballSpawnPoint.position, ballSpawnPoint.rotation);
                booler = false;
            } else
            {
                var ball = Instantiate(ballPrefab, ballSpawnPoint2.position, ballSpawnPoint2.rotation);
                booler = true;
            }
            timeRemaining = 6;

        }
        

    }
}
