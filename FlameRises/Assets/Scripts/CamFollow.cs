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

    private float fallTime = .5f;
    private float isFalling;

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
        Vector3 targetPos = transform.position;
        if(Rb.velocity.y < 0)
        { 
            // move down
            isFalling += Time.deltaTime;
            rb.velocity = new Vector2(0, Rb.velocity.y);
            targetPos.x = 0f;
            this.transform.position = targetPos;
            targetPos.y = Helper.Clamp(targetPos.y, -threshold + player.transform.position.y, 1.5f * threshold + player.transform.position.y);
            
            if(isFalling > fallTime)
            {                
                rb.velocity = new Vector2(0, Rb.velocity.y - (5f * speed));
                targetPos.x = 0f;
                this.transform.position = targetPos;
                targetPos.y = Helper.Clamp(targetPos.y, -threshold + player.transform.position.y, 1.5f * threshold + player.transform.position.y);
            }
        }
        else if(Rb.velocity.y > 0)
        {
            // move up
            isFalling = 0f;
            rb.velocity = new Vector2(0, Rb.velocity.y + speed);
            targetPos.x = 0f;
            targetPos.y = Helper.Clamp(targetPos.y, -threshold + player.transform.position.y, 1.5f * threshold + player.transform.position.y);
            this.transform.position = targetPos;
        }
        else
        {
            isFalling = 0f;
            // move to player
            if(above)
            {
                rb.velocity = new Vector2(0, -.1f * speed);
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
