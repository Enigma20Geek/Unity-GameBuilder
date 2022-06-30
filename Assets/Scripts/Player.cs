using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour{

    [SerializeField] private LayerMask groundLayer,enemyLayer;
    [SerializeField] private float speedX,jumpSpeed,jumpTime,minX;
    [SerializeField] private Text coins;
    [SerializeField] private GameObject restartPopup;
    public int coinsCnt{
        get{
            return int.Parse(coins.text);
        }
        set{
            coins.text = value.ToString();
        }
    }

    private float jumpDuration;

    private bool isJumping,isJumpKeyHeld,isJumpAudioPlayed;
    
    private int blinkCnt;

    private string BLINK,RUN,JUMP,FALL,DIE;

    private Animator anim;
    private SpriteRenderer sr;
    private Rigidbody2D body;
    private CapsuleCollider2D coll;

    private void Start(){
        blinkCnt = 0;

        isJumping = false;
        isJumpKeyHeld = false;
        isJumpAudioPlayed = false;

        BLINK = "Blink";
        RUN = "Run";
        JUMP = "Jump";
        FALL = "Fall";
        DIE = "Die";

        anim = GetComponent<Animator> ();
        sr = GetComponent<SpriteRenderer> ();
        body = GetComponent<Rigidbody2D> ();
        coll = GetComponent<CapsuleCollider2D> ();

        restartPopup.SetActive(false);
    }

    private void Update() {
        if(coll.enabled && transform.position.y <= -5f){
            Die();
        }
        if(!coll.enabled)
            return;
        if(IsGrounded() && anim.GetBool(JUMP)){
            anim.SetBool(JUMP,false);
        }
        if(IsGrounded() && anim.GetBool(FALL)){
            anim.SetBool(FALL,false);
        }
        RaycastHit2D hit = TouchedEnemy();
        if(hit){
            if(!hit.collider.gameObject.GetComponent<MonsterAI> ().isDead){
                Die();
            }
        }
        hit = OnEnemyHead();
        if(hit){
            if(!hit.collider.gameObject.GetComponent<MonsterAI> ().isDead){
                Physics2D.IgnoreCollision(coll,hit.collider,true);
                hit.collider.gameObject.GetComponent<MonsterAI> ().Die();
                body.velocity = new Vector2(body.velocity.x,jumpSpeed);
            }
        }
        MovePlayer();
        CaptureInput();  
    }

    private void FixedUpdate(){
        if(!coll.enabled)
            return;
        JumpPlayer();
        FallPlayer();
    }

    private void CaptureInput(){
        if(Input.GetButtonDown("Jump") && IsGrounded()){
            isJumping = true;
            isJumpKeyHeld = true;
            jumpDuration = jumpTime;
        }

        if(Input.GetButtonUp("Jump")){
            isJumping = false;
            isJumpKeyHeld = false;
            isJumpAudioPlayed = false;
        }
    }

    private void MovePlayer(){
        float movementX = Input.GetAxisRaw("Horizontal");
        if(transform.position.x + movementX*speedX*Time.deltaTime >= minX){
            transform.position += new Vector3(movementX*speedX,0f,0f) * Time.deltaTime;
        }
        if(movementX > 0){
            anim.SetBool(RUN,true);
            sr.flipX = false;
        }
        else if(movementX < 0){
            anim.SetBool(RUN,true);
            sr.flipX = true;
        }
        else{
            anim.SetBool(RUN,false);
        }
    }

    private void JumpPlayer(){
        if(isJumping && isJumpKeyHeld){
            if(!isJumpAudioPlayed){
                isJumpAudioPlayed = true;
                AudioManager.instance.Play("PlayerJump");
            }
            anim.SetBool(JUMP,true);
            if(jumpDuration > 0f){
                body.velocity = new Vector2(body.velocity.x,jumpSpeed);
                jumpDuration -= Time.fixedDeltaTime;
            }
            else{
                isJumping = false;
            }
        }
    }

    private void FallPlayer(){
        if(body.velocity.y <= 0f && !IsGrounded()){
            anim.SetBool(JUMP,false);
            anim.SetBool(FALL,true);
        }
    }

    public void AnimationEvent(){
        if(!coll.enabled)
            return;
        blinkCnt++;
        blinkCnt%=5;
        if(blinkCnt==0){
            anim.SetTrigger(BLINK);
        }
    }

    private bool IsGrounded(){
        if(!coll.enabled)
            return false;
        return Physics2D.BoxCast(coll.bounds.center,coll.bounds.size,0f,Vector2.down,.1f,groundLayer);
    }

    private RaycastHit2D OnEnemyHead(){
        Vector2 start = new Vector2(transform.position.x,transform.position.y-sr.bounds.size.y/2);
        Vector2 end = new Vector2(transform.position.x,transform.position.y-sr.bounds.size.y/2+0.1f);
        return Physics2D.Linecast(start,end,enemyLayer);
    }

    private RaycastHit2D TouchedEnemy(){
        Vector2 start = new Vector2(transform.position.x+sr.bounds.size.x/2,transform.position.y);
        Vector2 end = new Vector2(transform.position.x+sr.bounds.size.x/2+0.1f,transform.position.y);
        RaycastHit2D hit = Physics2D.Linecast(start,end,enemyLayer);
        if(hit)
            return hit;
        start = new Vector2(transform.position.x-sr.bounds.size.x/2,transform.position.y);
        end = new Vector2(transform.position.x-sr.bounds.size.x/2-0.1f,transform.position.y);
        return Physics2D.Linecast(start,end,enemyLayer);
    }

    public void Die(){
        anim.SetBool(DIE,true);
        body.velocity = new Vector2(body.velocity.x,1.5f*jumpSpeed);
        sr.flipY = true;
        coll.enabled = false;
        AudioManager.instance.Play("PlayerDeath");
        restartPopup.SetActive(true);
        Destroy(gameObject,5f);
    }
}
