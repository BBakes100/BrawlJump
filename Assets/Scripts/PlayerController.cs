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


    public float speed;
    public float jumpSpeed;
    public Vector2 boxSize;
    public float bottomCastDistance;
    public LayerMask groundLayer;
    public Vector2 sideBoxSize;
    public float sideCastDistance;
    private float forceOffWall = 500f;

    private bool canJump;
    private bool onWall;
    
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
        onWall = isOnWall() ? true : false;

        // Player On Ground and Can Jump
        if(canJump && jump != 0)
        {
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        } 

        // Player Moves
        rb.velocity = new Vector2(move.x * speed, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context){
        move = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context){
        jump = context.ReadValue<float>();
    }

    public bool isOnWall(){

        if(Physics2D.BoxCast(transform.position, sideBoxSize, 0, transform.right, sideCastDistance, groundLayer))
        {
            // Wall sliding mechanic
            rb.AddForce(new Vector2(-forceOffWall, 0));
            // rb.velocity = new Vector2(rb.velocity.x, -30);
            // rb.gravityScale = 5;
            return true;
        } 

         else if(Physics2D.BoxCast(transform.position, sideBoxSize, 0, -transform.right, sideCastDistance, groundLayer))
        {
            rb.AddForce(new Vector2(forceOffWall, 0));
            return true;
        } 
        else 
        {
            return false;
        }
    }

    public bool isGrounded(){
        if(Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, bottomCastDistance, groundLayer))
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
        Gizmos.DrawWireCube(transform.position - transform.up * bottomCastDistance, boxSize);
        Gizmos.DrawWireCube(transform.position + transform.right * sideCastDistance, sideBoxSize);
        Gizmos.DrawWireCube(transform.position - transform.right * sideCastDistance, sideBoxSize);
    }

}
