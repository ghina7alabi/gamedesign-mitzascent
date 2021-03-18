﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleScript : MonoBehaviour
{
    Vector3 OriginalPlatPos, WantedPlatPos, OriginalMousePos, CurrentMousePos, MouseDifference, PlatDifference, CurrentPlatPos, TurnBackSpeed;
    Rigidbody2D rb;
    bool shot;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        OriginalPlatPos = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //take mouse position then enable the sprite renderer
        {
            shot = false;
            OriginalMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0)) // 
        {
            CurrentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MouseDifference = CurrentMousePos - OriginalMousePos;
            WantedPlatPos = OriginalPlatPos + MouseDifference;
            CurrentPlatPos = this.gameObject.transform.position;
            PlatDifference = CurrentPlatPos - OriginalPlatPos;

            rb.velocity = (WantedPlatPos - CurrentPlatPos) * 10;

            if (rb.velocity.x > 20)
            {
                rb.velocity = new Vector3(20, rb.velocity.y, 0); 
            }
            if (rb.velocity.y > 20)
            {
                rb.velocity = new Vector3(rb.velocity.x, 20, 0);
            }
            if (rb.velocity.x > 20)
            {
                rb.velocity = new Vector3(20, rb.velocity.y, 0);
            }

            if (rb.velocity.x < -20)
            {
                rb.velocity = new Vector3(-20, rb.velocity.y, 0);
            }
            if (rb.velocity.y < -20)
            {
                rb.velocity = new Vector3(rb.velocity.x, -20, 0);
            }
            if (rb.velocity.x < -20)
            {
                rb.velocity = new Vector3(-20, rb.velocity.y, 0);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            rb.velocity = new Vector3(0, 0, 0); //stop velocity
            gameObject.transform.position = OriginalPlatPos;
        }
    }
}
