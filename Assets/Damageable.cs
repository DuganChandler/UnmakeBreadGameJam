using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;  
    Animator animator; 
   [SerializeField]
   private int _maxHealth = 3;
   public int MaxHealth
   {
    get
    {
        return _maxHealth;
    }
    set
    {
        _maxHealth = value;
    }
   
   }
   [SerializeField]
   private int _health = 3; 
   public int Health
   {
    get
    {
        return _health;
    }
    set
    {
        _health = value;

        if(_health <= 0)
        {
            IsAlive = false;
        }
    }
   }

    [SerializeField]
    private bool _isAlive = true;
    [SerializeField]
    private bool isInvincible = false;
    private float timeSinceHit = 0;
    public float invinciblityTime = 0.25f;
    
    
    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool("isAlive", value);
        }
    }

private void Awake() {
    animator = GetComponent<Animator>();
}
public void Update()
{
    if (isInvincible)
    {
        if(timeSinceHit > invinciblityTime)
        {
            isInvincible = false;
            timeSinceHit = 0;
        }

        timeSinceHit += Time.deltaTime;
    }
    
    //This destroys the game object when it is dead
    if (IsAlive == false)
    {
        
        Destroy(gameObject);
    }
}

    public void Hit(int damage, Vector2 knockback)
    {
        if(IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;

            animator.SetTrigger("hit");
            damageableHit?.Invoke(damage, knockback);
        }
    }
}
