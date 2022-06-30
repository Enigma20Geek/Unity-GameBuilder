using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour{

    private string PLAYER;

    private void Awake() {
        PLAYER = "Player";
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag(PLAYER)){
            other.gameObject.GetComponent<Player> ().Die();
        }
    }
}
