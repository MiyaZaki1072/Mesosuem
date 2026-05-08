using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectionCage : MonoBehaviour
{
    public Sprite[] StageOfCurrent;
    public Button MainButton;
    public GameObject[] AllCage;
    public List<GameObject> AvailableCage; // Using a generic List<GameObject>
    public int CurrentIndex=0;
    public GameObject[] Curplace;
    public CollectionSystem collectionSystem;
    public int CurrentWant=0;
    public Transform child;
    public SpawnVisitor doors;
    public GameManager gameManager;
    public int WantSize;
    private void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        collectionSystem = GameObject.Find("CollectionManager").GetComponent<CollectionSystem>();
        doors = GameObject.Find("VIsitorSpawn").GetComponent<SpawnVisitor>();
    }
    public void OnClickOpen(int Size){
        WantSize = Size;
        if(WantSize == 0)WantSize =1;
        CurrentIndex = 0;
        int Want = collectionSystem.CurrentShow; 
        if(Want == 0 || Want == 1 || Want == 2)CurrentWant = 2;
        else CurrentWant =3;
        if(Curplace[Want] !=null){
            Item item = Curplace[Want].GetComponent<Item>();
            item.equipped = false;
            child = Curplace[Want].transform.GetChild(CurrentWant);
            child.gameObject.SetActive(false);
            gameManager.UpdatePOP(-WantSize);
        }
        AllCage = GameObject.FindGameObjectsWithTag("Object");
        AvailableCage.Clear();
        foreach (GameObject cage in AllCage)
        {
        Item item = cage.GetComponent<Item>();
        if (item != null)
        {
            if(item.isacage == Size && item.equipped == false){
                AvailableCage.Add(cage);
            }
            // Log the cost of the item
        }
        else
        {
            Debug.LogWarning("Item component not found on GameObject: " + cage.name);
        }
    }

    if(AvailableCage.Count == 0)return;
    Camera childCamera =AvailableCage[0].transform.GetChild(0).GetComponent<Camera>();
    childCamera.enabled = true;
    child = AvailableCage[0].transform.GetChild(CurrentWant);
    child.gameObject.SetActive(true);
    }
    public void Show(){
        Camera childCamera =AvailableCage[CurrentIndex].transform.GetChild(0).GetComponent<Camera>();
        childCamera.enabled = true;
        Transform child = AvailableCage[CurrentIndex].transform.GetChild(CurrentWant);
        child.gameObject.SetActive(true);
    }
    public void ChangeCurrent(int index){
        Camera childCamera =AvailableCage[CurrentIndex].transform.GetChild(0).GetComponent<Camera>();
        childCamera.enabled = false;
        child = AvailableCage[CurrentIndex].transform.GetChild(CurrentWant);
        child.gameObject.SetActive(false);
        CurrentIndex+=index;
        if(CurrentIndex >= AvailableCage.Count){
            CurrentIndex = 0;
        }
        if(CurrentIndex <0){
            CurrentIndex = AvailableCage.Count-1;
        }
        Show();
        child = AvailableCage[CurrentIndex].transform.GetChild(CurrentWant);
        child.gameObject.SetActive(true);
    }
    public void Exit(){
        Camera childCamera =AvailableCage[CurrentIndex].transform.GetChild(0).GetComponent<Camera>();
        childCamera.enabled = false;
        Item item = AvailableCage[CurrentIndex].GetComponent<Item>();
        item.equipped = true;
        Curplace[collectionSystem.CurrentShow] = AvailableCage[CurrentIndex];
        gameManager.UpdatePOP(0.5f);
    }
    public void Unequipped(){
        Camera childCamera =AvailableCage[CurrentIndex].transform.GetChild(0).GetComponent<Camera>();
        childCamera.enabled = false;
        child = AvailableCage[CurrentIndex].transform.GetChild(CurrentWant);
        child.gameObject.SetActive(false);
    }
}
