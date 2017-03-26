using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    // Declares different variables within the code including the different types of variables
    public float moveSpeed;
    public float jumpHeight;
    private float moveVelocity;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool isGrounded;

    private bool doubleJump;

    private Animator anim;

    private bool isDPressed;

    private bool canCrouch;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
    // Update is called infinitely
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        isDPressed = Input.GetKey(KeyCode.D);
        canCrouch = isGrounded & Input.GetKey(KeyCode.S);
    }
	// Jump allows the player to jump based on a fixed value
    public void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, jumpHeight);
    }

	// Update is called once per frame
	void Update () {
        // Creates condition isGrounded
        if (isGrounded)
        {
            doubleJump = false;
        }

        if (canCrouch)
        {
            GetComponent<BoxCollider2D>().= new Vector2(1.6f, 1.7517305f);
        }
        else
        {
            GetComponent<BoxCollider2D>().size = new Vector2(1.6f, 3.503461f);
        }

        //Binds the jump function to the W key, and checks if player is grounded
		if(Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            Jump();

        }
        
        // Allows the player to double jump if both isGrounded and doubleJump conditions are false
        if (Input.GetKeyDown(KeyCode.W) && !isGrounded && !doubleJump)
        {
            Jump();
            doubleJump = true;

        }

        moveVelocity = 0f;
       
        //Binds positive movement (To the right) to the D key

        if (Input.GetKey(KeyCode.D))
        {
            //GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            moveVelocity = moveSpeed;
        }

        //Binds negative movement (To the left) to the A key
        if (Input.GetKey(KeyCode.A))
        {
            //GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            moveVelocity = -moveSpeed;
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocity,GetComponent<Rigidbody2D>().velocity.y);

        //Allows animations to switch when a fixed condition is met
        anim.SetFloat("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
        anim.SetBool("Grounded", isGrounded);
        anim.SetBool("Is D Pressed", isDPressed);
        anim.SetBool("Double Jump", doubleJump);
        anim.SetBool("Is Crouched", canCrouch);
        //Switches direction of player
        if (GetComponent<Rigidbody2D>().velocity.x > 0)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else if (GetComponent<Rigidbody2D>().velocity.x < 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);
    } 
}
