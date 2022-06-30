using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour{
    
    [SerializeField] private float parallaxEffect;
    [SerializeField] private GameObject cam;

    private float startPos,length;

    void Start(){
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer> ().bounds.size.x;
    }

    void LateUpdate(){
        float dist = cam.transform.position.x * parallaxEffect;
        float temp = cam.transform.position.x*(1-parallaxEffect);

        transform.position = new Vector3(startPos + dist,transform.position.y,transform.position.z);
        if(temp > startPos+length)
            startPos += length;
        else if(temp < startPos-length)
            startPos -= length;
    }
}
