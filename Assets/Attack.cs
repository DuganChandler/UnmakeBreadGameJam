using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int attackDamage = 1;
    public Vector2 knockback = Vector2.zero;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Colssion occured");
        Damageable damageable = collision.GetComponent<Damageable>();
        
        if(damageable != null)
        {
            damageable.Hit(attackDamage, knockback);
            Debug.Log("HIT");
        }
    }
}
