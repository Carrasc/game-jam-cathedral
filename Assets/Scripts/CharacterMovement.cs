using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private float inputX;
    private float inputY;

    private Rigidbody2D rb;
    private Animator anim;

    private Vector3 m_Velocity = Vector3.zero;

    [SerializeField] private float moveSpeed = 300f;
    [SerializeField] private float movementSmoothing = 0.07f;	// How much to smooth out the movement

    private bool isLookingRight = true;

    [SerializeField] private LayerMask whatIsGround;
    private Transform groundCheck; // A position marking where to check if the player is grounded.
    const float k_GroundedRadius = .01f; // Radius of the overlap circle to determine if grounded

    private bool isGrounded;            // Whether or not the structure is grounded.
    private bool isClimbing;

    // Variables for the restorer to check for agents
    private bool inAgentRange;
    private Transform agentCheck; // A position marking where to check if the constructor is in range
    const float k_AgentRadius = 0.5f; // Radius of the overlap circle to determine if player in range
    [SerializeField] private LayerMask whatIsAgent;

    public static bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // The ground check is the first child of the structure object
        if (gameObject.transform.GetChild(0))
            groundCheck = gameObject.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (CharacterController.activeCharacter == gameObject && canMove)
        {
            gameObject.GetComponent<Animator>().SetBool("isSelected", true);
            inputX = Input.GetAxisRaw("Horizontal");
            inputY = Input.GetAxisRaw("Vertical");
            UpdateAnimations();
        }
        else
        {
            anim.SetFloat("xVelocity", 0f);
            anim.SetBool("isClimbing", false);
            gameObject.GetComponent<Animator>().SetBool("isSelected", false);
        }



    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (CharacterController.activeCharacter == gameObject && canMove)
        {
            MovePlayer();
        }

        // The character is NOT selected, but should still apply gravity and stay in the stairs
        else
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, k_GroundedRadius, whatIsGround);

            if (isGrounded)
            {
                rb.gravityScale = 0;
                Vector3 targetVelocity = new Vector2(0f, 0f); // Move the character by finding the target velocity
                rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, movementSmoothing); // And then smoothing it out and applying it to the character
            }
            else
            {
                isClimbing = false;
                rb.gravityScale = 3;
                Vector3 targetVelocity = new Vector2(0, rb.velocity.y); // Move the character by finding the target velocity
                rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, movementSmoothing); // And then smoothing it out and applying it to the character
            }
        }


    }

    void MovePlayer()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, k_GroundedRadius, whatIsGround);

        if (isGrounded)
        {
            rb.gravityScale = 0;
            Vector3 targetVelocity;

            if (inputY != 0)
            {
                isClimbing = true;
                targetVelocity = new Vector2(0f, inputY * Time.fixedDeltaTime * moveSpeed / 2); // Move the character by finding the target velocity
            }
            else
            {
                isClimbing = false;
                targetVelocity = new Vector2(inputX * Time.fixedDeltaTime * moveSpeed, inputY * Time.fixedDeltaTime * moveSpeed / 2); // Move the character by finding the target velocity
            }

            // And then smoothing it out and applying it to the character
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, movementSmoothing);
        }
        else
        {
            isClimbing = false;
            rb.gravityScale = 3;
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(inputX * Time.fixedDeltaTime * moveSpeed, rb.velocity.y);
            // And then smoothing it out and applying it to the character
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, movementSmoothing);
        }


        // Check the inputX to know if we should rotate the player
        if (inputX > 0 && !isLookingRight)
        {
            // If he is climbing, dont rotate him
            if (!isClimbing)
            {
                Flip();
            }

        }
        else if (inputX < 0 && isLookingRight)
        {
            // If he is climbing, dont rotate him
            if (!isClimbing)
            {
                Flip();
            }
        }


    }

    void Flip()
    {

        isLookingRight = !isLookingRight;
        transform.Rotate(0f, 180, 0f);

    }

    private void UpdateAnimations()
    {
        anim.SetFloat("xVelocity", Mathf.Abs(inputX));
        anim.SetBool("isClimbing", isClimbing);
    }

    private void OnDrawGizmos()
    {
        if (agentCheck == null)
            return;

        Gizmos.DrawWireSphere(agentCheck.position, k_AgentRadius);
    }
}
