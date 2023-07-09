using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_script : MonoBehaviour
{
    public Camera cm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(MobSpawner.level%4==0&&MobSpawner.level !=0)  cm.backgroundColor =Color.gray;
    }
}
