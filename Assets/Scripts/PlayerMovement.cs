using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEditor.SearchService;

public class PlayerMovement : MonoBehaviour
{
    
    private float horizontal;
    bool isFacingRight = true;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private Animator anim;

    private float wallSlidingSpeed = 2f;
    private bool isWallSliding;


    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(2.5f, 12f);


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded()) 
            {
                rb.velocity = new UnityEngine.Vector2(rb.velocity.x, jumpForce);
                anim.SetTrigger("jump");
            }
        if(gameObject.transform.position.y < -10)
        {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        WallSlide();
        WallJump();

        anim.SetFloat("Yvelocity", rb.velocity.y);
        anim.SetBool("Run", horizontal != 0);
        anim.SetBool("isWallSliding", isWallSliding);

        if (!isWallJumping)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            Flip();
        }
        
    }


    private void Flip()
    {
        if(isFacingRight && horizontal < 0 || !isFacingRight && horizontal > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !isGrounded() && horizontal != 0)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if(isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = - transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if(Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;
            anim.SetTrigger("jump");

            if (transform.localScale.x != wallJumpingDirection)
            {
               isFacingRight = !isFacingRight;
                Vector3 ls = transform.localScale;
                ls.x *= -1f;
                transform.localScale = ls; 
            }
        }

        Invoke(nameof(StopWallJumping), wallJumpingDuration);
        
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Finish")
        {
            Destroy(gameObject);
            //temporary solution, not working for multiple scenes
            SceneManager.LoadScene("Level 2");
        }
        else if (collision.tag == "Checkpoint")
        {
            GameObject gameManagerObject = GameObject.FindGameObjectWithTag("Game Manager");
            GameManager gameManager = gameManagerObject.GetComponent<GameManager>();
            if (gameManager == null)
            {
                Debug.Log("Checkpoint passed");
            }
            gameManager.SetPlayerSpawnPos(collision.transform.position);
        }
    }

}
