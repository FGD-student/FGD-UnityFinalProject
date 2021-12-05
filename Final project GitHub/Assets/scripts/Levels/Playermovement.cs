using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playermovement : MonoBehaviour
{
    private float speed = 10f;
    private float jumpForce = 25f;

    private float moveHorizontal;

    private Rigidbody2D rb2D;

    public static bool isGrounded;
    [SerializeField] Transform groundCheck;
    //float groundCheckRadius = 0.05f;
    [SerializeField] LayerMask whatIsGround;

    private bool facingRight = true;
    private Animator playerAnim;

    //jumping
    private float nextActionTime;
    public float jumpPeriod = 5f;
    //jumpinf slower through powerup
    public float PowerUpTimeBlue = 1f;
    public float PowerUpTimeRed = 3f;
    float PowerUpTimeGreen = 0.2f;
    //timer for jumping
    float timeLeft;
    float maxTime;    //should be equal to nextActionTime
    //time left in case of a powerup
    float timeLeftPU;

    //gliding
    float fallingThreshold = -2f;
    float gravityForce = 6;
    float glidingForce = 4;
    //float timeLeftGliding;
    public static float timeLeftGliding;
    float maxTimeGliding = 1f;

    //audio
    AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip FallSound;
    bool atfinish = false;

    //health
    private float hp = 1;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    private static Playermovement instance;

    public static Playermovement MyInstance
    {
        get
        {
            if (instance == null)
                instance = new Playermovement();

            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        
        nextActionTime = jumpPeriod;

        timeLeft = maxTime;
        maxTime = jumpPeriod;
        //PowerUpTime = 0f;

        timeLeftGliding = maxTimeGliding;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Jump();
        Timer();
        Gliding();
        GlidingTimer();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheck.position, new Vector2(0.3f, 0.1f));
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(0.3f, 0.1f), 0, whatIsGround);
        MovePlayer();
        playerAnim.SetBool("IsGrounded", isGrounded);
    }

    private void MovePlayer()
    {
        rb2D.velocity = new Vector2(moveHorizontal * speed, rb2D.velocity.y);
    }

    private void FlipChar()
    {
        facingRight = !facingRight;
        Vector3 flip = transform.localScale;
        flip.x *= -1;
        transform.localScale = flip;
    }

    private void GetInput()
    {
        moveHorizontal = Input.GetAxis("Horizontal");

        if (moveHorizontal == 0)
        {
            playerAnim.SetBool("IsRunning", false);
        }
        else
        {
            playerAnim.SetBool("IsRunning", true);
        }

        if (facingRight && moveHorizontal < 0)
        {
            FlipChar();
        }
        else if (!facingRight && moveHorizontal > 0)
        {
            FlipChar();
        }
    }
    
    //wait x seconds (jumpperiod) and then jump again
    private void Jump()
    {   
        if (Time.timeSinceLevelLoad > nextActionTime) //& atfinish == false)
        {
            nextActionTime += jumpPeriod;
            rb2D.velocity = Vector2.up * jumpForce;
            if (atfinish == false)
            {
                PlaySound(jumpSound);
            }            
        }

        playerAnim.SetFloat("Jumping", rb2D.velocity.y);
    }

    private void Timer()
    {
        //timer UI
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            UIBarTimer.instance.SetValue(timeLeft / maxTime);
        }
        else
        {
            timeLeft = jumpPeriod;
            maxTime = jumpPeriod; 
        }
    }
    
    //gliding when space bar is pressed and for max seconds
    public void Gliding()
    {        
        if (Input.GetKey(KeyCode.Space) & rb2D.velocity.y < fallingThreshold & !isGrounded & timeLeftGliding > 0)
        {
            timeLeftGliding -= (Time.deltaTime);
            rb2D.velocity = new Vector2(rb2D.velocity.x, Mathf.Sign(rb2D.velocity.y) * glidingForce);
            playerAnim.SetBool("IsGliding", true);
        }
        else if (Input.GetKeyUp(KeyCode.Space) || isGrounded || timeLeftGliding < 0)
        {
            rb2D.gravityScale = gravityForce;
            playerAnim.SetBool("IsGliding", false);
        }
        if (isGrounded)
        {
            timeLeftGliding = maxTimeGliding;
        }
    }

    //shows how long able to glide
    private void GlidingTimer()
    {
        if (timeLeftGliding > 0)
        {
            UIBarGliding.instance.SetValue(timeLeftGliding / maxTimeGliding);
        }
    }

    public void PlayerHit(float _damage)
    {
        hp -= _damage;

        if(hp <= 0)
        {
            playerAnim.SetBool("IsAlive", false);   //needed otherwise can go to different states from 'any state' during animation
            playerAnim.SetTrigger("Death");
            rb2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            GameManager.MyInstance.Death();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetection")
        {
            GameManager.MyInstance.Death();
            atfinish = true; //no jump sound
            PlaySound(FallSound);
        }
        
        if (collision.tag == "Finish")
        {
            rb2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            playerAnim.SetBool("IsRunning", false);
            atfinish = true;
        }

        //function for changing time until next jump when touching powerup
        if (collision.tag == "PowerUpBlue")
        {
            nextActionTime -= PowerUpTimeBlue;
            timeLeft -= PowerUpTimeBlue;
            maxTime -= PowerUpTimeBlue;
        }  

        //function to jump immediatly 
        if (collision.tag == "PowerUpGreen")
        {
            nextActionTime = Time.timeSinceLevelLoad + PowerUpTimeGreen;
            timeLeft = PowerUpTimeGreen;
            //maxTime = PowerUpTimeGreen;
        }

        if (collision.tag == "PowerUpRed")
        {
            nextActionTime += PowerUpTimeRed;
            timeLeft += PowerUpTimeRed;
            maxTime += PowerUpTimeRed;
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}


