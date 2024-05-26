using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{

    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;

    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);
    [SerializeField] AudioClip playerDeathSFX;
    [SerializeField] GameObject arrow;
    [SerializeField] Transform bow;


    float gravityScaleAtStart;
    public bool isAlive = true;
    

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        gravityScaleAtStart = myRigidbody.gravityScale;
        myAnimator.SetBool("isShooting", false);
    }

    void Update()
    {
        if(!isAlive)
        {
            return;
        }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
        
    }

    void OnFire(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }

        Instantiate(arrow, bow.position, transform.rotation);
       
        if (value.isPressed)
        {
            myAnimator.SetBool("isShooting", true);
        }
        else
        {
            myAnimator.SetBool("isShooting", false);
        }
    }

    void OnMove(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
            
    
        if (value.isPressed)
            {
                myRigidbody.velocity += new Vector2(0f, jumpSpeed);
            }

       
        
    }

  

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        
        bool playerHasHorizontalMovement = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalMovement)
        {
            myAnimator.SetBool("isRunning", true);
        }
        else
        {
            myAnimator.SetBool("isRunning", false);
        }

    }

    void FlipSprite()
    {
        bool playerHasHorizontalMovement = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if(playerHasHorizontalMovement)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }

    }

    void ClimbLadder()
    {

        bool playerHasVerticalMovement = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;

        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myRigidbody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("isClimbing", false);
            return;
        }
        
       
            Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
            myRigidbody.velocity = climbVelocity;
            myRigidbody.gravityScale = 0f;

        if(playerHasVerticalMovement)
        {
            myAnimator.SetBool("isClimbing", true);
        }
        else
        {
            myAnimator.SetBool("isClimbing", false);
        }
            
    }

    void Die()
    {
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")) || myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            AudioSource.PlayClipAtPoint(playerDeathSFX, Camera.main.transform.position);
            myRigidbody.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    

}
