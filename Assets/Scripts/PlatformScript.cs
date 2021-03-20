using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public GameObject radius, reticle; //sprite radius
    Rigidbody2D rb; //rb of the tiny circle
    Vector3 OriginalPlatPos, WantedPlatPos, OriginalMousePos, CurrentMousePos, MouseDifference, PlatDifference, CurrentPlatPos, TurnBackSpeed;
    bool shot;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Center" && shot == true) 
        {
            shot = false;
            rb.velocity = new Vector3(0, 0, 0);
            this.gameObject.transform.position = OriginalPlatPos;
            PlayerController.platformSpeedUp = false;
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
