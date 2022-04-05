using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public int walkSpeed;
    // Jump Height variable for easy editing.
    public int jumpHeight;
    // Getting Rigidbody2D under the name RB.
    [SerializeField] private Rigidbody2D rb;
    // Getting Collider2d under the name Collider.
    [SerializeField] private Collider2D collider;
    // Getting the Animator under the name Anim.
    [SerializeField] private Animator anim;
    // Walk Speed variable for easy editing.
    // Enumerating states.
    private enum State {idle, run, jump};
    [SerializeField] private State state = State.idle;
    // Direction
    [SerializeField] private float hDirection;
    [SerializeField] private float vDirection;
    // Ground Layer
    [SerializeField] private LayerMask ground;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   
        hDirection = Input.GetAxis("Horizontal");
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
            rb.velocity = new Vector2(0,rb.velocity.y);
        }

        // Jump
        if (Input.GetButtonDown("Jump") && collider.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x,jumpHeight);
        }

        velocityState();
        anim.SetInteger("State", (int)state);
    }

    private void velocityState()
    {
        if(vDirection!=0)
        {
            state = State.jump;
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
