using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    // Controller context
    private Vector2 move;
    private float jump;


    private float speed = 10;
    private float jumpSpeed = 20;
    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;

    private bool canJump;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movePlayer();
    }

    private void movePlayer(){

        canJump = isGrounded() ? true : false;

        // Player Jumps
        if(canJump && jump != 0)
        {
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        } 
        // Player is Grounded
        else 
        {
            rb.velocity = new Vector2(move.x * speed, rb.velocity.y);
            
            // TODO KEEP WORKING ON WALL SLIDING
            if(move.x == 1 && rb.velocity.x == 1){
                Debug.Log("on RIght wall");
                // rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context){
        move = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context){
        jump = context.ReadValue<float>();
    }

    public bool isGrounded(){
        if(Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            return true;
        } 
        else 
        {
            return false;
        }
    }

    /**
        Helps to Check the ground condition for player
    */
    private void OnDrawGizmos(){
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }

}
