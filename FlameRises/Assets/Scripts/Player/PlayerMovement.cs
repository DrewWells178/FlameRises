using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Player initialization variables
    private Rigidbody2D rb;
    private BoxCollider2D bc2;
    [SerializeField] LayerMask lm;

    // Player input variables
    float inputHorizontal;

    // Jumping Variables
    public float jumpForce = 15f;

    // Running Variables
    public float runSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        bc2 = transform.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        KBM_Run();
        KBM_Jump();
        KBM_WallSliding();
    }

    void KBM_Run()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        if (inputHorizontal != 0)
        {
            rb.velocity = new Vector2(inputHorizontal * runSpeed, rb.velocity.y);
        }
        else if (inputHorizontal == 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    void KBM_Jump()
    {
        Debug.Log("WeGOTHere");
        Debug.Log(isGrounded());
        Debug.Log("ShouldBeJumping");
        if (Input.GetKeyDown("space") && isGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void KBM_WallSliding()
    {
        if (!isGrounded() && isSliding())
        {
            Debug.Log("Should Be Slow");
            rb.velocity = new Vector2(rb.velocity.x, -.9f);           
        }        
    }

    bool isGrounded()
    {
        RaycastHit2D rayCastGroundCheck = Physics2D.BoxCast(bc2.bounds.center, bc2.bounds.size, 0f, Vector2.down, .12f, lm);
        return rayCastGroundCheck.collider != null;
    }

    private bool isWallRight()
    {
        RaycastHit2D rayCastWallCheck = Physics2D.BoxCast(bc2.bounds.center, bc2.bounds.size * .7f, 0f, Vector2.right, .3f, lm);
        return rayCastWallCheck.collider != null;
    }

    private bool isWallLeft()
    {
        RaycastHit2D rayCastWallCheck = Physics2D.BoxCast(bc2.bounds.center, bc2.bounds.size * .7f, 0f, Vector2.left, .3f, lm);
        return rayCastWallCheck.collider != null;
    }

    private bool isSliding()
    {
        if (isWallLeft() && inputHorizontal < 0)
        {
            return true;
        }
        else if (isWallRight() && inputHorizontal > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
