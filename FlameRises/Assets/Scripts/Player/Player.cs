using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{

    public int score = 0;
    public float height;
    private Rigidbody2D rb;

    private float timeElapsed = 0f;
       

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        score = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        getScore();
        timeElapsed += Time.deltaTime;
        Debug.Log(transform.position.y / timeElapsed);
    }

    void getScore()
    {
        height = transform.position.y;
        if((int)height > score)
        {
            score = (int)height;                      
        }
             

    } 
 
}
