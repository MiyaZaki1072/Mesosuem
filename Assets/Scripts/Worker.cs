using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Worker : MonoBehaviour
{
    public int Salary;
    public string Name;
    public string Job;
    public int WorkSkill;
    public TMP_Text Name_Text;
    public TMP_Text Salary_Text;
    public Image WorkerSkill_Image;
    public List<Sprite> SkillStar = new List<Sprite>();
    public GameManager gameManager;
    public SpawnVisitor spawnVisitor;
    public int CurWorker=0;
    public GameObject[] AllWorker;
    public List<GameObject> Allworkerlist;
    public TMP_Text[] TextCurName;
    public TMP_Text[] TextCurCost;
    public Image[] ImageCur;
    public Image[] JobImage;
    public Sprite[] JobSprite;
    public Button[] ButtonCur;
    public Image[] RatingWorker;
    public Image CurrentPeople;
    public Sprite[] StarRating;
    public TMP_Text TextPosition;
    public List<string> Joblist = new List<string>(){"Cleaner","Digger"};
    public List<string> AllNames = new List<string>(){"Yuri","Frey ", "Kati ", "Charlie ", "David ", "Emily ", "Michael ", "Jennifer ", "Matthew ", "Ashley ", "Kevin ", "Sarah ", "William ", "Elizabeth ", "Daniel ", "Jessica ", "Andrew ", "Amanda ", "Christopher ", "Samantha ", "Joseph ", "Stephanie ", "Ryan ", "Nicole ", "Nicholas ", "Kayla ", "Jacob ", "Margaret ", "Ethan ", "Lauren ", "Aiden ", "Sofia ", "Noah ", "Mia ", "Logan ", "Chloe ", "Jackson ", "Isabelle ", "Benjamin ", "Evelyn ", "Alexander ", "Madison ", "Daniel ", "Hailey ", "William ", "Charlotte ", "Angel ", "Abigail ", "Anthony ", "Olivia ", "David "};
    public void GetRandomResume(){
        int WhatisName =  Random.Range(0,AllNames.Count);
        Name_Text.text = AllNames[WhatisName];
        Name = AllNames[WhatisName];
        int WorkerSkill = Random.Range(0,100);
        if(WorkerSkill <=30)WorkSkill =1;
        else if(WorkerSkill <=50)WorkSkill = 2;
        else if(WorkerSkill <=70)WorkSkill = 3;
        else if(WorkerSkill <=90)WorkSkill = 4;
        else if(WorkerSkill <=100)WorkSkill =5;
        WorkerSkill_Image.sprite = SkillStar[WorkSkill];
        if(WorkSkill == 1)Salary = Random.Range(1,25);
        else if(WorkSkill == 2)Salary = Random.Range(10,30);
        else if(WorkSkill == 3)Salary = Random.Range(15,40);
        else if(WorkSkill == 4)Salary = Random.Range(15,50);
        else if(WorkSkill == 5)Salary = Random.Range(25,45);
        Salary_Text.text = Salary.ToString();
        int WhatJob = Random.Range(0,2);
        Job = Joblist[WhatJob];
        TextPosition.text = Job;
        if(Job == "Cleaner")CurrentPeople.sprite = JobSprite[0];
        else CurrentPeople.sprite = JobSprite[1];
    }
    public void Buy(){
        if(CurWorker <6 && Job == "Cleaner"){
            gameManager.UpdateCost(Salary);
            spawnVisitor.SpawnCleaner(WorkSkill,Salary,Name);
            CurWorker++;
            ShowCurrentWorker();
            GetRandomResume();
        }
        else if (CurWorker <6 && Job == "Digger"){
            gameManager.UpdateCost(Salary);
            spawnVisitor.SpawnDigger(WorkSkill,Salary,Name);
            CurWorker++;
            ShowCurrentWorker();
            GetRandomResume();
        }
    }
    public void ShowCurrentWorker(){
        AllWorker = GameObject.FindGameObjectsWithTag("Cleaner");
        Allworkerlist.Clear();
        for(int i=0;i<AllWorker.Length;i++){
            if(AllWorker[i] != null){
            Allworkerlist.Add(AllWorker[i]);
            }
        }
        Debug.Log(Allworkerlist.Count);
        for(int i=0;i<Allworkerlist.Count;i++){
            TextCurCost[i].enabled =true;
            TextCurName[i].enabled = true;
            ImageCur[i].enabled =true;
            RatingWorker[i].enabled =true;
            ButtonCur[i].gameObject.SetActive(true);
            Cleaner cleanerScript = Allworkerlist[i].GetComponent<Cleaner>();
            if (cleanerScript != null)
            {
                TextCurName[i].text = cleanerScript.Name;
                TextCurCost[i].text = cleanerScript.Cost.ToString();
                ImageCur[i].sprite = JobSprite[0];
                RatingWorker[i].sprite = StarRating[cleanerScript.WorkSpeed-1];
                Debug.Log(cleanerScript.Name + " (Cleaner)"); // Indicate cleaner type
            }
            else
            {
                Digger diggerScript = Allworkerlist[i].GetComponent<Digger>();
                if (diggerScript != null)
                {
      // Update UI elements for Digger (assuming you have properties like Name and Cost)
                    TextCurName[i].text = diggerScript.Name; // Update with Digger's name property
                    TextCurCost[i].text = diggerScript.Cost.ToString(); // Update with Digger's cost property
                    ImageCur[i].sprite = JobSprite[1];
                    RatingWorker[i].sprite = StarRating[diggerScript.WorkSpeed-1];
                    Debug.Log(diggerScript.Name + " (Digger)"); // Indicate digger type
                }
            }
        }
        if(Allworkerlist.Count <6){
            for(int i=Allworkerlist.Count;i<6;i++){
                TextCurCost[i].enabled =false;
                TextCurName[i].enabled =false;
                ImageCur[i].enabled =false;
                ButtonCur[i].gameObject.SetActive(false);
                RatingWorker[i].enabled =false;
            }
        }
    }
    public void Update() {
       ShowCurrentWorker(); 
    }
    public void Laidoff(int val){
        AllWorker =GameObject.FindGameObjectsWithTag("Cleaner");
        Cleaner cleanerScript = Allworkerlist[val].GetComponent<Cleaner>();
        if(cleanerScript != null){
        gameManager.UpdateCost(-cleanerScript.Cost);
        Debug.Log(cleanerScript.Name);
        GameObject ObjtoDestroy = Allworkerlist[val];
        Destroy(ObjtoDestroy);
        Allworkerlist.RemoveAt(val);
        CurWorker--;
        ShowCurrentWorker();
        }
        else {
            Digger diggerscript =  Allworkerlist[val].GetComponent<Digger>();
            gameManager.UpdateCost(-diggerscript.Cost);
            Debug.Log(diggerscript.Name);
            GameObject ObjtoDestroy = Allworkerlist[val];
            Destroy(ObjtoDestroy);
            Allworkerlist.RemoveAt(val);
            CurWorker--;
            ShowCurrentWorker();
        }
    }
    private void Start() {
        spawnVisitor = GameObject.Find("VIsitorSpawn").GetComponent<SpawnVisitor>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GetRandomResume();
        ShowCurrentWorker();
    }
}
