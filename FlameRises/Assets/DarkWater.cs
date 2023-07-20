using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkWater : MonoBehaviour
{
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
       
    }
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.tag);
       
        Platform platform = hitInfo.GetComponent<Platform>();
        if (platform != null)           
        {
            platform.Die();
        }      
    }
   
}
