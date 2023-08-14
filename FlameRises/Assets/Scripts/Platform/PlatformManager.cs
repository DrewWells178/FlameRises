using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlatformManager : MonoBehaviour
{

    [SerializeField] private Player player;

    private int counter;

    private bool isBuilding = false;

    public static event Action OnHeightReached;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void checkHeight()
    {
        if ((score % 10 == 0) && score / 10 == counter)
        {
            isBuilding = true;
            counter++;
        }
    }

    void buildPlatform()
    {
        if (isBuilding)
        {
            // Push event to platform spawner based on array of two classes of type and shape.
        }
    }
}
