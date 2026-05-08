using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyingLandManager : MonoBehaviour
{
    public bool[,] OwnLand = new bool[3, 3];
    public GameManager gameManager;
    public GameObject Block;
    public MeshRenderer CurrentClaim;
    public Button[] Buttons;
    public QuestMaster questMaster;
    public int i,j;
    public void Start()
    {
        questMaster = GameObject.Find("QuestManager").GetComponent<QuestMaster>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        OwnLand[0,0] = true;
    }
    public void S10(){
        i=1;
        j=0;        
    }
    public void S20(){
        i=2;
        j=0;        
    }
    public void S01(){
        i=0;
        j=1;        
    }
    public void S02(){
        i=0;
        j=2;        
    }
    public void S11(){
        i=1;
        j=1;        
    }
    public void S21(){
        i=2;
        j=1;        
    }
    public void S22(){
        i=2;
        j=2;        
    }
    public void S12(){
        i=1;
        j=2;        
    }
    public void buyingLand(){
        if(gameManager.Money <=1000)return;
        if(i == 1 && j==0){
            if(OwnLand[2,0] || OwnLand[0,0] || OwnLand[1,1]){
                gameManager.UpdateMoney(-5000);
                ClaimLand(1,0);
                Buttons[1].gameObject.SetActive(false);
            }
        }
        if(i==2 && j==0){
            if(OwnLand[1,0] || OwnLand[2,1]){
                gameManager.UpdateMoney(-5000);
                ClaimLand(2,0);
                Buttons[0].gameObject.SetActive(false);
            }
        }
        if(i==0 && j==1){
            if(OwnLand[1,1] || OwnLand[0,0]){
                gameManager.UpdateMoney(-5000);
                ClaimLand(0,1);
                Buttons[4].gameObject.SetActive(false);
            }
        }
        if(i==1 && j==1){
            if(OwnLand[1,2] || OwnLand[0,1] || OwnLand[1,0] || OwnLand[2,1]){
                gameManager.UpdateMoney(-5000);
                ClaimLand(1,1);
                Buttons[3].gameObject.SetActive(false);
            }
        }
        if(i==2 && j==1){
            if(OwnLand[1,1] || OwnLand[2,0] || OwnLand[2,2]){
                gameManager.UpdateMoney(-5000);
                ClaimLand(2,1);
                Buttons[2].gameObject.SetActive(false);
            }
        }
        if(i==0 && j==2){
            if(OwnLand[0,1] || OwnLand[1,2]){
                gameManager.UpdateMoney(-5000);
                ClaimLand(0,2);
                Buttons[7].gameObject.SetActive(false);
            }
        }
        if(i==1 && j==2){
            if(OwnLand[1,1] || OwnLand[0,2] || OwnLand[2,2]){
                gameManager.UpdateMoney(-5000);
                ClaimLand(1,2);
                Buttons[6].gameObject.SetActive(false);
            }
        }
        if(i==2 && j==2){
            if(OwnLand[2,1] || OwnLand[1,2]){
                gameManager.UpdateMoney(-5000);
                ClaimLand(2,2);
                Buttons[5].gameObject.SetActive(false);
            }
        }
    }
    public void ClaimLand(int i,int j){
        questMaster.UpdateQuest(1,0);
        CurrentClaim = GameObject.Find(i.ToString()+","+j.ToString()).GetComponent<MeshRenderer>();
        CurrentClaim.enabled = true;
        Block = GameObject.Find("Block"+i.ToString()+","+j.ToString());
        Destroy(Block);
        gameManager.UpdateUtil(-1,0);
        OwnLand[i,j] = true;
    }
}
