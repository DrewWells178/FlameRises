using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private float speed = 2f;
    private Rigidbody2D rb;
    private BoxCollider2D bc2;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        bc2 = transform.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(rb.velocity.x, speed);
    }
    /*private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name);
       
            PlayerScript player = hitInfo.GetComponent<PlayerScript>();
            if (player != null)           
            {
                player.TakeDamage(damage);
            }          
        // Also destroy other game objects: platforms, powerups. Not walls or barriers. Way of cleaning up game memory.
       
    }
    */
}
