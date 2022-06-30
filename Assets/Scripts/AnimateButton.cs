using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateButton : MonoBehaviour{

    [SerializeField]
    private float lerpDuration;
    private float elapsedTime;

    private RectTransform rectTransform;
    private Vector2 start,end;

    private void Awake() {
        rectTransform = GetComponent<RectTransform> ();    
        start = new Vector2(0f,0f);
        end = new Vector2(256f,256f);
    }
    
    void Update(){
        if(!gameObject.activeInHierarchy)
            return;
        elapsedTime+=Time.deltaTime;
        float completion = Mathf.Clamp(elapsedTime/lerpDuration,0f,1f);
        rectTransform.sizeDelta = Vector2.Lerp(start,end,completion);
    }
}
