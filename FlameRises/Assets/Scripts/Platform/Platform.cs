using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : Surfaces
{   
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        bc2 = transform.GetComponent<BoxCollider2D>();

        checkPlatforms();
        this.enabled = true;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Begin();
    }
    
}
