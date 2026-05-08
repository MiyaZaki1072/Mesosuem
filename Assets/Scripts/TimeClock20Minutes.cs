using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // If using TextMesh Pro

public class TimeClock20Minutes : MonoBehaviour
{
    public TextMeshProUGUI clockText; // Replace with your text object reference
    private float lastUpdateTime;
    public int Day=0;
    public int StartMonthMoney;
    public GameManager gameManager;
    public GameObject MonthReport;
    public TextMeshProUGUI EarnTEXT;
    public TextMeshProUGUI CostTEXT;
    public TextMeshProUGUI ProfitTEXT;
    public QuestMaster questMaster;
    void Start()
    {
        questMaster = GameObject.Find("QuestManager").GetComponent<QuestMaster>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        lastUpdateTime = Time.time;
        StartMonthMoney = gameManager.Money;
        UpdateTime();
    }
    public void UpdateTime(){
        clockText.text = Day.ToString();
    }
    void Update()
    {
        float currentTime = Time.time;
        float timeDiff = currentTime - lastUpdateTime;
        questMaster.UpdateQuest(3,timeDiff);
        if (timeDiff >= 20f*60f) // Check if 20 minutes have passed
        {
            Day++;
            if(Day==31){
                MonthReport.SetActive(true);
                int Earn = gameManager.Money-StartMonthMoney;
                int Profit =Earn-gameManager.Cost;
                Day=0;
                ProfitTEXT.text = (Profit).ToString();
                EarnTEXT.text = (Earn).ToString();
                CostTEXT.text = (gameManager.Cost).ToString();
                GameManager.Instance.UpdateMoney(-GameManager.Instance.Cost);
                Debug.Log(Earn);
                StartMonthMoney = gameManager.Money;
            }
            else{
                UpdateTime();
            }
            lastUpdateTime = Time.time;
        }
    }
}

