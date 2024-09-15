using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private PlayerController controller;
    public Vector2 knockback = Vector2.zero;
    public float speed = 20f;
    public int damage = 1;
    public Rigidbody2D rb;
    void Awake () {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    void Start()
    {
        if (controller._isFacingRight) {
            rb.velocity = transform.right *speed;
        } else {
            rb.velocity = transform.right *-speed;
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Damageable attack = hitInfo.GetComponent<Damageable>();
        if (attack != null)
        {
            attack.Hit(damage, knockback);
        }
        Destroy(gameObject);
    }
    
}
