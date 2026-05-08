using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Selection : MonoBehaviour
{
    public GameObject selectedObject;
    public BuildingManager buildingManager;
    public GameManager gameManager;
    public GameObject NormalUI;
    public GameObject SelectUI;
    public GameObject AlrPayUI;
    private void Start() {
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void FixedUpdate()
    {
        ///Debug.Log(buildingManager.alrplace);
        if(Input.touchCount >0 && buildingManager.building == true){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit ,1000)){
                if(hit.collider.gameObject.CompareTag("Object") && buildingManager.alrplace == true){
                    if(selectedObject == hit.collider.gameObject)return;
                    if(selectedObject!=null)Deselect();
                    Select(hit.collider.gameObject);
                }
            }
        }
    }
    private void Select(GameObject obj){
        if(obj == selectedObject)return;
        Outline outline = obj.GetComponent<Outline>();
        if(outline == null)obj.AddComponent<Outline>();
        else outline.enabled = true;
        selectedObject = obj;
        SelectUI.SetActive(true);
        NormalUI.SetActive(false);
        AlrPayUI.SetActive(false);
    }
    public void Deselect(){
        if(selectedObject == null)return;
        selectedObject.GetComponent<Outline>().enabled = false;
        selectedObject=null;
        SelectUI.SetActive(false);
        NormalUI.SetActive(true);
        AlrPayUI.SetActive(false);
    }
    public void Move(){
        buildingManager.pendingObject = selectedObject;
        buildingManager.canPlace = true;
        buildingManager.alrplace = false;
        Deselect();
        SelectUI.SetActive(false);
        NormalUI.SetActive(false);
        AlrPayUI.SetActive(true);
    }
    public void Delete(){
        GameObject objTodestroy = selectedObject;
        if(objTodestroy.CompareTag("Object")){
            Item itemInfo = selectedObject.GetComponent<Item>();
            gameManager.UpdateCost(-itemInfo.cost);
            gameManager.UpdateMoney(itemInfo.price*7/10);
            if (itemInfo.isacage == 0)
            {
                buildingManager.SCage.Remove(objTodestroy);
                buildingManager.CountCageS-=1;
            }
            else if (itemInfo.isacage == 1)
            {
                buildingManager.MCage.Remove(objTodestroy);
                buildingManager.CountCageM-=1;
            }
            else if (itemInfo.isacage == 2)
            {
                buildingManager.LCage.Remove(objTodestroy);
                buildingManager.CountCageL-=1;
            }
            if(itemInfo.decvalue >0)gameManager.UpdateDec(-itemInfo.decvalue);
            else gameManager.UpdateUtil(itemInfo.decvalue,-1);
        }
        Deselect();
        Destroy(objTodestroy);
    }
}
