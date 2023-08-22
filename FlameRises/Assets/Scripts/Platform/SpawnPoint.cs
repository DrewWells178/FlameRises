using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private PlatformManager manager;
    Vector3 v;
    float y;
    public GameObject platform;

    
    public int spawnIndex;

    // Random position variables.
    private Random rnd = new Random();
    public float upperBound = 2f;
    public float lowerBound = 2f;



    void Start()
    {       
        PlatformManager.OnHeightReached += Spawn_Platform;        
    }

    void Update()
    {
        Move_With_Player();
    }
    
    void Spawn_Platform(Vector2[,] data)
    {
        for(int i = 0; i < manager.maxPossiblePlatSpawned; i++)
        {
            if (data[spawnIndex, i].x != -1)
            {
                //Spawn something
            }
            else
            {
                //return;
            }
        }
        Debug.Log("we got here");
        Instantiate(platform, Randomize_Position(), transform.rotation);
    }

    void Move_With_Player()
    {
        v = player.position;
        y = v.y;
        v = transform.position;
        v.y = y + 20f;
        transform.position = v;
    }

    Vector3 Randomize_Position()
    {
        double range = (double)(transform.position.x + upperBound) - (double)(transform.position.x - lowerBound);
        double sample = rnd.NextDouble();
        double scaled = (sample * range) + (double)(transform.position.x - lowerBound);
        float f = (float)scaled;

        range = (double)(transform.position.x + upperBound) - (double)(transform.position.x - lowerBound);
        sample = rnd.NextDouble();
        scaled = (sample * range) + (double)(transform.position.x - lowerBound);
        float g = (float)scaled;

        Vector3 v1 = new Vector3(f, g, 0);
        return v1;
    }
}
