using UnityEngine;

public class PenguinScript : MonoBehaviour
{

    public Rigidbody2D myRigidbody;
    public SpriteRenderer myRenderer;
    public BoxCollider2D myCollider;
    public Animator myAnimator;
    public float jumpStrength;
    public int jumpCount;
    public ScriptCamera mainCamera;

    [SerializeField] Transform groundCheckCollider;
    [SerializeField] LayerMask groundCheckLayerMask;
    [SerializeField] AudioSource jumpSound;
    

    public float moveSpeed = 5f;
    public bool isGrounded = false;
    public bool facingRight;
    bool firstJump;


    void Start()
    {
        myCollider = GetComponent<BoxCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myRenderer = GetComponent<SpriteRenderer>();
        jumpCount = 0;
        gameObject.name = "Penguin";
        firstJump = false;
    }

    void Update()
    {
        myAnimator.SetFloat("yVelocity", myRigidbody.velocity.y);

        // daca s a apasat space apelez functia "Jump()"
        if (Input.GetButtonDown("Jump"))
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
        // daca este prima saritura incepe camera sa urce
        if (firstJump == false)
        {
            firstJump = true;
            mainCamera.startMoving();
        }
        if (jumpCount != 1)
        {
            jumpSound.Play();
            jumpCount++;
            myRigidbody.velocity = Vector2.up * jumpStrength;
        }
    }

    void Movement()
    {
        // daca se apasa "A"
        myAnimator.SetFloat("xVelocity", Mathf.Abs(myRigidbody.velocity.x));
        if (Input.GetKey(KeyCode.A))
        {
            // pinguinul se muta la stanga
            myRigidbody.velocity = new Vector2(-moveSpeed, myRigidbody.velocity.y);
            transform.localScale = new Vector3((float)(-0.75), (float)0.75, (float)0.75);
            facingRight = false;
        }
        // daca se apasa "D"
        else if (Input.GetKey(KeyCode.D))
        {
            // pinguinul se muta la dreapta
            myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);
            transform.localScale = new Vector3((float)0.75, (float)0.75, (float)0.75);
            facingRight = true;
        }
        else
        {
            // altfel este "idle"
            myRigidbody.velocity = new Vector2(0f, myRigidbody.velocity.y);
        }
    }

    // functie pentru verificare daca pinguinul a aterizat pe ceva
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

