using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour{
    
    [SerializeField] private Transform leftChecker,rightChecker;
    [SerializeField] private float speedX;
    [SerializeField] private LayerMask groundLayer;

    private BoxCollider2D coll;
    private SpriteRenderer sr;
    private Animator anim;
    private Rigidbody2D body;

    [HideInInspector]
    public bool isDead;
    private string DIE;

    void Start(){
        isDead = false;
        DIE = "Die";

        coll = GetComponent<BoxCollider2D> ();
        sr = GetComponent<SpriteRenderer> ();
        anim = GetComponent<Animator> ();
        body = GetComponent<Rigidbody2D> ();
    }

    void Update(){
        transform.position += new Vector3(speedX,0f,0f) * Time.deltaTime;
        if(speedX > 0f && isGrounded() && RightGroundCheck()){
            speedX *= -1f;
            sr.flipX = true;
        }
        if(speedX < 0f && isGrounded() && LeftGroundCheck()){
            speedX = -speedX;
            sr.flipX = false;
        }
    }

    private bool LeftGroundCheck(){
        Vector2 start = new Vector2(leftChecker.position.x,leftChecker.position.y);
        Vector2 end = new Vector2(leftChecker.position.x,leftChecker.position.y-0.1f);
        return !Physics2D.Linecast(start,end,groundLayer);
    }

    private bool RightGroundCheck(){
        Vector2 start = new Vector2(rightChecker.position.x,rightChecker.position.y);
        Vector2 end = new Vector2(rightChecker.position.x,rightChecker.position.y-0.1f);
        return !Physics2D.Linecast(start,end,groundLayer);
    }

    private bool isGrounded(){
        return Physics2D.BoxCast(coll.bounds.center,coll.bounds.size,0f,Vector2.down,.1f,groundLayer);
    }

    public void Die(){
        speedX = 0f;
        isDead = true;
        anim.SetBool(DIE,true);
        AudioManager.instance.Play("MonsterDeath");
        Destroy(gameObject,5f);
    }
}
