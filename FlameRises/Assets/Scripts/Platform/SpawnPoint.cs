using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Transform player;
    Vector3 v;
    float y;
    public GameObject platform;

    void Start()
    {       
        PlatformManager.OnHeightReached += Spawn_Platform;
    }

    void Update()
    {
        Move_With_Player();
    }
    
    void Spawn_Platform()
    {
        Instantiate(platform, transform.position, transform.rotation);
    }

    void Move_With_Player()
    {
        v = player.position;
        y = v.y;
        v = transform.position;
        v.y = y + 20f;
        transform.position = v;
    }
}
