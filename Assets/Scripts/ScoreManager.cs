using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
   // public static ScoreManager instance;
    public Text scoreText;
    private Animator anim;
    int score = 0;
   
    void Start()
    {
        anim = GetComponent<Animator>();
        scoreText.text = "POINTS: " + score.ToString();
    }

    // Update is called once per frame
    public void AddPoint(){
        score+=1;
        scoreText.text = "POINTS: " + score.ToString();
    }
    
}
