using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Narrate;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //player variables
    Rigidbody2D playerRB;
    public float walkSpeed = 7;
    public float thrust = 220f;
    public static float sticktimer;

    bool onPlatform, mouseClicked, canMove;
    bool canDoubleJump, canMidair; //powerup booleans
    public static bool canIncreasePlatformSpeed; //powerup booleans

    public static bool playerSticked;

    int fallingDistancePoints, climbingDistancePoints;
    Vector3 stablePosition;
    public TextMeshProUGUI fallingPointsText, climbingPointsText;
    public Image doubleJumpUI, platformSpeedUI, midairStopUI;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        stablePosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))// && canMove) 
        {
            float horizontal = Input.GetAxis("Horizontal");
            playerRB.velocity = new Vector2(horizontal, playerRB.velocity.y / walkSpeed) * walkSpeed;
        }
        if (Input.GetKeyDown(KeyCode.W))// && onPlatform && canMove)
        {
            playerRB.AddForce(transform.up * thrust, ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.W) && !onPlatform && canDoubleJump)
        {
            playerRB.AddForce(transform.up * thrust, ForceMode2D.Impulse);
            canDoubleJump = false;
            doubleJumpUI.color = new Color(1, 1, 1, 0.3f); //disappear
        }

        if (Input.GetMouseButtonDown(0))
        {
            mouseClicked = true;
            canMove = false;
        }
        if (Input.GetMouseButtonUp(0))
        {
            mouseClicked = false;
            if (canIncreasePlatformSpeed)
            {
                platformSpeedUI.color = new Color(1, 1, 1, 0.3f); //disappear
                canIncreasePlatformSpeed = false;
            }

        }

        //DistancePoints
        if(onPlatform)
        {
            stablePosition = gameObject.transform.position;
        }

        if (!onPlatform && !canMove && canMidair)
        {
            midairStopUI.color = new Color(1, 1, 1, 0.3f); //disappear
            canMove = true;
            canMidair = false;
        }
    }

    //TODO: Add the range thingie around the player

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Base")
        {
            if (stablePosition.y > gameObject.transform.position.y + 5)
            {
                fallingDistancePoints  += (int)(stablePosition.y-gameObject.transform.position.y);
                fallingPointsText.text = "Falling Points: " + fallingDistancePoints;
            }

            if (stablePosition.y < gameObject.transform.position.y - 4)
            {
                climbingDistancePoints += (int)(gameObject.transform.position.y - stablePosition.y);
                climbingPointsText.text = "Climbing Points: " + climbingDistancePoints;
            }
        }

        if (collision.gameObject.tag == "Sticky" && Time.time > sticktimer)
        {
            gameObject.GetComponent<Rigidbody2D>().simulated = false;
            gameObject.transform.SetParent(collision.gameObject.transform);
            playerSticked = true;
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
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NPC")
        {
            if (!NarrationManager.instance.isPlaying)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1) && fallingDistancePoints >= 10 && !canDoubleJump)
                {
                    Debug.Log("Take a double jump");
                    fallingDistancePoints -= 10;
                    fallingPointsText.text = "Falling Points: " + fallingDistancePoints;

                    canDoubleJump = true;
                    doubleJumpUI.color = new Color(1, 1, 1, 1); //appear
                }

                if (Input.GetKeyDown(KeyCode.Alpha2) && fallingDistancePoints >= 10 && !canIncreasePlatformSpeed)
                {
                    Debug.Log("Change platform speeds");
                    fallingDistancePoints -= 10;
                    fallingPointsText.text = "Falling Points: " + fallingDistancePoints;

                    canIncreasePlatformSpeed = true;
                    platformSpeedUI.color = new Color(1, 1, 1, 1); //appear
                }

                if (Input.GetKeyDown(KeyCode.Alpha3) && fallingDistancePoints >= 10 && !canMidair)
                {
                    Debug.Log("Midair Stop");
                    fallingDistancePoints -= 10;
                    fallingPointsText.text = "Falling Points: " + fallingDistancePoints;

                    canMidair = true;
                    midairStopUI.color = new Color(1, 1, 1, 1); //appear
                }
            }
        }
    }
}