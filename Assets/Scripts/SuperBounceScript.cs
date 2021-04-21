﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperBounceScript : MonoBehaviour
{
    public GameObject player;
    public Transform teleportCircle;

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
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            player.transform.position = teleportCircle.position;
        }
    }
}
