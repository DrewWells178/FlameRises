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
    public GameObject square;
    public GameObject LSurface;

    public float playerSeperation = 10f;
    
    public int spawnIndex;

    // Random position variables.
    private Random rnd = new Random();
    public float upperBound = 3f;
    public float lowerBound = 3f;



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
                if (data[spawnIndex, i].x == 0) 
                {
                    Instantiate(platform, Randomize_Position(), transform.rotation);
                }
                else if(data[spawnIndex, i].x == 1)
                {
                    Instantiate(square, Randomize_Position(), transform.rotation);
                }
                else
                {
                    Instantiate(LSurface, Randomize_Position(), transform.rotation);
                }
            }
            else
            {
                return;
            }
        }
    }

    void Move_With_Player()
    {
        v = player.position;
        y = v.y;
        v = transform.position;
        v.y = y + playerSeperation;
        transform.position = v;
    }

    Vector3 Randomize_Position()
    {
        double range = (double)(transform.position.x + upperBound) - (double)(transform.position.x - lowerBound);
        double sample = rnd.NextDouble();
        double scaled = (sample * range) + (double)(transform.position.x - lowerBound);
        float f = (float)scaled;

        range = (double)(transform.position.y + 2 * upperBound) - (double)(transform.position.y - 2 * lowerBound);
        sample = rnd.NextDouble();
        scaled = (sample * range) + (double)(transform.position.y - 2 *lowerBound);
        float g = (float)scaled;

        Vector3 v1 = new Vector3(f, g, 0);
        return v1;
    }
}
