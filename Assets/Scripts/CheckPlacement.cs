using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlacement : MonoBehaviour
{
    public BuildingManager buildingManager;
    public LayerMask groundLayer; // Layer mask for ground
    void Start()
    {
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
    }
    private void OnTriggerStay(Collider other) {
        if(other.gameObject.CompareTag("Object") || other.gameObject.CompareTag("Block")){
            ///Debug.Log("false");
            buildingManager.canPlace = false;
        }
    }
    private void OnTriggerExit(Collider other) {
        Debug.Log("exit");
           if(other.gameObject.CompareTag("Object") || other.gameObject.CompareTag("Block")){
            Debug.Log("true");
            buildingManager.canPlace = true;
        }
    }
}
