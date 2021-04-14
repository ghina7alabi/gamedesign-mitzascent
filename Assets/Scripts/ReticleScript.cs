using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleScript : MonoBehaviour
{
    public Camera MouseCamera;
    Vector3 OriginalPlatPos, WantedPlatPos, OriginalMousePos, CurrentMousePos, MouseDifference, PlatDifference, CurrentPlatPos, TurnBackSpeed;
    Rigidbody2D rb;

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
            OriginalMousePos = MouseCamera.ScreenToWorldPoint(Input.mousePosition); //Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0)) // 
        {
            CurrentMousePos = MouseCamera.ScreenToWorldPoint(Input.mousePosition); //Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MouseDifference = CurrentMousePos - OriginalMousePos;
            WantedPlatPos = OriginalPlatPos + MouseDifference;
            CurrentPlatPos = this.gameObject.transform.position;
            PlatDifference = CurrentPlatPos - OriginalPlatPos;

            rb.velocity = (WantedPlatPos - CurrentPlatPos) * 10;
            //rb.velocity = new Vector3(((WantedPlatPos.x - CurrentPlatPos.x)*(1920/1080)), ((WantedPlatPos.y - CurrentPlatPos.y)*(1080/1920)), 0);

            if (rb.velocity.x > 20 || rb.velocity.x < -20 || rb.velocity.y > 20 || rb.velocity.y < -20)
            {
                rb.velocity = new Vector3(rb.velocity.x / 2, rb.velocity.y / 2, 0);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            rb.velocity = new Vector3(0, 0, 0); //stop velocity
            gameObject.transform.position = OriginalPlatPos;
        }
    }
}
