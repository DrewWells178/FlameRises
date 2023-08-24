using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] LayerMask lm;
    private Rigidbody2D rb;
    private BoxCollider2D bc2;

    private Vector3 posUp;
    private Vector3 posDown;
    private Vector3 moveDirection;

    [SerializeField] private float moveSpeed = 4f;

    private bool isClose = false;
    
    // Start is called before the first frame update
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
        if(isClose)
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

    public void Die()
    {
        //Instantiate() some gameObject death effect
        // GameOver
        Destroy(gameObject);
    }

    
    void movePlat()
    {
        if(isPlatUp() && isPlatDown())
        {
            moveDirection = (transform.position - posUp) + (transform.position - posDown);
            moveDirection = moveDirection.normalized;
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
        else if(isPlatUp())
        {
            moveDirection = transform.position - posUp;
            moveDirection = moveDirection.normalized;
            //Debug.Log(transform.position);
            //Debug.Log(posUp);
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
        else if(isPlatDown())
        {
            moveDirection = transform.position - posDown;
            moveDirection = moveDirection.normalized;
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
            isClose = false;
        }
    }

    bool checkPlatforms()
    {
        if(isPlatUp() || isPlatDown())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    
    private bool isPlatUp()
    {
        RaycastHit2D rayCastWallCheck = Physics2D.BoxCast(bc2.bounds.center, bc2.bounds.size * .2f, 0f, Vector2.up, 3f, lm);
        if(rayCastWallCheck.collider != null)
        {
            posUp = rayCastWallCheck.transform.position;
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool isPlatDown()
    {
        RaycastHit2D rayCastWallCheck = Physics2D.BoxCast(bc2.bounds.center, bc2.bounds.size * .2f, 0f, Vector2.down, 3f, lm);
        if(rayCastWallCheck.collider != null)
        {
            posDown = rayCastWallCheck.transform.position;
            return true;
        }
        else
        {
            return false;
        }
    }
    
}
