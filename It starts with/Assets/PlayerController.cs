using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D poopy;
    public float speed = 5.2f;
    public float moveInput;
    public float jumpForce;

    // Start is called before the first frame update
    void Start()
    {
        poopy = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            poopy.velocity = Vector2.up * speed;
        }
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        poopy.velocity = new Vector2(moveInput * speed, poopy.velocity.y);
    }

}
