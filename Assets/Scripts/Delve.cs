using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class Delve : MonoBehaviour
{
    public GameObject[] YesOrNoShow;
    private int Lastindex=-1;
    public int CorrectHole;
    private float currentTime;
    public float startTime=0;
    public bool IsDigging = false;
    public string IsCorrectIsLAND ="";
    public int IsCorrectIndex;
    public bool IsCorrect = false;
    public Research_Delve research_Delve;
    public GameObject[] AllHoles;
    public GameObject[] AllDigger;
    public GameObject CurrentDelve;
    public TMP_Text DiggingTime;
    public Button[] AllButton;
    public GameObject SuccesFull;
    public GameObject BadFull;
    public int CurIndex;
    public MainDelve DelveManager;
    public SoundManager soundManager;
    private bool play = false;
    public VideoPlayerManager videoPlayerManager;
    void Start()
    {
        soundManager = GameObject.Find("SoungManager").GetComponent<SoundManager>();
        DelveManager = GameObject.Find("MainDelveManager").GetComponent<MainDelve>();
        research_Delve = GameObject.Find("ResearchManager").GetComponent<Research_Delve>();
        CorrectHole = DelveManager.EnterHole();
        videoPlayerManager = GameObject.Find("VideoPlayerManager").GetComponent<VideoPlayerManager>();
        Debug.Log(CorrectHole);
    }
    void Update(){
        if(DelveManager.IsDiggingMain){
        currentTime = DelveManager.MainCur;
        DiggingTime.text = ConvertFloatTimeToMinutesSeconds(currentTime);
        if (currentTime <= 0.0f)
        {
            if(!play)soundManager.PlayWorker(3);
            play=true;
            DiggingTime.text = "";
            currentTime = 0.0f; // Ensures timer doesn't go negative
            // Timer finished! Execute any actions here (e.g., play sound, change scene)
            if(IsCorrect){
                DiggingTime.text = "";
                SuccesFull.SetActive(true);
                if(research_Delve.DoneDelve){
                Debug.Log(CurIndex);
                string curisland = research_Delve.CurrentIsland;
                string curtype = research_Delve.CurrentTypeResearch;
                int curindex = research_Delve.index;
                if(IsCorrectIsLAND == "Grass")research_Delve.DelveGrassDone[curindex] = true;
                else research_Delve.DelveDesertDone[curindex] = true;
                if(curisland == "Grass"){
                    research_Delve.BackToMainMenu(); 
                    research_Delve.GrassDelve[curindex].SetActive(false);
                    research_Delve.SelectPinImage.gameObject.SetActive(false);
                }
                if(curisland == "Desert"){
                    research_Delve.BackToMainMenu(); 
                    research_Delve.DesDelve[curindex].SetActive(false);
                    research_Delve.SelectPinImage.gameObject.SetActive(false);
                }
                SuccesFull.SetActive(false);
                DelveManager.IsDiggingMain = false;
                }
                return; 
            }
            else{
                for(int i=0;i<3;i++){
                    if(i != CurIndex){
                    AllHoles[i].SetActive(true);
                    AllButton[i].interactable = true;
                    }
                    else AllButton[i].gameObject.SetActive(false);
                }
                BadFull.SetActive(true);
            }
            DelveManager.IsDiggingMain = false;
        }
        //Debug.Log(currentTime);
        }
    }
    public string ConvertFloatTimeToMinutesSeconds(float timeInFloat)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeInFloat);
        return timeSpan.ToString("mm':'ss");
    }
    public void OnHoleClick(int index){
        if(Lastindex !=-1)YesOrNoShow[Lastindex].SetActive(false);
        YesOrNoShow[index].SetActive(true);
        Lastindex = index;
    }
    public void OnExit(int index){
        soundManager.PlayWorker(1);
        YesOrNoShow[index].SetActive(false);
        Lastindex =-1;
    }
    public void OnYes(int Index){
        if(DelveManager.IsDiggingMain == false){
        soundManager.PlayWorker(0);
        startTime = 1*30;
        AllDigger = GameObject.FindGameObjectsWithTag("Digger");
        Debug.Log(AllDigger.Length);
        currentTime = startTime-(startTime*(10f/100)*AllDigger.Length);
        DelveManager.MainCur = currentTime;
        DelveManager.IsDiggingMain = true;
        if(Index == CorrectHole){
            IsCorrectIsLAND = research_Delve.CurrentIsland;
            IsCorrectIndex = research_Delve.index;
            IsCorrect = true;
        }
        for(int i=0;i<3;i++){
            if(i !=Index)AllHoles[i].SetActive(false);
            if(i== Index)AllButton[i].interactable = false;
        }
        OnExit(Index);
        CurIndex = Index;
        videoPlayerManager.PlayCutscene(7);
        }
    }
}
