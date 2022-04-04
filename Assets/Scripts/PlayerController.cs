using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Getting Rigidbody2D under the name RB.
    private Rigidbody2D rb;
    // Getting the Animator under the name Anim.
    private Animator anim;
    // Walk Speed variable for easy editing.
    public int walkSpeed;
    // Jump Height variable for easy editing.
    public int jumpHeight;
    // Enumerating states.
    private enum State {idle, run, jump};
    private State state = State.idle;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   
        float hDirection = Input.GetAxis("Horizontal");
        float vDirection = Input.GetAxis("Vertical");
        // Moving Right
        if (hDirection>0) 
        {
            rb.velocity = new Vector2(walkSpeed,rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
        // Moving Left
        else if (hDirection<0) 
        {
            rb.velocity = new Vector2(-walkSpeed,rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
        // Stopped
        else
        {

        }

        // Jump
        if (vDirection>0)
        {
            rb.velocity = new Vector2(rb.velocity.x,jumpHeight);
            state = State.jump;
        }

        velocityState();
        anim.SetInteger("State", (int)state);
    }

    private void velocityState()
    {
        if(state == State.jump)
        {

        }
        else if(Mathf.Abs(rb.velocity.x) > 2f)
        {
            state = State.run;
        }
        else
        {
            state = State.idle;
        }
    }
}
