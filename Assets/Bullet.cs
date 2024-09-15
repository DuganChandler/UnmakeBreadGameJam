using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public PlayerController controller;
    public float speed = 20f;
    public int damage = 1;
    public Rigidbody2D rb;
    void Start()
    {
        rb.velocity = transform.right *speed;
            
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Damageable attack = hitInfo.GetComponent<Damageable>();
        if (attack != null)
        {
            attack.Hit(damage);
        }
        Destroy(gameObject);
    }
    
}
