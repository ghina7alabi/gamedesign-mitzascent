using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Narrate;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    //player variables
    Rigidbody2D playerRB;
    public float walkSpeed = 7f;
    public float thrust = 220f;
    public static float sticktimer;
    public Animator animator;
    float horizontalMove = 0f;
    public GameObject[] powerEffect;
    

    bool onPlatform, mouseClicked, canMove;

    static bool canRewind, canStop, canSpeedUp, canDoubleJump;


    private bool m_FacingRight = true;

    public static bool playerSticked, platformSpeedUp;

    public static int fallingDistancePoints = 0, climbingDistancePoints = 0;
    Vector3 stablePosition;
    public TextMeshProUGUI fallingPointsText, climbingPointsText;

    //powerups
    public static bool[] powerUpTaken = new bool[4];
    public static bool[] powerUpActivated = new bool[3];
    Vector3 rewindPosition;
    public GameObject[] powerupUI;
    GameObject npcInRange;
    bool nearNPC;


    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

        
    // Start is called before the first frame update
    void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
        stablePosition = gameObject.transform.position;

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        nearNPC = false;
        canRewind = false;
        canStop = false;
        canDoubleJump = false;
        canSpeedUp = false;
        platformSpeedUp = false;

        //Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * walkSpeed;
        animator.SetFloat("speed", Mathf.Abs(horizontalMove));

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))) // && canMove
        {
            float horizontal = Input.GetAxis("Horizontal");
            playerRB.velocity = new Vector2(horizontal, playerRB.velocity.y / walkSpeed) * walkSpeed;
        }

        if (Input.GetKeyDown(KeyCode.W) && onPlatform) // && canMove
        {
            playerRB.AddForce(transform.up * thrust, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
            animator.SetBool("inAir", true);
        }
        if (Input.GetKeyDown(KeyCode.W) && !onPlatform && canDoubleJump) //&& !canMove 
        {
            playerRB.AddForce(transform.up * thrust, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
            animator.SetBool("inAir", true);
            powerupUI[3].GetComponent<RectTransform>().sizeDelta = new Vector2(55, 55);
            canDoubleJump = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            mouseClicked = true;
            canMove = false;
            animator.SetBool("isPower", true);
            StartCoroutine(WaitForEffect(0.6f));
            Debug.Log("down");
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseClicked = false;
            animator.SetBool("isPower", false);
            powerEffect[0].SetActive(false);
            powerEffect[1].SetActive(false);
        }
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("mitzPowerStand"))
            //|| !animator.GetCurrentAnimatorStateInfo(0).IsName("powerToWalk")
            //|| !animator.GetCurrentAnimatorStateInfo(0).IsName("PowerJump") )
        {
            powerEffect[0].SetActive(false);
            powerEffect[1].SetActive(false);
        }

        //DistancePoints
        if (onPlatform)
        {
            stablePosition = gameObject.transform.position;
        }
        if (!onPlatform && playerRB.velocity.y != 0 && !Input.GetMouseButton(0))
        {
            //animator.SetBool("isJumping", true);
            animator.SetBool("inAir", true);
        }

        //powerups
        if (Input.GetKeyDown(KeyCode.Alpha1) && canRewind) //rewind
        {
            transform.position = rewindPosition;
            powerupUI[0].GetComponent<RectTransform>().sizeDelta = new Vector2(55, 55);
            canRewind = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && canStop) //stop midair
        {
            playerRB.velocity = Vector3.zero;
            powerupUI[1].GetComponent<RectTransform>().sizeDelta = new Vector2(55, 55);
            canStop = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && canSpeedUp)
        {
            platformSpeedUp = true;
            powerupUI[2].GetComponent<RectTransform>().sizeDelta = new Vector2(55, 55);
            canSpeedUp = false;
        }



        if (nearNPC && !NarrationManager.instance.isPlaying && fallingDistancePoints >= 5) // && have required amount of points
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && !canRewind)
            {
                canRewind = true;
                PowerupUIController(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && !canStop)
            {
                canStop = true;
                PowerupUIController(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && !canSpeedUp)
            {
                canSpeedUp = true;
                PowerupUIController(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) && !canDoubleJump) //double jump
            {
                canDoubleJump = true;
                PowerupUIController(3);
            }
        }
        //else if (nearNPC && !NarrationManager.instance.isPlaying && fallingDistancePoints <= 5)
        //{
        //    NarrationManager.instance.PlayNarration(npcInRange.GetComponent<NPCController>().notEnoughSpeech);
        //}

            //animation
            // If the input is moving the player right and the player is facing left...
            if (horizontalMove > 0 && !m_FacingRight)
        {
            // ... flip the player.
          Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (horizontalMove < 0 && m_FacingRight)
        {
            // ... flip the player.
           Flip();
        }


    }

    //TODO: Add the range thingie around the player

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Base")
        {
            onPlatform = true; 

            if (stablePosition.y > gameObject.transform.position.y + 5)
            {
                fallingDistancePoints  += (int)(stablePosition.y-gameObject.transform.position.y);
                fallingPointsText.text = "" + fallingDistancePoints;
            }

            if (stablePosition.y < gameObject.transform.position.y - 4)
            {
                climbingDistancePoints += (int)(gameObject.transform.position.y - stablePosition.y);
                climbingPointsText.text = "" + climbingDistancePoints;
            }

            OnLanding();
        }

        if (collision.gameObject.tag == "Sticky" && Time.time > sticktimer)
        {
            gameObject.GetComponent<Rigidbody2D>().simulated = false;
            gameObject.transform.SetParent(collision.gameObject.transform);
            playerSticked = true;

            OnLanding();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Base")
        {
            onPlatform = true;
            if (!mouseClicked)
            {
                canMove = true;
                Camera.main.transform.position = new Vector3(0, gameObject.transform.position.y, -10);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Base")
        {
            onPlatform = false;
            canMove = false;

            if (canRewind) //rewind
            {
                rewindPosition = transform.position;
            }    
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "NPC")
        {
            nearNPC = true;
            npcInRange = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NPC")
        {
            nearNPC = false;
        }
    }

    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
        animator.SetBool("inAir", false);
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    IEnumerator WaitForEffect(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("mitzPowerStand")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("powerToWalk")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("PowerJump") )
        {
            powerEffect[0].SetActive(true);
            powerEffect[1].SetActive(true);
        }
    }

    void PowerupUIController(int index)
    {
        powerupUI[index].GetComponent<RectTransform>().sizeDelta = new Vector2(65, 65);
        NarrationManager.instance.PlayNarration(npcInRange.GetComponent<NPCController>().powerupSpeech);
        fallingDistancePoints -= 5;
        fallingPointsText.text = "" + fallingDistancePoints;
    }

}