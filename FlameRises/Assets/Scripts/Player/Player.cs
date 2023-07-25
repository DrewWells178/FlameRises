using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{

    public int score = 0;
    public float height;
    private Rigidbody2D rb;
    private int counter = 1;

    public static event Action OnHeightReached;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        getScore();
    }

    void getScore()
    {
        height = transform.position.y;
        if((int)height > score)
        {
            score = (int)height;
            isSpawnCheckpoint();           
        }
             

    }

    //This will be how we check if the score has increased enough to spawn platforms.
    void isSpawnCheckpoint()
    {
        if((score % 10 == 0) && score / 10 == counter) 
        {     
            counter++;
            OnHeightReached?.Invoke();            
        }
    }
}
