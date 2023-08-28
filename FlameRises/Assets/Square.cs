using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : Surfaces
{
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        bc2 = transform.GetComponent<BoxCollider2D>();

        isClose = checkPlatforms();
        this.enabled = true;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isClose)
        {
            movePlat();
        }
        else
        {
            Debug.Log("Kinematic right away");
            rb.isKinematic = true;
            this.enabled = false;
        }
    }
}
