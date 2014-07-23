using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public Vector2
         maxVelocity = new Vector2(3, 5);

    public bool standing;
    public float jumpSpeed;

    private Vector2 rigidbodyForce;

    private Animator playerAnimator;
    private PlayerDefaultController pDefController;

    void Start()
    {
        playerAnimator = gameObject.GetComponent<Animator>();
        pDefController = gameObject.GetComponent<PlayerDefaultController>();
    }

	void Update ()
    {
        ProcessInputs();
	}

    void ProcessInputs()
    {
        float forceX = 0f;
        float forceY = 0f;

        float absVelX = Mathf.Abs(rigidbody2D.velocity.x);
        float absVelY = Mathf.Abs(rigidbody2D.velocity.y);

        if(absVelY < .2f)
        {
            standing = true;
        }
        else
        {
            standing = false;
        }

        if(pDefController.moving.x !=0)
        {
            if (absVelX < maxVelocity.x)
                forceX = speed * pDefController.moving.x;

            transform.localScale = new Vector3(pDefController.moving.x, 1, 1);

            playerAnimator.SetInteger("Walk", 1);   
        }
        else
        {
            playerAnimator.SetInteger("Walk", 0);
        }

        if(pDefController.moving.y > 0 && standing)
        {
            if (absVelY < maxVelocity.y)
                forceY = jumpSpeed;
        }

        rigidbodyForce = new Vector2(forceX, forceY);
    }
    void FixedUpdate()
    {
        rigidbody2D.AddForce(rigidbodyForce);
    }
}