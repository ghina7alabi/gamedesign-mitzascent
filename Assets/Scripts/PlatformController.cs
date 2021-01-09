using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    //platform details
    Rigidbody2D platformRB;
    Vector3 originalPlatPos, wantedPlatPos, platDifference, currentPlatPos;
    public float platformSpeed = 10;

    //platform range
    GameObject range;
    SpriteRenderer rangeSprite;
    float rangeRadius;

    //mouse details
    Vector3 originalMousePos, currentMousePos, mouseDifference;

    //momentum
    Vector3 turnbackSpeed;
    bool shot;

    // Start is called before the first frame update
    void Start()
    {
        //platform details
        platformRB = gameObject.GetComponent<Rigidbody2D>();
        originalPlatPos = gameObject.transform.position;

        //radius
        range = gameObject.transform.parent.GetChild(0).gameObject;
        rangeSprite = range.GetComponent<SpriteRenderer>(); //sprite
        rangeRadius = range.GetComponent<Renderer>().bounds.size.x; //radius size (it gives 16 bounds=8*2)

        shot = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //get mouse position, enable sprite
        {
            rangeSprite.enabled = true;

            //mouse details
            originalMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            shot = false;
        }

        if (Input.GetMouseButton(0)) //hold and change 
        {
            //mouse details
            currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseDifference = currentMousePos - originalMousePos;

            //platform positions
            wantedPlatPos = originalPlatPos + mouseDifference;
            currentPlatPos = gameObject.transform.position;
            platDifference = currentPlatPos - originalPlatPos;

            if (platDifference.sqrMagnitude < rangeRadius) //if the plat position is smaller than the radius, move it
            {
                platformRB.velocity = (wantedPlatPos - currentPlatPos) * platformSpeed;  //move the platform with this speed
            }
            else //if not make it zero
            {
                platformRB.velocity = Vector3.zero;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            rangeSprite.enabled = false; //disable sprite renderer

            platformRB.velocity = Vector3.zero; //stop velocity
            turnbackSpeed = (originalPlatPos - currentPlatPos) * platformSpeed;
            platformRB.velocity = turnbackSpeed; //move the character with turnback speed
            shot = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Center" && shot == true)
        {
            platformRB.velocity = Vector3.zero;
            gameObject.transform.position = originalPlatPos;
            shot = false;
        }
    }
}
