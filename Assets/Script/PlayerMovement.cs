using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MovementSpeed;
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        if(moveY>0)
        {
            Debug.Log("up");
            anim.SetInteger("dir", 2);
        }
        else if (moveY < 0)
        {
            Debug.Log("down");
            anim.SetInteger("dir", 0);
        }
        if (moveX > 0)
        {
            Debug.Log("right");
            anim.SetInteger("dir", 3);
        }
        else if (moveX < 0)
        {
            Debug.Log("left");
            anim.SetInteger("dir", 1);
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * MovementSpeed, moveDirection.y * MovementSpeed);
        if(rb.velocity!=Vector2.zero)
        {
            anim.SetBool("moving", true);
        }
        else
        {

            anim.SetBool("moving", false);
        }
        
    }
}
