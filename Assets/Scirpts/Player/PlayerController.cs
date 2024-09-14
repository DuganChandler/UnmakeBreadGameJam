using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {
    [SerializeField] LayerMask groundLayer;
    public float walkSpeed = 5f;
    public float jumpImpulse = 4f;
    Vector2 moveInput;
    public bool IsMoving { get; private set; }
    Rigidbody2D rigidbody2D;
    CircleCollider2D circleCollider2D;

    void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();    
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    void Start() {

    }

    void Update() {
    }

    void FixedUpdate() {
        rigidbody2D.velocity = new Vector2 (moveInput.x * walkSpeed, rigidbody2D.velocity.y);
    }

    private bool isOnGround() {
       RaycastHit2D hit = Physics2D.CircleCast(circleCollider2D.bounds.center, circleCollider2D.radius, Vector2.down, 0.1f, groundLayer);
       return hit.collider != null;
    }
    
    public void onMove(InputAction.CallbackContext context) {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
    }

    public void onJump(InputAction.CallbackContext context) {
        if (context.started && isOnGround()) {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpImpulse);
        }
    }
}