using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class mushroom_script : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject this_object;
    private Animator anim;
    private bool isDead;
    public bool isDead2;
    public LayerMask deadMonster;
    public float checkRadius;
    public Transform headPos;
    public LayerMask Player;
    public Transform bodyPos;
    public LayerMask monster;
    public LayerMask Wall;
    private Rigidbody2D rb;
    private CircleCollider2D col;
    public float speed;
    private int direction=1;
    private bool wallCollision;
    public bool monsterCollision=false;
    public float check;
    private bool temp=false;
    private int directionDegree=0;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    wallCollision = Physics2D.OverlapCircle(headPos.position,0.1f,Wall);
    rb.velocity = new Vector2(speed*direction, rb.velocity.y);
    check = rb.velocity.y;
    //if(monsterCollision&&rb.velocity.y<0)direction*=-1;
    }
    void changeDirection(){
        direction *=-1;
        directionDegree +=180;
        transform.eulerAngles = new Vector3(0,directionDegree,0);

    }


    private void OnTriggerEnter2D(Collider2D collider){
        changeDirection();
        isDead = Physics2D.OverlapCircle(headPos.position,checkRadius,Player);
        isDead2 = Physics2D.OverlapCircle(transform.position,1f,deadMonster);
    }
    private void OnTriggerExit2D(Collider2D other) {
        StartCoroutine(deathTimer(1f));
    }

    void OnCollisionEnter2D(Collision2D col){
        if(wallCollision&&rb.velocity.y<0)direction*=-1;
    }
    void OnCollisionExit2D(Collision2D col){
        if(isDead2){
             
        mario_move.score += 1;
        }
    }
    IEnumerator deathTimer(float val)
    {
    if(isDead||isDead2){
        if(isDead2){Destroy(GetComponent<CircleCollider2D>());
        }
        Destroy(gameObject, 0.8f);
        anim.SetBool("dead",true);
        direction=0;
        yield return new WaitForSeconds (val);
        gameObject.layer = LayerMask.NameToLayer("deadMonster");
    }

    }
}
