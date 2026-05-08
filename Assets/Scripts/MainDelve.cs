using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDelve : MonoBehaviour
{
    public bool IsDiggingMain = false;
    public float MainCur = 0;
    public int EnterHole(){
        return UnityEngine.Random.Range(0,3);
    }
    private void Update() {
        if(IsDiggingMain){
            MainCur -= Time.deltaTime;
        }
    }
}
