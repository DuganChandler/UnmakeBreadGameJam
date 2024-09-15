using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {
    [SerializeField] LayerMask groundLayer;
    public float walkSpeed = 5f;
    public int itemsCollected = 0;
    //Dash variables
    private bool canDash = true;
    private bool isDashing;
    public float dashingPower = 24f;
    public float dashingTime = 0.2f;
    public float dashingCoolDown = 1f;
    [SerializeField] private TrailRenderer tr;

    //Dash 
    private IEnumerator Dash()
    {
        canDash = true;
        isDashing = true;
        float originalGravity = rigidbody2D.gravityScale;
        rigidbody2D.gravityScale = 0.2f;
        rigidbody2D.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rigidbody2D.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCoolDown);
        canDash = true;
    }
    
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
    public bool IsAlive {
        get {
            return animator.GetBool("isAlive");
        }
    }
    public bool LockVelolcity {
        get{
            return animator.GetBool("lockVelocity");
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
        if (isDashing)
        {
            return;
        }
        
        IsFalling = !isOnGround();
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }


    void FixedUpdate() {
        if (!LockVelolcity && !isDashing)
            rigidbody2D.velocity = new Vector2 (moveInput.x * CurrentSpeed, rigidbody2D.velocity.y);
    }

    private bool isOnGround() {
       RaycastHit2D hit = Physics2D.CircleCast(circleCollider2D.bounds.center, circleCollider2D.radius, Vector2.down, 0.1f, groundLayer);
       return hit.collider != null;
    }
    
    public void onMove(InputAction.CallbackContext context) {
        moveInput = context.ReadValue<Vector2>();
        if (IsAlive) {
            IsMoving = moveInput != Vector2.zero;
            SetFacingDirection(moveInput);
        } else {
            IsMoving = false;
        }
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

    public void onHit(int damage, Vector2 knockback) {
        rigidbody2D.velocity = new Vector2(knockback.x, rigidbody2D.velocity.y + knockback.y);
    }
}