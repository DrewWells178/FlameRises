using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    //public Static Helper helper;
    public float baseSpeed = 2f;
    private Rigidbody2D rb;
    private BoxCollider2D bc2;
    public float speed;
    public int counter = 1;
    //player based speed variable
    // height based speed variable
    // need a counter of height

    //height speed variables
    public int speedIncrement;
    private int speedCounter = 1;
    public Player player;
    public float speedChange;

    //Player based speed modifier
    public float playerSpeedModifier;


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        bc2 = transform.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {   
        calcSpeed();
        speed = baseSpeed + (player.speed * playerSpeedModifier) + speedChange;
        rb.velocity = new Vector2(rb.velocity.x, Helper.Clamp(speed, 0f, 3f));        
    }
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name);
       
            Player player = hitInfo.GetComponent<Player>();
            if (player != null)           
            {
                player.Die();
            }          
        // Also destroy other game objects: platforms, powerups. Not walls or barriers. Way of cleaning up game memory.
       
    }
    

    void calcSpeed()
    {
        
        if ((player.score % speedIncrement == 0) && player.score / speedIncrement == counter)
        {
            
            speedChange += speedChange / (float)counter;
            counter++;
        }
                
    }
}
