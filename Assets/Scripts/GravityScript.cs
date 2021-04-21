using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityScript : MonoBehaviour
{
    public GameObject Player;
    Rigidbody2D rb;
    bool playerisin;
    Vector3 pullForce;
    float pullPower;

    // Start is called before the first frame update
    void Start()
    {
        rb = Player.GetComponent<Rigidbody2D>();
        pullPower = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerisin)
        {
            pullForce = (gameObject.transform.position - Player.transform.position).normalized;
            rb.AddForce(pullForce * pullPower);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerisin = true;
            rb.gravityScale = 0;
            pullPower = 500;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerisin = false;
            rb.gravityScale = 5;
            pullPower = 0;
        }
    }
}
