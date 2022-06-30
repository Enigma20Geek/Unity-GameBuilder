using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTake : MonoBehaviour{

    private string PLAYER_TAG;

    private ParticleSystem ps;
    private SpriteRenderer sr;
    private BoxCollider2D coll;

    private void Awake() {
        PLAYER_TAG = "Player";

        ps = GetComponent<ParticleSystem> ();
        sr = GetComponent<SpriteRenderer> ();
        coll = GetComponent<BoxCollider2D> ();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(!GetComponent<CoinFly> ().isTaken && other.CompareTag(PLAYER_TAG)){
            ps.Play();
            AudioManager.instance.Play("CoinCollect");
            coll.enabled = false;
            GetComponent<CoinFly> ().isTaken = true;
        }
    }
}
