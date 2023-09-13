using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Surfaces : MonoBehaviour
{
    [SerializeField] LayerMask lm;
    public Rigidbody2D rb;
    public BoxCollider2D bc2;
    public float radius = 2f;

    public Vector3 posUp;
    public Vector3 posDown;
    public Vector3 moveDirection;

    [SerializeField] public float moveSpeed = 4f;

    public bool isClose;

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

    public void Begin()
    {
        if(!isClose)
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * 0f;
            rb.isKinematic = true;
            //this.enabled = false;
        }
        else
        {
            checkPlatforms();
            //SurfaceClose();
            //Move();
        }
    }

    public void Die()
    {
        //Instantiate() some gameObject death effect
        // GameOver
        Destroy(gameObject);
    }

    public void checkPlatforms()
    {
        bool check = false;
        List<Collider2D> hitColliders = Physics2D.OverlapCircleAll(transform.position, radius).ToList();
        
        if(hitColliders.Contains(bc2))
        {
            hitColliders.Remove(bc2);
        }

        if(!hitColliders.Any())
        {
            isClose = false;
        }
        
        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.tag == "Platform" && hitCollider.tag != "Player" && hitCollider.tag != "Barrier")
            {
                Surfaces surface = hitCollider.GetComponent<Surfaces>();
                if(surface != null)
                {
                    surface.Move(transform.position);
                    isClose = true;
                    check = true;
                }
            }

            if(!check)
            {
                isClose = false;
            }
        }
    }

    /*
    public void SurfaceClose()
    {
        var hitColliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            Surfaces surface = hitCollider.GetComponent<Surfaces>();
            if(surface != null)
            {
                //moveDirection += (transform.position - surface.transform.position);// move vector (of normalized vectors of other platforms)
            }
        }
        if(moveDirection == new Vector3(0f,0f,0f))
        {
            //moveDirection = new Vector2(transform.position.x, transform.position.y);
        }
        //moveDirection = moveDirection.normalized;
        checkPlatforms();
    }
    */

    public void Move(Vector3 v)
    {
        v = transform.position - v;
        v = v.normalized;
        rb.velocity = new Vector2(v.x, v.y) * moveSpeed;
        //moveSpeed  = moveSpeed * .5f;
    }
}
