using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {
    [SerializeField] LayerMask groundLayer;
    public float walkSpeed = 5f;

    public float CurrentSpeed { get {
        if (CanMove) {
            return walkSpeed;
        } else {
            return 0;
        }
    }}
    public float jumpImpulse = 4f;
    Vector2 moveInput;
    [SerializeField]
    public bool _isFacingRight = true;
    public bool IsFacingRight { get {return _isFacingRight; } private set {
        if (_isFacingRight != value) {
            transform.localScale *= new Vector2(-1, 1);
        }
        _isFacingRight = value;
    }}
    private bool _isMoving = false;
    public bool IsMoving { 
        get {
            return _isMoving; 
        } private set {
            _isMoving = value;
            animator.SetBool("isMoving", value);
        } 
    }

    private bool _isFalling = false;
    public bool IsFalling {
        get {
            return _isFalling;
        } private set {
            _isFalling= value;
            animator.SetBool("isFalling", value);
        }
    }

    public bool CanMove { get {
            return animator.GetBool("canMove");
        } 
    }
    Rigidbody2D rigidbody2D;
    Animator animator;
    CircleCollider2D circleCollider2D;

    void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();    
        circleCollider2D = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
    }

    void Start() {

    }

    void Update() {
        IsFalling = !isOnGround();
    }

    void FixedUpdate() {
        rigidbody2D.velocity = new Vector2 (moveInput.x * CurrentSpeed, rigidbody2D.velocity.y);
    }

    private bool isOnGround() {
       RaycastHit2D hit = Physics2D.CircleCast(circleCollider2D.bounds.center, circleCollider2D.radius, Vector2.down, 0.1f, groundLayer);
       return hit.collider != null;
    }
    
    public void onMove(InputAction.CallbackContext context) {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;

        SetFacingDirection(moveInput);
    }

    private void SetFacingDirection(Vector2 moveInput) {
        if (moveInput.x > 0 && !IsFacingRight) {
            IsFacingRight = true;
        } else if (moveInput.x < 0 && IsFacingRight) {
           IsFacingRight = false; 
        }
    }

    public void onJump(InputAction.CallbackContext context) {
        if (context.started && isOnGround() && CanMove) {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpImpulse);
        }
    }

    public void onAttack(InputAction.CallbackContext context) {
        if (context.started) {
            animator.SetTrigger("attack");
        }
    }
}