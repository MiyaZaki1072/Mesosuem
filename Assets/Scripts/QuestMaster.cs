using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class QuestMaster : MonoBehaviour
{
    public int CurrentOwnPlot = 1;
    public int CurrentPlotQuest = 2;
    public int CurrentVisitor = 0;
    public int CurrentVisitorQuest = 1500;
    public float CurrentHourSpent = 0;
    public float CurrentHourQuest = 3600;
    public int Q1Mx = 8;
    public bool Quest1Mx=false;
    public string Quest1str;
    public string Quest2str;
    public string Quest3str;
    public TMP_Text Quest1;
    public TMP_Text Quest2;
    public TMP_Text Quest3;
    public GameManager gameManager;
    public SoundManager soundManager;
    
    void Start()
    {
        soundManager = GameObject.Find("SoungManager").GetComponent<SoundManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        DisplayQuest();
    }
    void Update()
    {
        DisplayQuest();
        if(CurrentHourQuest == CurrentHourSpent ){
            AdvanceQuest(3);
            gameManager.UpdateHeart(1);
            soundManager.PlayWorker(5);
        }
        if(CurrentOwnPlot == CurrentPlotQuest && !Quest1Mx){
            if(CurrentOwnPlot == Q1Mx){Quest1Mx = true;}
            AdvanceQuest(1);
            gameManager.UpdateHeart(1);
            soundManager.PlayWorker(5);
        }
        if(CurrentVisitor == CurrentVisitorQuest){
            AdvanceQuest(2);
            gameManager.UpdateHeart(1);
            soundManager.PlayWorker(5);
        }
    }
    private void DisplayQuest(){
        string Q1 = "Acquiring a total of "+CurrentPlotQuest.ToString()+" "+"plots of land("+CurrentOwnPlot.ToString()+"/"+CurrentPlotQuest.ToString()+")";
        string Q2 = "Reach a total of "+CurrentVisitorQuest.ToString()+" "+"visitors("+CurrentVisitor.ToString()+"/"+CurrentVisitorQuest.ToString()+")";
        string Q3 = "Stay logged in for "+(CurrentHourQuest/3600).ToString("F2")+" hours("+((CurrentHourSpent)/3600).ToString("F2")+"/"+((CurrentHourQuest)/3600).ToString("F2")+")";
        ///Debug.Log(CurrentHourSpent);
        Quest1.text = Q1;
        Quest2.text = Q2;
        Quest3.text = Q3;
    }
    public void UpdateQuest(int index,float time){
        if(index == 1)CurrentOwnPlot++;
        if(index == 2)CurrentVisitor++;
        if(index == 3)CurrentHourSpent = time;
    }
    public void AdvanceQuest(int index){
        if(index == 1){
            if(CurrentPlotQuest != 8){
                CurrentPlotQuest+=2;
            }
        }
        if(index == 2){
            CurrentVisitorQuest +=1500;
        }
        if(index == 3){
            CurrentHourQuest +=7200;
        }
        DisplayQuest();
    }
}
