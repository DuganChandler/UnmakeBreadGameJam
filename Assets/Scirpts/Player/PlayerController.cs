using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D rigidbody2D;
    private CircleCollider2D circleCollider2D;
    [SerializeField] private LayerMask groundLayer;
    [Range(0, 10f)] [SerializeField] private float speed = 0f;

    float horizontal = 0f;
    private bool isFacingRight = true;
    bool jump = false;

    void Start() {
       rigidbody2D = GetComponent<Rigidbody2D>(); 
       circleCollider2D = GetComponent<CircleCollider2D>(); 
    }

    void Update() {
        horizontal = Input.GetAxisRaw("Horizontal") * speed;

        if (isOnGround() && horizontal.Equals(0)) {
            // idle
        } else if (isOnGround() && (horizontal > 0 || horizontal < 0)) {
            // moving
        }

        if (isOnGround() && Input.GetButtonDown("Jump")){
            jump = true;
        } 
    }

    void FixedUpdate() {
       float moveFactor = horizontal * Time.fixedDeltaTime; 

       // Movement
       rigidbody2D.velocity = new Vector2(moveFactor * 10f, rigidbody2D.velocity.y);

       if (jump) {
            float jumpvel = 2f;
            rigidbody2D.velocity = Vector2.up * jumpvel;
            jump = false;
       }
    }

    private bool isOnGround() {
       RaycastHit2D hit = Physics2D.CircleCast(circleCollider2D.bounds.center, circleCollider2D.radius, Vector2.down, 0.1f, groundLayer);
       return hit.collider != null;
    }
}