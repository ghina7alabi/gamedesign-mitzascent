using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //player variables
    Rigidbody2D playerRB;
    public float walkSpeed = 7;
    public float thrust = 220f;

    bool onPlatform, mouseClicked, canMove;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))) //&&canMove 
        {
            float horizontal = Input.GetAxis("Horizontal");
            playerRB.velocity = new Vector2(horizontal, playerRB.velocity.y / walkSpeed) * walkSpeed;
        }
        if (Input.GetKeyDown(KeyCode.W)) //&& onPlatform && canMove
        {
            playerRB.AddForce(transform.up * thrust, ForceMode2D.Impulse);
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
    }

    //TODO: Add the range thingie around the player

    private void OnCollisionEnter2D(Collision2D collision)
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

}