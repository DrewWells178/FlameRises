using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlatformManager : MonoBehaviour
{

    [SerializeField] private Player player;

    private int counter;

    public static event Action OnHeightReached;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
