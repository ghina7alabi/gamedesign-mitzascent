using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyScript : MonoBehaviour
{
    public GameObject radius, player; //sprite radius
    Rigidbody2D rb; //rb of the tiny circle
    Vector3 OriginalPlatPos, WantedPlatPos, OriginalMousePos, CurrentMousePos, MouseDifference, PlatDifference, CurrentPlatPos, TurnBackSpeed;
    bool shot;

    float WantedLimitPointX, WantedLimitPointY;
    float thex, they;

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
            OriginalMousePos = Camera.main.WorldToViewportPoint(Input.mousePosition); //Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0)) // 
        {
            CurrentMousePos = Camera.main.WorldToViewportPoint(Input.mousePosition); //Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MouseDifference = CurrentMousePos - OriginalMousePos;
            WantedPlatPos = OriginalPlatPos + MouseDifference;
            CurrentPlatPos = this.gameObject.transform.position;
            PlatDifference = CurrentPlatPos - OriginalPlatPos;

            if ((PlatDifference.x * PlatDifference.x) + (PlatDifference.y * PlatDifference.y) < 16) //i think its like the radius size, so if the plat position is smaller than the radius, move it
            {
                rb.velocity = new Vector3(WantedPlatPos.x - CurrentPlatPos.x, WantedPlatPos.y - CurrentPlatPos.y, 0) * 10; //10 is the speed?
            }
            else //if not make it zero
            {
                rb.velocity = new Vector3(0, 0, 0);

                thex = (Mathf.Abs(MouseDifference.x)) / ((Mathf.Abs(MouseDifference.x)) + (Mathf.Abs(MouseDifference.y)));
                they = (Mathf.Abs(MouseDifference.y)) / ((Mathf.Abs(MouseDifference.x)) + (Mathf.Abs(MouseDifference.y)));

                WantedLimitPointX = ((Mathf.Sqrt(-thex * thex + 2f * thex)) - they / 4) * 4f;
                WantedLimitPointY = ((Mathf.Sqrt(-they * they + 2f * they)) - thex / 4) * 4f;

                //if (MouseDifference.x >= 0 && MouseDifference.y >= 0)
                //{
                //    gameObject.transform.position = new Vector3(OriginalPlatPos.x + WantedLimitPointX, OriginalPlatPos.y + WantedLimitPointY, 0);
                //}
                //if (MouseDifference.x >= 0 && MouseDifference.y <= 0)
                //{
                //    gameObject.transform.position = new Vector3(OriginalPlatPos.x + WantedLimitPointX, OriginalPlatPos.y - WantedLimitPointY, 0);
                //}
                //if (MouseDifference.x <= 0 && MouseDifference.y >= 0)
                //{
                //    gameObject.transform.position = new Vector3(OriginalPlatPos.x - WantedLimitPointX, OriginalPlatPos.y + WantedLimitPointY, 0);
                //}
                //if (MouseDifference.x <= 0 && MouseDifference.y <= 0)
                //{
                //    gameObject.transform.position = new Vector3(OriginalPlatPos.x - WantedLimitPointX, OriginalPlatPos.y - WantedLimitPointY, 0);
                //}

                //to use velocity instead of transform.position
                if (MouseDifference.x >= 0 && MouseDifference.y >= 0)
                {
                    WantedPlatPos = new Vector3(OriginalPlatPos.x + WantedLimitPointX, OriginalPlatPos.y + WantedLimitPointY, 0);
                }
                if (MouseDifference.x >= 0 && MouseDifference.y <= 0)
                {
                    WantedPlatPos = new Vector3(OriginalPlatPos.x + WantedLimitPointX, OriginalPlatPos.y - WantedLimitPointY, 0);
                }
                if (MouseDifference.x <= 0 && MouseDifference.y >= 0)
                {
                    WantedPlatPos = new Vector3(OriginalPlatPos.x - WantedLimitPointX, OriginalPlatPos.y + WantedLimitPointY, 0);
                }
                if (MouseDifference.x <= 0 && MouseDifference.y <= 0)
                {
                    WantedPlatPos = new Vector3(OriginalPlatPos.x - WantedLimitPointX, OriginalPlatPos.y - WantedLimitPointY, 0);
                }

                rb.velocity = new Vector3(WantedPlatPos.x - CurrentPlatPos.x, WantedPlatPos.y - CurrentPlatPos.y, 0) * 10; //10 is the speed?
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            shot = true;
            radius.GetComponent<SpriteRenderer>().enabled = false; //enable sprite renderer
            rb.velocity = new Vector3(0, 0, 0); //stop velocity
            TurnBackSpeed = new Vector3(OriginalPlatPos.x - CurrentPlatPos.x, OriginalPlatPos.y - CurrentPlatPos.y) * 10;
            rb.velocity = TurnBackSpeed; //take it
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Center" && shot == true)
        {
            player.GetComponent<Rigidbody2D>().simulated = true;
            player.transform.parent = null;

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