using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyScript : MonoBehaviour
{
    public GameObject radius, player, reticle; //sprite radius
    Rigidbody2D rb; //rb of the tiny circle
    Vector3 OriginalPlatPos, WantedPlatPos, OriginalMousePos, CurrentMousePos, MouseDifference, PlatDifference, CurrentPlatPos, TurnBackSpeed;
    bool shot, charSticked;

    public float originalSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        OriginalPlatPos = this.gameObject.transform.position;
        shot = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //take mouse position then enable the sprite renderer
        {
            shot = false;
            radius.GetComponent<SpriteRenderer>().enabled = true;
            OriginalMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0)) // 
        {
            if (Input.GetMouseButtonDown(0)) //enable the sprite renderer, set shot false
            {
                shot = false;
                radius.GetComponent<SpriteRenderer>().enabled = true;
            }

            if (Input.GetMouseButton(0)) //follow reticle
            {
                CurrentPlatPos = this.gameObject.transform.position;
                WantedPlatPos = OriginalPlatPos + reticle.transform.position;

                rb.velocity = (WantedPlatPos - CurrentPlatPos) * 20;
            }
            if (Input.GetMouseButtonUp(0)) //get shot
            {
                shot = true;
                radius.GetComponent<SpriteRenderer>().enabled = false; //enable sprite renderer
                rb.velocity = new Vector3(0, 0, 0); //stop velocity
                TurnBackSpeed = new Vector3(OriginalPlatPos.x - CurrentPlatPos.x, OriginalPlatPos.y - CurrentPlatPos.y) * originalSpeed;

                if (PlayerController.platformSpeedUp)
                {
                    rb.velocity = TurnBackSpeed * 1.3f;
                }
                else
                {
                    rb.velocity = TurnBackSpeed;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            shot = true;
            radius.GetComponent<SpriteRenderer>().enabled = false; //enable sprite renderer
            rb.velocity = new Vector3(0, 0, 0); //stop velocity
            TurnBackSpeed = new Vector3(OriginalPlatPos.x - CurrentPlatPos.x, OriginalPlatPos.y - CurrentPlatPos.y) * 10;
            rb.velocity = TurnBackSpeed; //take it

            PlayerController.sticktimer = Time.time + 0.2f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Center" && shot == true)
        {
            player.GetComponent<Rigidbody2D>().simulated = true;
            player.transform.parent = null;

            if (PlayerController.playerSticked == true)
            {
                player.GetComponent<Rigidbody2D>().velocity = rb.velocity;
            }

            PlayerController.playerSticked = false;
            shot = false;
            rb.velocity = new Vector3(0, 0, 0);
            this.gameObject.transform.position = OriginalPlatPos;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Center" && shot == true)
        {
            shot = false;
            rb.velocity = new Vector3(0, 0, 0);
            this.gameObject.transform.position = OriginalPlatPos;
        }
    }
}