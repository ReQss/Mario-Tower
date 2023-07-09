using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_script : MonoBehaviour
{
    private Rigidbody2D rb;
    public float startSpawnTime =2;
    public float spawnTime;
    private int dir = 1;
    // Start is called before the first frame update
    void Start()
    {
      //  startMove = moveTime;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnTime <=0){
            rb.velocity = new Vector2(dir*18, rb.velocity.y);
            dir*=-1;
            spawnTime = startSpawnTime;
        }
        else spawnTime -= Time.deltaTime;
    }
   
}
