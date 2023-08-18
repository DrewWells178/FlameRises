using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;
using UnityEngine.Events;

public class PlatformManager : MonoBehaviour
{
    // Player height variables
    [SerializeField] private Player player;
    private int counter = 1;
    private bool isBuilding = false;

    // Random
    private Random rnd = new Random();

    // Random numbers generated
    private int numberPlatforms;
    private int temp;

    // Events
    public static event Action OnHeightReached;

    // Basic Parameters
    public int numPlatSpawners = 3;
    public int minPlatformsSpawned = 3;
    public int maxPossiblePlatSpawned = 8;
    public int spawnIncrement = 20;
    public float upperBound = .66f;
    public float lowerBound = .33f;

    private int cycleHeight = 200;
    private int cycleCurrentCount = 0;
    private int cycleTicks;
    private int state = 0;

    private float boundI = .6f;
    private float boundV = .6f;



    public Vector2[,] data;

    void Start()
    {
        data = new Vector2[numPlatSpawners, maxPossiblePlatSpawned];
        cycleTicks = cycleHeight / spawnIncrement;
    }

    void Update()
    {
        checkHeight();
        buildPlatforms();
    }

    void checkHeight()
    {        
        if ((player.score % spawnIncrement == 0) && player.score / spawnIncrement == counter)
        {
            isBuilding = true;
            counter++;            
        }        
    }

    void buildPlatforms()
    {
        if (isBuilding)
        {
            // reset the array
            reset();
            
            // determine # if plats to spawn, which spawners spawn them, and the shape to be spawned
            getPlatCount();
            
            // Push event to platform spawner based on array of two classes of type and shape.
        }
        isBuilding = false;
    }

    void getPlatCount()
    {
        // Generate number of platforms between minimum and maximum number of platforms
        numberPlatforms = rnd.Next(minPlatformsSpawned, maxPossiblePlatSpawned);
        for(int i = 0; i < numberPlatforms; i++)
        {
            bool found = false;
            int j = 0;
            temp = rnd.Next(0, numPlatSpawners);
            while(found == false)
            {
                if(data[temp,j].x == -1)
                {
                    //set x value to platform shape we want to spawn
                    data[temp, j].x = determinePlatShape();
                    data[temp, j].y = determinePlatType();
                    found = true;
                }
                j++;
            }
        }        
        cycleCurrentCount++;        
    }

    // reset the array
    void reset()
    {
        for(int i = 0; i < numPlatSpawners; i++)
        {
            for(int j = 0; j < maxPossiblePlatSpawned; j++)
            {
                data[i, j].x = -1;
            }
        }
    }

    int determinePlatShape()
    {
        double temp1;
        temp1 = rnd.NextDouble();

        if(temp1 <= lowerBound)
        {
            return 0;
        }
        else if(temp1 > lowerBound && temp1 <= upperBound)
        {
            return 1;
        }
        else if(temp1 > upperBound)
        {
            return 2;
        }
        else
        {
            return 0;
        }
    }

    int determinePlatType()
    {
        double temp1;
        temp1 = rnd.NextDouble();

        if(state == 0) // stone
        {
            if(cycleCurrentCount < cycleTicks)
            {
                // we do stone
                return 0;
            }
            else
            {
                state++;
                cycleHeight = 200 + rnd.Next(-100,100);
                cycleTicks = cycleHeight / spawnIncrement; 
                cycleCurrentCount = 0;
            }
        }
        
        if(state == 1) // ice and stone
        {
            if(cycleCurrentCount < cycleTicks)
            {
                // we do stone and ice
                if(temp1 < boundI)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                state++;
                cycleHeight = 200 + rnd.Next(-100,100);
                cycleTicks = cycleHeight / spawnIncrement; 
                cycleCurrentCount = 0;
            }
        }
        
        if(state == 2) // veg and stone
        {
            if(cycleCurrentCount < cycleTicks)
            {
                // we do stone and veg
                if(temp1 < boundV)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                state = 0;
                cycleHeight = 200 + rnd.Next(-100,100);
                cycleTicks = cycleHeight / spawnIncrement; 
                cycleCurrentCount = 0;
            }
        }
        return 0;
    }

    void printData()
    {
        for (int i = 0; i < numPlatSpawners; i++)
        {
            for(int j = 0; j < maxPossiblePlatSpawned; j++)
            {
                Debug.Log(data[i,j].x);
                Debug.Log(data[i,j].y);
            }
        }       
    }
    
}
