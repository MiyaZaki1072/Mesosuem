using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int Money=0;
    public static float Sound;
    public static float Music;
    public int Cost=0;
    public int Heart=3;
    public int Cleaness=5,Popularity=0;
    public float DinoPOP=0,DecorPOP,CurDec,UtilPOP,bin=0,food=0,lamp=0;
    public Image HeartBar;
    public Sprite Heart0,Heart1,Heart2,Heart3;
    public Image Image_Clean;
    public Sprite Star0,Star1,Star2,Star3,Star4,Star5;
    public Image Image_POP;
    public Image Overall;
    public Sprite OverAll1,OverAll2,OverAll3,OverAll4,OverAll5,OverAll6;
    public TMP_Text TEXT_Money;
    public TMP_Text TEXT_Cost;
    public Slider SoundSlider;
    public Slider MusicSlider;
    public float LevelMusic;
    public float LevelSound;
    public bool DuringCutscene=false;
    public VideoPlayerManager videoPlayerManager;
    public int countGarbage=0;
    public bool AlrPlay=false;
    public GameObject SuccesfullScreen;
    public GameObject Lose;
    public QuestMaster questMaster;
    private void Awake() {
        if(Instance  == null){
            Instance = this;
        }
        else Destroy(Instance);
    }
    public void ChangeLVLSOUND(float v1,float v2){
        LevelMusic = v1;
        LevelSound = v2;
    }
    public void ChangeDuring(bool wat ){
        DuringCutscene = wat;
    }
    void Setup(){
        DisplayMoney();
        DisplayHeart();
        DisplayClean();
        DisplayPOP();
        DisplayOverAll();
        DisplayCost();
    }
    void Start()
    {
        questMaster = GameObject.Find("QuestManager").GetComponent<QuestMaster>();
        videoPlayerManager = GameObject.Find("VideoPlayerManager").GetComponent<VideoPlayerManager>();
        GameObject po = GameObject.Find("SettingVal");
        if(po !=null){
            DontDestroy info = po.GetComponent<DontDestroy>();
            if(info != null){
                LevelMusic = info.LevelMusic;
                LevelSound = info.LevelSound;
                SoundSlider.value = LevelSound;
                MusicSlider.value = LevelMusic;
            }
        }
        Setup();   
    }
    public void LoadScene(){
        SceneManager.LoadScene("MainMenu");
    }
    private void Update() {
        if(!DuringCutscene){
        LevelMusic = MusicSlider.value;
        LevelSound = SoundSlider.value;
        }
        else {
            LevelMusic =0;
            LevelSound =0;
        }
        if(((Popularity + Cleaness)/2) == 5 && !AlrPlay){
            videoPlayerManager.PlayCutscene(8);
            AlrPlay = true;
            SuccesfullScreen.SetActive(true);
        }
        if(Money <0){
            Lose.SetActive(true);
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void DisplayCost(){
        TEXT_Cost.text = Cost.ToString();
    }
    public void UpdateCost(int amount){
        Cost+=amount;
        DisplayCost();
    }
    public void DisplayMoney(){
        TEXT_Money.text = Money.ToString();
    }
    public void UpdateMoney(int amount){
        Money +=amount;
        if(Money <0){
            Debug.Log("GAME END");
        }
        DisplayMoney();
    }
   public void DisplayHeart(){
        if(Heart == 0){
            HeartBar.sprite = Heart0;
        }
        else if(Heart==1){
            HeartBar.sprite = Heart1;
        }
        else if(Heart==2){
            HeartBar.sprite = Heart2;
        }
        else if(Heart==3){
            HeartBar.sprite = Heart3;
        }
   }
   public void UpdateHeart(int amount){
        Heart+=amount;
        if(Heart >3)Heart=3;
        DisplayHeart();
   }
   public void DisplayOverAll(){
        int x = (Popularity+Cleaness)/2;
        if(x== 0){
            Overall.sprite = OverAll1;
        }
        else if(x==1){
            Overall.sprite = OverAll2;
        }
        else if(x==2){
            Overall.sprite = OverAll3;
        }
        else if(x==3){
            Overall.sprite = OverAll4;
        }
        else if(x==4){
            Overall.sprite = OverAll5;
        }
        else if(x==5){
            Overall.sprite = OverAll6;
        }
   }
   public void DisplayClean(){
    ///Debug.Log(Cleaness);
     int x = Cleaness;
        if(x== 0){
            Image_Clean.sprite = Star0;
        }
        else if(x==1){
            Image_Clean.sprite = Star1;
        }
        else if(x==2){
            Image_Clean.sprite = Star2;
        }
        else if(x==3){
            Image_Clean.sprite = Star3;
        }
        else if(x==4){
            Image_Clean.sprite = Star4;
        }
        else if(x==5){
            Image_Clean.sprite = Star5;
        }
   }
   public void UpdateClean(int amount){
        countGarbage ++;
        Debug.Log(countGarbage);
        if(countGarbage >0 && countGarbage <=5)Cleaness=5;
        else if(countGarbage<=10)Cleaness=4;
        else if(countGarbage<=15)Cleaness=3;
        else if(countGarbage<=20)Cleaness=2;
        else if(countGarbage<=30)Cleaness=1;
        else Cleaness=0;
        if(Cleaness >5)Cleaness=5;
        DisplayClean();
        DisplayOverAll();
   }
   public void DisplayPOP(){
     Popularity= Mathf.FloorToInt(DinoPOP+DecorPOP+UtilPOP);
     int x=Popularity;
        if(x== 0){
            Image_POP.sprite = Star0;
        }
        else if(x==1){
            Image_POP.sprite = Star1;
        }
        else if(x==2){
            Image_POP.sprite = Star2;
        }
        else if(x==3){
            Image_POP.sprite = Star3;
        }
        else if(x==4){
            Image_POP.sprite = Star4;
        }
        else if(x==5){
            Image_POP.sprite = Star5;
        }
   }
   public void UpdatePOP(float amount){
        DinoPOP+=amount;
        ///Debug.Log(Popularity);
        DisplayPOP();
        DisplayOverAll();
   }
   public void UpdateDec(int amount){
        CurDec+=amount;
        if(CurDec >300)DinoPOP=1;
        else DinoPOP =0;
        DisplayPOP();
        DisplayOverAll();
   }
   public void UpdateUtil(int type,int amount){
        if(type == -1)bin+=amount;
        if(type == -2)food+=amount;
        if(type == -3)lamp+=amount;
        int currentown = questMaster.CurrentOwnPlot;
        if(currentown == 1){
            if(bin >=1 || food >=1 || lamp >=1){
	            UtilPOP = 1;
            }
            else UtilPOP = 0;
        }
        else if(currentown <=4){
            if(bin >=2 && food >=2 && lamp >=2 ){
	            UtilPOP = 2;
            }
            else if(bin >=2 || food >=2 || lamp >=2){
	            UtilPOP = 1;
            }
            else UtilPOP = 0;
        }
        else if(currentown <=9){
            if(bin >=3 && food >=3 && lamp >=3 ){
	            UtilPOP = 2;
            }
            else if(bin >=3 || food >=3 || lamp >=3){
	            UtilPOP = 1;
            }
            else UtilPOP = 0;
        }
        DisplayPOP();
        DisplayOverAll();
   }
}
