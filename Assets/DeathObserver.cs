using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class DeathObserver : MonoBehaviour
{
    public GameObject player;
    private Damageable damageable;

    void Awake() {
        damageable = player.GetComponent<Damageable>();
    }
    void Update() {
        if (!damageable.IsAlive) {
            SceneManager.LoadScene("Title");
        }
    }
}
