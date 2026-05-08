using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class BuildingManager : MonoBehaviour
{
    public GameObject[] objects;
    public GameManager gameManager;
    public GameObject pendingObject;
    public Selection SelectingScript;
    [SerializeField]private Material[] materials;
    public Vector3 pos;
    private RaycastHit hit;
    [SerializeField]private LayerMask layerMask;
    [SerializeField]private LayerMask UIMASK;
    public Button placebutton;
    public GameObject GroundLevel;
    public float rotateAmount=45;
    public bool canPlace = true;
    public bool alrplace =true;
    public int current;
    public int currentcost;
    public GameObject NormalUI;
    public GameObject SelectUI;
    public GameObject AlrPayUI;
    public List<GameObject> LCage = new List<GameObject>();
    public List<GameObject> MCage = new List<GameObject>();
    public List<GameObject> SCage = new List<GameObject>();
    public int CountCageL=0,CountCageM=0,CountCageS=0;
    public bool building= false;
    // Update is called once per frame
    private void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        SelectingScript = GameObject.Find("Selection").GetComponent<Selection>();
    }
    public void NowBuilding(){
        building = true;
    }
    public void ExitBuilding(){
        SelectingScript.Deselect();
        building =false;
    }
    public void Update()
    {
        if(pendingObject != null){
            pendingObject.transform.position = pos;
            UpdateMaterials();
        }
    }
    public void PlaceObject(){
        if(pendingObject == null)return;
        else if(canPlace && gameManager.Money >= current){
            gameManager.UpdateMoney(-current);
            gameManager.UpdateCost(currentcost);
            pendingObject.GetComponent<MeshRenderer>().material = materials[2];
            Item itemInfo = pendingObject.GetComponent<Item>();
            if(itemInfo.isacage!=-1){
                Debug.Log("register");
                if (itemInfo.isacage == 0)
                {
                    SCage.Add(pendingObject); // Add to the SCage list
                    CountCageS++;
                }
                else if (itemInfo.isacage == 1)
                {
                    MCage.Add(pendingObject); // Add to the MCage list
                    CountCageM++;
                }
                else if (itemInfo.isacage == 2)
                {
                    LCage.Add(pendingObject); // Add to the LCage list
                    CountCageL++;
                }
            }
            if(itemInfo.decvalue <0)gameManager.UpdateUtil(itemInfo.decvalue,1);
            else gameManager.UpdateDec(itemInfo.decvalue);
            pendingObject = null;
            canPlace=true;
            alrplace=true;
        }
    }
    public void AlrPlaceObject(){
        if(pendingObject == null)return;
        if(canPlace){
            pendingObject.GetComponent<MeshRenderer>().material = materials[2];
            pendingObject = null;
            canPlace=true;
            alrplace=true;
        }
    }
    void UpdateMaterials(){
        if(canPlace){
            pendingObject.GetComponent<MeshRenderer>().material = materials[0];
        }
        else pendingObject.GetComponent<MeshRenderer>().material = materials[1];
    }
    private void FixedUpdate() {
        pos = GroundLevel.transform.position;
    }
    public void SelectObject(int index){
        SelectingScript.Deselect();
        SelectUI.SetActive(false);
        NormalUI.SetActive(true);
        AlrPayUI.SetActive(false);
        if(pendingObject != null)return;
        ///pendingObject = null;
        pendingObject =  Instantiate(objects[index],pos,transform.rotation);
        Item itemInfo = pendingObject.GetComponent<Item>();
        materials[2] = pendingObject.GetComponent<MeshRenderer>().material;
        alrplace = false;
        current = itemInfo.price;
        currentcost = itemInfo.cost;
    }
    public void RotateObject(){
        if(pendingObject !=null){
        pendingObject.transform.Rotate(Vector3.up,rotateAmount);
        }
    }
    public void Delete(){
        if(pendingObject != null){
        GameObject objTodestroy = pendingObject;
        Destroy(objTodestroy);
        pendingObject = null;
        alrplace=true;
        }
    }
}
