using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D poopy;
    public float speed = 5.2f;
    public float moveInput;
    public float jumpForce;

    private bool grounded;
    public Transform groundCheck;
    public float radius;
    public LayerMask ground;

    public float jumpTime;
    private float jumpCounter;
    private bool jumping;

    private bool facingRight;

    // Start is called before the first frame update
    void Start()
    {
        poopy = GetComponent<Rigidbody2D>();
        facingRight = true;
        FindObjectOfType<AudioManager>().Play("Chem");
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        poopy.velocity = new Vector2(moveInput * speed, poopy.velocity.y);

        if (facingRight && moveInput < 0)
        {
            Flip();
        } else if (!facingRight && moveInput > 0)
        {
            Flip();
        }
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Finish")
        {
            FindObjectOfType<AudioManager>().Play("Num");
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    void Movement()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, radius, ground);

        if (grounded && Input.GetKeyDown(KeyCode.UpArrow))
        {
            jumping = true;
            jumpCounter = jumpTime;
            poopy.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKey(KeyCode.UpArrow) && jumping)
        {
            if (jumpCounter > 0)
            {
                poopy.velocity = Vector2.up * jumpForce;
                jumpCounter -= Time.deltaTime;
            }
            else
            {
                jumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            jumping = false;
        }
    }

}
