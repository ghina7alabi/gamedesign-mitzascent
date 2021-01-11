using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Narrate;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //player variables
    Rigidbody2D playerRB;
    public float walkSpeed = 7;
    public float thrust = 220f;
    public static float sticktimer;

    bool onPlatform, mouseClicked, canMove, canDoubleJump;
    public static bool playerSticked;

    int fallingDistancePoints;
    Vector3 stablePosition;
    public TextMeshProUGUI fallingPointsText;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && canMove) //&&canMove 
        {
            float horizontal = Input.GetAxis("Horizontal");
            playerRB.velocity = new Vector2(horizontal, playerRB.velocity.y / walkSpeed) * walkSpeed;
        }
        if (Input.GetKeyDown(KeyCode.W) && onPlatform && canMove) //&& onPlatform && canMove
        {
            playerRB.AddForce(transform.up * thrust, ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.W) && !onPlatform && canDoubleJump)
        {
            playerRB.AddForce(transform.up * thrust, ForceMode2D.Impulse);
            canDoubleJump = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            mouseClicked = true;
            canMove = false;
        }
        if (Input.GetMouseButtonUp(0))
        {
            mouseClicked = false;
        }
        

        //fallingDistancePoints
        if(onPlatform)
        {
            stablePosition = gameObject.transform.position;
        }

        if (!canMove && !onPlatform)
        {
            //he's in the air, but is he falling or not?
            //if (stablePosition.y > gameObject.transform.position.y)
            //{
            //    fallingDistancePoints++;
            //    fallingPointsText.text = "Falling Points: " + fallingDistancePoints;
            //}
        }
    }

    //TODO: Add the range thingie around the player

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Base")
        {
            if (stablePosition.y > gameObject.transform.position.y)
            {
                fallingDistancePoints  += (int)(stablePosition.y-gameObject.transform.position.y);
                fallingPointsText.text = "Falling Points: " + fallingDistancePoints;
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
            if (!NarrationManager.instance.isPlaying && Input.GetKeyDown(KeyCode.X) && fallingDistancePoints >= 10)
            {
                Debug.Log("Take a powerup");
                fallingDistancePoints -= 10;
                fallingPointsText.text = "Falling Points: " + fallingDistancePoints;
                canDoubleJump = true;
            }
        }
    }
}