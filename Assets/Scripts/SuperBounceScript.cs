using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperBounceScript : MonoBehaviour
{
    public Rigidbody2D playerRb;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerRb.AddForce(new Vector2(0, -800), ForceMode2D.Impulse);
        }
    }
}
