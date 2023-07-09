using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class mario_move : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    public float speed;
    private float moveInput;
    private bool isGrounded;
    public Transform feetPos;
    public Transform handPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    public LayerMask Wall;
    public LayerMask monster;
    public LayerMask deadMonster;
    public float jumpForce;
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;
    public bool marioDeath;
    private bool deadMonsterCollision=false;
    public GameObject player;
    private Animator anim;
    private bool enemyKill=false;
    int speed2 = 1;
    private bool wallCollision=false;
    public Text scoreText;
    public Text levelText;
    public Image[] hearts;
    private int heartAmount = 3;
    [SerializeField]
    static public int score=0;
    private bool pause;
    public Text GameOver;
    public bool Invincible=false;
    void Start()
    {
        GameOver.enabled = false;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
      // Update is called once per frame
    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        if(Input.GetKey(KeyCode.DownArrow)==false)anim.SetBool("duck",false);
        if(!anim.GetBool("duck"))
            rb.velocity = new Vector2(moveInput * speed * (float)speed2, rb.velocity.y);
    }
    void Update(){
        LevelUp();
        scoreText.text = "POINTS: " + score.ToString();
        isGrounded = Physics2D.OverlapCircle(feetPos.position,checkRadius,whatIsGround);
        enemyKill =  Physics2D.OverlapCircle(feetPos.position,checkRadius,monster);
        deadMonsterCollision =  Physics2D.OverlapCircle(feetPos.position,checkRadius,deadMonster);
        wallCollision = Physics2D.OverlapCircle(handPos.position,checkRadius,Wall);
        moveAnimation();
        if(deadMonsterCollision)jump();
        if(isGrounded && (Input.GetKeyDown(KeyCode.UpArrow)||deadMonsterCollision)){
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }
        jump();
        if(isGrounded==false)anim.SetBool("ground",false);
        if(wallCollision==false)speed2=1;
        else speed2=0;
        marioDeath =false; 
    }
    void jump(){
        if(isGrounded && Input.GetKeyDown(KeyCode.UpArrow)){
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }
        if((Input.GetKey(KeyCode.UpArrow)||deadMonsterCollision)&&isJumping){
            if(jumpTimeCounter>0){
            rb.velocity = Vector2.up * jumpForce;
            jumpTimeCounter -=Time.deltaTime;
            }
        }
        else isJumping = false;
        if(isJumping&& isGrounded!=true)anim.SetBool("jump",true);
        if(isGrounded)anim.SetBool("jump",false);
    }
    void moveAnimation(){
        if(isGrounded)anim.SetBool("jump",false);
        if(Input.GetKey(KeyCode.RightArrow)){
            jump();
            player.transform.eulerAngles = new Vector3(0,0,0);
            anim.SetBool("run",true);
            anim.SetBool("duck",false);
        }
        else if(Input.GetKey(KeyCode.LeftArrow)){
            jump();
            player.transform.eulerAngles = new Vector3(0,180,0);
             anim.SetBool("run",true);
             anim.SetBool("duck",false);
        }
        else anim.SetBool("run",false);
        // kucanie
        if(Input.GetKey(KeyCode.DownArrow))anim.SetBool("duck",true);
        
        if(Input.GetKeyUp(KeyCode.DownArrow))anim.SetBool("duck",false);

    }
    // enemy kill and mario death
    IEnumerator Death(){
        if(enemyKill==true){
            rb.velocity = Vector2.up * jumpForce*2;
            AddPoint(1);
            isGrounded=true;
            
        }
        else{
        if(!Invincible)
        marioDeath = Physics2D.OverlapCircle(handPos.position,0.9f,monster);
            if(marioDeath){
                if(heartAmount>1){
                anim.SetBool("death",true);
                heartAmount-=1;
                Destroy(hearts[heartAmount],0f);
                rb.velocity = new Vector2(-1* speed * (float)speed2, jumpForce * 2);
                Invincible = true;
                yield return new WaitForSeconds (2.5f);
                Invincible = false;
                anim.SetBool("death",false);
                }
                else {
                    StartCoroutine(gameOver());
                    }
            }
        }
    }
    void OnCollisionEnter2D(Collision2D col){
    //add point if enemy is killed
        StartCoroutine(Death());

    }
    //score manager
    public void AddPoint(int val){
        score+=val;
    }
    public void setPoint(int val){
        score=val;
    }
    public void LevelUp(){
        if(score>=10){
        MobSpawner.level+=1;
        levelText.text = "LEVEL: " + MobSpawner.level.ToString();
        score=0;
        }
    }
    IEnumerator gameOver(){
        GameOver.text = "Game Over\n" + "Level: " + MobSpawner.level.ToString()  ;
        GameOver.enabled = true;
        Time.timeScale = 0;
        yield return new WaitForSeconds (3);
        Time.timeScale = 1;
        Application.LoadLevel(0);
    }
   
    
}
