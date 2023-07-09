using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MobSpawner : MonoBehaviour
{
    public float startSpawnTime =1;
    public float spawnTime;
    [SerializeField]
    public static int level;
    public Transform[] spawnPoints;
    public Text scoreText;
    public GameObject Boss1;
    public GameObject[] enemyPrefabs;
    public static MobSpawner Score;
    private Vector3 pos = new Vector3(0f,0f,0f);
    void Start(){
      spawnTime = startSpawnTime;
    }
    // enemy spawner
    void LevelUp(){
    }
    void Update(){
    if(level==4)BossFight();
        if(spawnTime <=0&&(level%4!=0||level==0)){
            int currentLevel = level+1;
            if(currentLevel>enemyPrefabs.Length) currentLevel = enemyPrefabs.Length;
            int randEnemy = Random.Range(0,currentLevel);
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);
            pos = new Vector3(spawnPoints[randSpawnPoint].transform.position.x,spawnPoints[randSpawnPoint].transform.position.y,0f);
            Instantiate(enemyPrefabs[randEnemy],pos,transform.rotation);
            spawnTime = startSpawnTime;
        }
        else spawnTime -= Time.deltaTime;
    }
   void BossFight(){
        Debug.Log("Boss Fight");
        Instantiate(Boss1,new Vector3(0,4,0),transform.rotation);
        level +=1;
   }
}
