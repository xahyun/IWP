using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float MovementSpeed;
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    public AnimationClip[] idle;
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
        if (moveY>0)
        {
            Debug.Log("up");
            anim.SetFloat("dir", 2);
            anim.SetInteger("MoveDir", 2);
        }
        else if (moveY < 0)
        {
            Debug.Log("down");
            anim.SetFloat("dir", 0);
            anim.SetInteger("MoveDir", 0);
        }
        if (moveX > 0)
        {
            Debug.Log("right");
            anim.SetFloat("dir", 3);

            anim.SetInteger("MoveDir", 3);
        }
        else if (moveX < 0)
        {
            Debug.Log("left");
            anim.SetFloat("dir", 1);

            anim.SetInteger("MoveDir", 1);
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * MovementSpeed, moveDirection.y * MovementSpeed);
        if(rb.velocity!=Vector2.zero)
        {
            hungerBar.hb.SpeedUP(false);
            anim.SetBool("moving", true);
        }
        else
        {
            hungerBar.hb.SpeedUP(true);
            anim.SetBool("moving", false);
        }
        
    }

    public void ForceBack(Vector3 dir)
    {
        rb.AddForce(-dir);
    }

}
