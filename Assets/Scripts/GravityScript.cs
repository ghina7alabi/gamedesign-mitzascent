using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityScript : MonoBehaviour
{
    public GameObject Player;
    Rigidbody2D rb;
    bool playerisin;
    Vector3 pullForce;

    // Start is called before the first frame update
    void Start()
    {
        rb = Player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerisin)
        {
            pullForce = (gameObject.transform.position - Player.transform.position).normalized;
            rb.AddForce(pullForce * 50);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerisin = true;
            rb.gravityScale = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerisin = false;
            rb.gravityScale = 5;
        }
    }
}
