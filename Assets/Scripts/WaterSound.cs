using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSound : MonoBehaviour{

    private string PLAYER;

    void Start(){
        PLAYER = "Player";
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag(PLAYER)){
            AudioManager.instance.Play("WaterSound");
        }
    }
}
