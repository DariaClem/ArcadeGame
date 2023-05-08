using UnityEngine;

public class PenguinScript : MonoBehaviour
{

    public Rigidbody2D myRigidbody;
    public SpriteRenderer myRenderer;
    public BoxCollider2D myCollider;
    public Animator myAnimator;
    public float jumpStrength;
    public int jumpCount;

    [SerializeField] Transform groundCheckCollider;
    [SerializeField] LayerMask groundCheckLayerMask;
    [SerializeField] AudioSource jumpSound;
    

    public float moveSpeed = 5f;
    public bool isGrounded = false;
    public bool facingRight;

    private void Awake()
    {

    }

    void Start()
    {
        myCollider = GetComponent<BoxCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myRenderer = GetComponent<SpriteRenderer>();
        jumpCount = 0;
        gameObject.name = "Penguin";
    }

    void Update()
    {
        myAnimator.SetFloat("yVelocity", myRigidbody.velocity.y);

        if (Input.GetButtonDown("Jump") || Input.GetKey(KeyCode.W))
        {
            myAnimator.SetBool("Jump", true);
            Jump();   
        }
    }

    void FixedUpdate()
    {
        GroundCheck();
        Movement();
    }


    void Jump()
    {
        if (jumpCount != 1)
        {
            jumpSound.Play();
            jumpCount++;
            myRigidbody.velocity = Vector2.up * jumpStrength;
        }
    }

    void Movement()
    {
        myAnimator.SetFloat("xVelocity", Mathf.Abs(myRigidbody.velocity.x));
        if (Input.GetKey(KeyCode.A))
        {
            myRigidbody.velocity = new Vector2(-moveSpeed, myRigidbody.velocity.y);
            transform.localScale = new Vector3((float)(-0.75), (float)0.75, (float)0.75);
            facingRight = false;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);
            transform.localScale = new Vector3((float)0.75, (float)0.75, (float)0.75);
            facingRight = true;
        }
        else
        {
            myRigidbody.velocity = new Vector2(0f, myRigidbody.velocity.y);
        }
    }

    void GroundCheck()
    {
        isGrounded = false;
        if (Physics2D.BoxCast(myCollider.bounds.center, myCollider.bounds.size, 0f, Vector2.down, .1f, groundCheckLayerMask))
        {
            isGrounded = true;
            jumpCount = 0;
            
        }
        myAnimator.SetBool("Jump", !isGrounded);
    }
}

