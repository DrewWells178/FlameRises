using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    // Player initialization variables
    private Rigidbody2D rb;
    private BoxCollider2D bc2;
    [SerializeField] LayerMask lm;
    [SerializeField] LayerMask climbingLayer;
    public EnergyBar energyBar;

    // Player input variables
    float inputHorizontal;

    // Jumping Variables
    public float jumpForce = 18f;

    // Running Variables
    [SerializeField] private float runSpeed = 13f;

    // Jumping Variables
    private float jumpTime = .25f;
    int jumpCount = 2;
    private float jumpTimer;
    private bool isJumping;

    // Wall jumping variables
    float wallJumpingDirection; 
    float wallJumpingCounter;
    float wallJumpingTime = .5f;
    private Vector2 wallJumpingPower = new Vector2(13f, 20f);

    float cantRunTimer = 0f;
    float cantRunTime = .1f;

    // Position variables
    private Vector3 pos;
    private float y;
    private float height;

    // Ledge climb variables
    [HideInInspector] public bool ledgeDetectedRight;
    [HideInInspector] public bool ledgeDetectedLeft;
    
    [SerializeField] private Vector2 offset1R;
    [SerializeField] private Vector2 offset1L;
    [SerializeField] private Vector2 offset2R;
    [SerializeField] private Vector2 offset2L;

    private Vector2 climbBeginPos;
    private Vector2 climbAfterPos;

    private bool canGrabLedge = true;
    private bool canClimb;

    // Flight Variables
    public float flightForce = .15f;
    private int flightEnergy = 3;
    private float flightWait = .4f;
    public float flightSpeed = 18f;
    [SerializeField] private float flightDelay;
    private int energy;
    [SerializeField] private int maxEnergy = 200;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        bc2 = transform.GetComponent<BoxCollider2D>();
        energy = maxEnergy;
        energyBar.SetMaxEnergy(maxEnergy);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(rb.velocity.x, Helper.Clamp(rb.velocity.y, -20f, 30f));
        
        cantRunTimer -= Time.deltaTime;
        if(cantRunTimer <= 0)
        {
            KBM_Run();
        }
        KBM_Jump();
        KBM_WallSliding();
        KBM_WallJumping();
        checkForLedge();
        //KBM_Flight();
        flightWait -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        if(energy < maxEnergy)
        {
            energy += 1;
            energyBar.SetEnergy(energy);
        }
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
        //  && Input.GetKeyDown("space")
        if(isGrounded() && Input.GetKeyDown("space"))
        {
            //flightWait = flightDelay;
            isJumping = true;
            jumpTimer = 0f;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount = 1;
        }
        else if(Input.GetKeyDown("space") && jumpCount > 0)
        {
            //flightWait = flightDelay;
            isJumping = true;
            jumpTimer = .125f;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount = 0;
        }
        
        if (Input.GetKey("space") && isJumping)
        {
            if(jumpTimer < jumpTime)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimer += Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if(Input.GetKeyUp("space"))
        {
            isJumping = false;
        }
        
    }

    void KBM_Flight()
    {
        if(Input.GetKey("space") && !isGrounded() && !isWallLeft() && !isWallRight() && energy > flightEnergy && flightWait <= 0)
        {
            // Do Flight (Apply Force up)
            rb.AddForce(Vector2.up * flightForce, ForceMode2D.Impulse);
            //rb.velocity = new Vector2(rb.velocity.x, flightSpeed);
            energy -= flightEnergy;
            energyBar.SetEnergy(energy);
            Debug.Log(energy);
        }
    }

    void KBM_WallSliding()
    {
        if (!isGrounded() && isSliding())
        {
            rb.velocity = new Vector2(rb.velocity.x, -.9f);           
        }        
    }
          
    void KBM_WallJumping()
    {
        if(isSliding() && !isGrounded())
        {            
            if(isWallLeft())
            {
                inputHorizontal = 1f;
                wallJumpingDirection = 1f;
            }
            else
            {
                inputHorizontal = -1f;
                wallJumpingDirection = -1f;
            }
            wallJumpingCounter = wallJumpingTime;            
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if(Input.GetKeyDown("space") && wallJumpingCounter > 0f)
        {       
            jumpCount = 1;     
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;    
            cantRunTimer = cantRunTime;   
            flightWait = flightDelay;   
        }
    }

    void checkForLedge()
    {
        if(ledgeDetectedRight)
        {
            canGrabLedge = false;

                Vector2 ledgePosition = GetComponentInChildren<RightLedgeDetection>().transform.position;
                climbBeginPos = ledgePosition + offset1R;
                //climbAfterPos = ledgePosition + offset2R;

                canClimb = true;
            

            if(canClimb)
            {
                // start animation
                transform.position = climbBeginPos;
                rb.velocity = new Vector2(0f, 0f);
                isJumping = false;
                LedgeClimbOver();
            } 
        }
        else if(ledgeDetectedLeft)
        {
            canGrabLedge = false;

            Vector2 ledgePosition = GetComponentInChildren<LeftLedgeDetection>().transform.position;
            climbBeginPos = ledgePosition + offset1L;
            //climbAfterPos = ledgePosition + offset2L;

            canClimb = true;

            if(canClimb)
            {
                transform.position = climbBeginPos;
                rb.velocity = new Vector2(0f, 0f);
                isJumping = false;
                LedgeClimbOver();
            } 
        }
    }

    private void LedgeClimbOver()
    {
        canClimb = false;
        //transform.position = climbAfterPos;
        canGrabLedge = true;
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

    private bool isLedgeRight()
    {
        RaycastHit2D rayCastWallCheck = Physics2D.BoxCast(bc2.bounds.center, bc2.bounds.size * .7f, 0f, Vector2.right, 1f, climbingLayer);
        return rayCastWallCheck.collider != null;
    }

    private bool isLedgeLeft()
    {
        RaycastHit2D rayCastWallCheck = Physics2D.BoxCast(bc2.bounds.center, bc2.bounds.size * .7f, 0f, Vector2.left, 1f, climbingLayer);
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
