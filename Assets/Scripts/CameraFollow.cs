using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour{
    
    [SerializeField] private GameObject player;
    [SerializeField] private float minX,maxX;

    private Vector3 tempPos;

    void LateUpdate(){
        if(!player || !player.GetComponent<CapsuleCollider2D> ().enabled)
            return;

        tempPos = transform.position;
        tempPos.x = player.transform.position.x;
        
        if(tempPos.x < minX){
            tempPos.x = minX;
        }
        if(tempPos.x > maxX){
            tempPos.x = maxX;
        }

        transform.position = tempPos;
    }
}
