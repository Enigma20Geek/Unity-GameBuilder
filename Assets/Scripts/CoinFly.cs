using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFly : MonoBehaviour{

    [HideInInspector]
    public bool isTaken;

    // [SerializeField]
    private GameObject endPosition,player;

    [SerializeField]
    private float lerpDuration;
    private float lerpTime;

    private string COINUI,PLAYER;

    private Vector3 startPos;

    void Start(){
        isTaken = false;
        COINUI = "CoinUI";
        PLAYER = "Player";
        lerpTime = 0f;
        startPos = transform.position;
        endPosition = GameObject.FindGameObjectWithTag(COINUI);
        player = GameObject.FindWithTag(PLAYER);
    }

    void Update(){
        if(!isTaken)
            return;
        lerpTime += Time.deltaTime;
        float completion = Mathf.Clamp(lerpTime/lerpDuration,0f,1f);
        transform.position = Vector3.Lerp(startPos,endPosition.transform.position,completion);
        if(completion == 1f){
            player.GetComponent<Player> ().coinsCnt++;
            Destroy(gameObject);
        }
    }
}
