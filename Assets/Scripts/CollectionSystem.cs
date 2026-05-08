using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CollectionSystem : MonoBehaviour
{
    public Sprite[] Dinosaur;
    public Sprite[] Skeleton;
    public string[] DinoName;
    public GameObject[] AllTheCollection;
    public GameObject CannotEnter;
    public Button SkeletonButton;
    public Image Dinosaur_Image;
    public TextMeshProUGUI Dinosaur_Name_Text;
    public Research_Delve research_Delve;
    public int CurrentShow=0;
    public bool[] JigsawDone;
    public SelectionCage selectionCage;
    public GameObject CollectionSelf;
    public GameObject OverViewCanva;
    public SoundManager soundManager;
    
    void Start()
    {
        CurrentShow=0;
        research_Delve = GameObject.Find("ResearchManager").GetComponent<Research_Delve>();
        selectionCage = GameObject.Find("OverviewManager").GetComponent<SelectionCage>();
        soundManager = GameObject.Find("SoungManager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentShow <0 || CurrentShow >5)return;
        if(CurrentShow <3 ){
            if(!research_Delve.ResearchGrassDone[CurrentShow]){
                Dinosaur_Image.color = Color.gray;
            }
            else Dinosaur_Image.color = Color.white;
            if(!research_Delve.DelveGrassDone[CurrentShow])SkeletonButton.GetComponent<Image>().color = Color.gray;
            else if(research_Delve.DelveGrassDone[CurrentShow ]&& !JigsawDone[CurrentShow])SkeletonButton.GetComponent<Image>().color = Color.yellow;
            else SkeletonButton.GetComponent<Image>().color = Color.white;
        }
        if(CurrentShow >=3){
            if(!research_Delve.ResearchDesertDone[CurrentShow-3]){
                Dinosaur_Image.color = Color.gray;
            }
            else Dinosaur_Image.color = Color.white;
            if(!research_Delve.DelveDesertDone[CurrentShow-3])SkeletonButton.GetComponent<Image>().color = Color.gray;
            else if(research_Delve.DelveDesertDone[CurrentShow-3] && !JigsawDone[CurrentShow])SkeletonButton.GetComponent<Image>().color = Color.yellow;
            else SkeletonButton.GetComponent<Image>().color = Color.white;
        }
        Dinosaur_Name_Text.text = DinoName[CurrentShow];
        SkeletonButton.image.sprite = Skeleton[CurrentShow];
        Dinosaur_Image.sprite = Dinosaur[CurrentShow];
    }
    public void OnClick(int index){
        soundManager.PlayWorker(4);
        CurrentShow +=index;
        if(CurrentShow ==-1)CurrentShow=5;
        if(CurrentShow == 6)CurrentShow=0;
    }
    public void OnClickEnter(){
        soundManager.PlayWorker(1);
        if(JigsawDone[CurrentShow])return;
        if(CurrentShow <3 ){
            if(research_Delve.DelveGrassDone[CurrentShow]){
                AllTheCollection[CurrentShow].SetActive(true);
            }
            else CannotEnter.SetActive(true);
        }
        if(CurrentShow >=3){
            if(research_Delve.DelveDesertDone[CurrentShow-3]){
                AllTheCollection[CurrentShow].SetActive(true);
            }
            else CannotEnter.SetActive(true);
        }
    }
    public void CurrentDone(){
        AllTheCollection[CurrentShow].SetActive(false);
        JigsawDone[CurrentShow] = true;
    }
    public void OnClickEnterEquipped(){
        Debug.Log(JigsawDone[CurrentShow]);
        if(JigsawDone[CurrentShow]){
            if(CurrentShow <3 ){
            if(research_Delve.ResearchGrassDone[CurrentShow]){
                if(CurrentShow == 0)selectionCage.OnClickOpen(1);
                if(CurrentShow == 1)selectionCage.OnClickOpen(0);
                if(CurrentShow == 2)selectionCage.OnClickOpen(2);
                if(selectionCage.AvailableCage.Count == 0){
                    CannotEnter.SetActive(true);
                    return;
                }
                OverViewCanva.SetActive(true);
                CollectionSelf.SetActive(false);
            }
            else CannotEnter.SetActive(true);
        }
        if(CurrentShow >=3){
            if(research_Delve.ResearchDesertDone[CurrentShow-3]){
                if(CurrentShow == 3)selectionCage.OnClickOpen(1);
                if(CurrentShow == 4)selectionCage.OnClickOpen(2);
                if(CurrentShow == 5)selectionCage.OnClickOpen(0);
                if(selectionCage.AvailableCage.Count == 0){
                    CannotEnter.SetActive(true);
                    return;
                }
                CollectionSelf.SetActive(false);
                OverViewCanva.SetActive(true);
            }
            else CannotEnter.SetActive(true);
        }
        }
        else CannotEnter.SetActive(true);
    }
}
