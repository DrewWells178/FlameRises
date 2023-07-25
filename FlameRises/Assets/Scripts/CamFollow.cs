using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField] GameObject player;
    private Rigidbody2D Rb;
    [SerializeField] float threshold;
    private bool above;
    private bool same;
    private Rigidbody2D rb;

    public float speed = 20f;
    
    // Start is called before the first frame update
    void Start()
    {
       player = GameObject.FindGameObjectWithTag("Player");
       Rb = player.GetComponent<Rigidbody2D>();
       rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isAbove();
        Move();
    }

    void Move()
    {
        if(Rb.velocity.y < 0)
        { 
            // move down
            Debug.Log("moving down");
            rb.velocity = new Vector2(0, Rb.velocity.y - (.25f * speed));
            Vector3 targetPos = transform.position;
            targetPos.x = 0f;
            targetPos.y = Helper.Clamp(targetPos.y, -threshold + player.transform.position.y, threshold + player.transform.position.y);
            this.transform.position = targetPos;
        }
        else if(Rb.velocity.y > 0)
        {
            // move up
            Debug.Log("moving up");
            rb.velocity = new Vector2(0, Rb.velocity.y + speed);
            Vector3 targetPos = transform.position;
            targetPos.x = 0f;
            targetPos.y = Helper.Clamp(targetPos.y, -threshold + player.transform.position.y, threshold + player.transform.position.y);
            this.transform.position = targetPos;
        }
        else
        {
            Debug.Log("shits fucked gang gang");
            // move to player
            if(above)
            {
                rb.velocity = new Vector2(0, -.5f * speed);
            }
            else if(same)
            {
                rb.velocity = new Vector2(0, 0);
            }
            else
            {
                rb.velocity = new Vector2(0, .5f * speed);
            }
        }
    }

    void isAbove()
    {
        if(transform.position.y > player.transform.position.y)
        {
            above = true;
        }
        else if(transform.position.y < player.transform.position.y)
        {
            above = false;
        }
        else
        {
            same = true;
        }
    }
}
