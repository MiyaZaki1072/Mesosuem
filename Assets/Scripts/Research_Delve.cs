using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Research_Delve : MonoBehaviour
{
    public Image CurrentType;
    public string CurrentIsland;
    public string CurrentTypeResearch;
    public Button ResearchBut,DelveBut,RealExit;
    public Sprite[] TypeSprite;
    public Sprite[] GrassPin;
    public Image[] RealGrassPin;
    public Image[] RealDesPin;
    public Sprite[] OrangePin;
    public GameObject[] GrassDino;
    public GameObject[] DesDino;
    public GameObject[] GrassDelve;
    public GameObject[] DesDelve;
    public Sprite[] Heart;
    public Image CurrentHeart;
    public Button ArrowL;
    public Button ArrowR;
    public GameObject[] Island;
    public bool[] ResearchGrassDone;
    public bool[] DelveGrassDone;
    public bool[] ResearchDesertDone;
    public bool[] DelveDesertDone;
    public Image BlockBG;
    public GameManager gameManager;
    public Image SelectPinImage;
    public int index=-1;
    public MainDelve DelveManager;
    public int strcurpin;
    public string strcurisland;
    public bool DoneResearch;
    public bool DoneDelve;
    public VideoPlayerManager videoPlayerManager;
    private void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        DelveManager = GameObject.Find("MainDelveManager").GetComponent<MainDelve>();
        videoPlayerManager = GameObject.Find("VideoPlayerManager").GetComponent<VideoPlayerManager>();
    }
    public void EnterResearch(){
        BlockBG.enabled = false;
        ResearchBut.gameObject.SetActive(false);
        DelveBut.gameObject.SetActive(false);
        CurrentType.gameObject.SetActive(true);
        CurrentType.sprite = TypeSprite[0];
        RealExit.gameObject.SetActive(false);
        CurrentIsland = "Grass";
        CurrentTypeResearch ="Research";
        DisplayIsland();
    }
    public void EnterDelve(){
        BlockBG.enabled = false;
        ResearchBut.gameObject.SetActive(false);
        DelveBut.gameObject.SetActive(false);
        CurrentType.gameObject.SetActive(true);
        CurrentType.sprite = TypeSprite[1];
        RealExit.gameObject.SetActive(false);
        CurrentIsland = "Grass";
        CurrentTypeResearch = "Delve";
        DisplayIsland();
        if(DelveManager.IsDiggingMain){
            if(strcurisland == "Grass"){
                GrassDelve[strcurpin].gameObject.SetActive(true);
            }
            if(strcurisland == "Desert"){
                DesDelve[strcurpin].gameObject.SetActive(true);
            }
        }
    }
    public void BackToMainMenu(){
        BlockBG.enabled = true;
        ResearchBut.gameObject.SetActive(true);
        DelveBut.gameObject.SetActive(true);
        RealExit.gameObject.SetActive(true);
        CurrentType.gameObject.SetActive(false);
    }
    public void Switch(){
        if(CurrentIsland =="Grass"){
            CurrentIsland = "Desert";
        }
        else CurrentIsland = "Grass";
        DisplayIsland();
    }
    public void CheckWhatDone(string str){
        if(str == "Grass"){
            for(int i=0;i<3;i++){
                if(CurrentTypeResearch =="Research"){
                    if(ResearchGrassDone[i]) RealGrassPin[i].gameObject.SetActive(false);
                    else RealGrassPin[i].gameObject.SetActive(true);
                }
                if(CurrentTypeResearch =="Delve"){
                    if(DelveGrassDone[i])RealGrassPin[i].gameObject.SetActive(false);
                    else RealGrassPin[i].gameObject.SetActive(true);
                }
            }
        }
        if(str == "Desert"){
            for(int i=0;i<3;i++){
                if(CurrentTypeResearch =="Research"){
                    if(ResearchDesertDone[i]) RealDesPin[i].gameObject.SetActive(false);
                    else RealDesPin[i].gameObject.SetActive(true);
                }
                if(CurrentTypeResearch =="Delve"){
                    if(DelveDesertDone[i])RealDesPin[i].gameObject.SetActive(false);
                    else RealDesPin[i].gameObject.SetActive(true);
                }
            }
        }
    }
    public void DisplayIsland(){
        CheckWhatDone(CurrentIsland);
        if(CurrentIsland == "Grass"){

            Island[1].gameObject.SetActive(false);
            Island[0].gameObject.SetActive(true);
        }
        if(CurrentIsland == "Desert"){
            Island[0].gameObject.SetActive(false);
            Island[1].gameObject.SetActive(true);
        }
    }
    public void DisplayCurrentHeart(){
        CurrentHeart.sprite = Heart[gameManager.Heart];
    }
    public void OnpinClick(int x){
        index = x;
        DisplayCurrentHeart();
        if(CurrentIsland == "Grass"){
            SelectPinImage.sprite = GrassPin[index];
        }
        if(CurrentIsland == "Desert"){
            SelectPinImage.sprite = OrangePin[index];
        }
        Debug.Log(gameManager.Heart);
    }
    public void OnYesClick(){
        if(gameManager.Heart>0){
            gameManager.Heart--;
            if(CurrentTypeResearch == "Research"){
                if(CurrentIsland == "Grass"){
                    GrassDino[index].SetActive(true);
                    videoPlayerManager.PlayCutscene(index+1);
                }
                if(CurrentIsland == "Desert"){
                    DesDino[index].SetActive(true);
                    videoPlayerManager.PlayCutscene(index+4);
                }
                DoneResearch = false;
            }
            if(CurrentTypeResearch == "Delve"){
                if(CurrentIsland == "Grass"){
                    GrassDelve[index].SetActive(true);
                    //videoPlayerManager.PlayCutscene(index+1);
                }
                if(CurrentIsland == "Desert"){
                    DesDelve[index].SetActive(true);
                    //videoPlayerManager.PlayCutscene(index+4);
                }
                strcurisland = CurrentIsland;
                strcurpin = index;
                DoneDelve = false; 
            }   
        }
    }
    public void DoneR(){
        DoneResearch = true;
    }
    public void DoneD(){
        DoneDelve = true;
    }
}
