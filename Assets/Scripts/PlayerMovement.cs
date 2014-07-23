using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float maxSpeed = 10f;
    bool facingRight = true;

    private Animator playerAnimator;

    public float jumpForce = 700f;

    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    void Start()
    {
        playerAnimator = gameObject.GetComponent<Animator>();
    }

    void FixedUpdate ()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        
        playerAnimator.SetBool("Ground", grounded);

        playerAnimator.SetFloat("vSpeed", rigidbody2D.velocity.y);

        Movement();
	}

    void Update()
    {
        //TODO criar um botao generico para pular
        if(grounded && Input.GetKeyDown(KeyCode.Space))
        {
            playerAnimator.SetBool("Ground", false);
            rigidbody2D.AddForce(new Vector2(0, jumpForce));
        }
    }

    void Movement()
    {
        float move = Input.GetAxis("Horizontal");

        playerAnimator.SetFloat("Speed", Mathf.Abs(move));

        rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);

        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
