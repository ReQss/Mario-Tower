using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
public class turtle_script : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    private bool isDead;
    public bool isDead2=false;
    public LayerMask deadMonster;
    public float checkRadius;
    public Transform headPos;
    public LayerMask Player;
    public LayerMask monster;
    public LayerMask Wall;
    private Rigidbody2D rb;
    private CircleCollider2D col;
    public float speed;
    private int direction=1;
    public bool wallCollision;
    public bool monsterCollision=false;
    public float check;
    private bool temp=false;
    public float startJumpTime = 3;
    public float JumpTime;
    private bool deadState = false;
    public Transform Left;
    public Transform Right;
    private bool RightCol = false;
    private bool LeftCol = false;
    public bool shellAttackTrigger = false;
    private int directionDegree = 180;
    public GameObject head;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        JumpTime = Random.Range(4, 6);
    }
    IEnumerator death(float val)
    {
    yield return new WaitForSeconds (0.3f);
    gameObject.layer = LayerMask.NameToLayer("deadMonster");
  //  head.layer =  LayerMask.NameToLayer("deadMonster");
    }
    // Update is called once per frame
    void Update()
    {

    RightCol = Physics2D.OverlapCircle(Right.position,0.1f,Player);
    LeftCol = Physics2D.OverlapCircle(Left.position,0.1f,Player);
    wallCollision = Physics2D.OverlapCircle(headPos.position,1f,Wall);
    isDead = Physics2D.OverlapCircle(headPos.position,checkRadius,Player);
    isDead2 = Physics2D.OverlapCircle(transform.position,1f,deadMonster);
    if(isDead2&&!deadState){
        Destroy(GetComponent<CircleCollider2D>());
        Destroy(gameObject,0.5f);
        }
    if(isDead&&!isDead2){
        if(deadState == false)
            transform.eulerAngles = new Vector3(0,180,0);
        deadState = true;
        anim.SetBool("dead",true);
        // GetComponent<CircleCollider2D>().size = new Vector2(1f, 0.5f);
        // GetComponent<CircleCollider2D>().offset = new Vector2(0, -0.5f);
        direction=0;
        //Destroy(gameObject.GetComponent<Collider>());
        //Destroy(gameObject, 4f);
        GetComponent<CircleCollider2D>().offset = new Vector2(-0.125f, -0.45f);
        GetComponent<CircleCollider2D>().radius = 0.57f;
        if(deadState){StartCoroutine(death(0.1f));
        Destroy(gameObject, 5f);}
    }
    if(shellAttackTrigger)ShellAttack();
    check = rb.velocity.y;
    // random jumps;
    if(deadState==false){
        Jump();
    }
    }



    void changeDirection(){
        direction *=-1;
        directionDegree +=180;
        transform.eulerAngles = new Vector3(0,directionDegree,0);
    }
    void Jump(){
        rb.velocity = new Vector2(speed*direction, rb.velocity.y);
        if(JumpTime<=0){
            rb.velocity = new Vector2(rb.velocity.x,speed*2f);
            JumpTime = Random.Range(4, 6);
        }
        else JumpTime -= Time.deltaTime;
    }
    void ShellAttack(){
        if(deadState){
            if(LeftCol){
                direction = 1;
            }
            if(RightCol){
                direction = -1;
            }
            rb.velocity = new Vector2(speed*direction*5, rb.velocity.y);
        }
        gameObject.layer = LayerMask.NameToLayer("deadMonsterAttack");
        head.layer =  LayerMask.NameToLayer("deadMonsterAttack");
    }
    private void OnTriggerEnter2D(Collider2D collider){
        isDead = Physics2D.OverlapCircle(headPos.position,checkRadius,Player);
        if(isDead)mario_move.score+=1;
        changeDirection();
        
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(!deadState){
        deathTimer();
        }
        if(isDead)mario_move.score+=1;
    }
    void deathTimer()
    {
    if(isDead2){
       // Destroy(GetComponent<CircleCollider2D>());
        mario_move.score += 1;
        Destroy(gameObject, 0.8f);
        anim.SetBool("dead",true);
        direction=0;
        }
    }
    void OnCollisionEnter2D(Collision2D col){
    if(deadState){
        shellAttackTrigger = true;
        ShellAttack();
    }
    monsterCollision = Physics2D.OverlapCircle(headPos.position,0.7f,monster);
    if(monsterCollision){
       // if(deadState)changeDirection();
    }
    }
}
