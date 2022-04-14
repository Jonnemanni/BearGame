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
    // Ground & Door Layer
    [SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask door;
    // Checking if we are on door and if we have went through door recently.
    [SerializeField] private bool ondoor;
    [SerializeField] private bool gonethrudoor;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    // OnTriggerStay2d that currently checks if we are on a door, and then checks if we are trying to enter the door by pressing jump.
    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Door")
        {
            ondoor = true;
        }
        if (other.gameObject.tag == "Door" && Input.GetButton("Jump") && gonethrudoor == false)
        {
            Debug.Log("Walk through attempted.");
            DoorScript ds = other.GetComponent<DoorScript>();
            ds.WalkThrough(this.gameObject);
            gonethrudoor = true;
            StartCoroutine(DoorTimer());
        }
    }

    // A simple half a second coroutine that then sets 'gonethrudoor' to be true.
    IEnumerator DoorTimer()
    {
        yield return new WaitForSeconds(0.5f);
        gonethrudoor = false;
    }

    // On trigger exit where we ensure that the 'on top of door' check is brought back to false.
    private void OnTriggerExit2D(Collider2D other) {
        ondoor = false;
    }

    // Update is called once per frame
    void Update()
    {

        hDirection = Input.GetAxis("Horizontal");
        // Moving Right
        if (hDirection>0 && !DialogueManager.GetInstance().dialoguePlaying) 
        {
            rb.velocity = new Vector2(walkSpeed,rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
        // Moving Left
        else if (hDirection<0 && !DialogueManager.GetInstance().dialoguePlaying) 
        {
            rb.velocity = new Vector2(-walkSpeed,rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }        
        // Stopped
        else
        {
            rb.velocity = new Vector2(0,rb.velocity.y);
        }

        // We jump if we have pressed the jump button, are on ground, are not in front of a door, and are not in dialogue.
        if (Input.GetButtonDown("Jump") && collider.IsTouchingLayers(ground) && ondoor == false && !DialogueManager.GetInstance().dialoguePlaying)
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
