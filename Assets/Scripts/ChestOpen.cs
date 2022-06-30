using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpen : MonoBehaviour{

    private Animator anim;

    private string OPEN;

    private void Awake() {
        
        OPEN = "Open";

        anim = GetComponent<Animator> ();
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        anim.SetBool(OPEN,true);
    }
}
