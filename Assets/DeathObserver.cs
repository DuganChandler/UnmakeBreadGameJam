using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class DeathObserver : MonoBehaviour
{
    public GameObject player;
    private PlayerController controller; 
    private Damageable damageable;

    void Awake() {
        damageable = player.GetComponent<Damageable>();
        controller = player.GetComponent<PlayerController>();
    }
    void Update() {
        if (!damageable.IsAlive) {
            SceneManager.LoadScene("Title");
        } else if (controller.itemsCollected == 4) {
            SceneManager.LoadScene("Win");
        }
    }
}
