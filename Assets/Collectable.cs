using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private PlayerController controller;
    void Awake() {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            controller.itemsCollected++;
            Destroy(this.gameObject);
        } 
    }
}
