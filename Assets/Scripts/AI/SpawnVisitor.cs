using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnVisitor : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Visitor;
    public GameObject Cleaner;
    public GameObject Digger;
    public Transform Spawner;
    public GameManager gameManager;
    public BuildingManager buildingManager;
    public int cnt=0;
    public int MaxVisitor=0;
    public QuestMaster questMaster;
    public void Spawn(){
        if(cnt <MaxVisitor && buildingManager.building == false){
            cnt++;
            questMaster.UpdateQuest(2,0);
            gameManager.UpdateMoney(5);
            Vector3 SpawningArea = new Vector3(Spawner.position.x+Random.Range(0,3),Spawner.position.y,Spawner.position.z+Random.Range(0,3));
            GameObject newObject = Instantiate(Visitor,SpawningArea,transform.rotation);
            newObject.transform.Rotate(0f, 180f, 0f);
        }
    }
    public void DestroyAllVisitors()
    {
        GameObject[] visitorObjects = GameObject.FindGameObjectsWithTag("Visitor");
        foreach (GameObject visitor in visitorObjects)
        {
            cnt--;
            Destroy(visitor);
        }
    }
    private void Update() {
        if(buildingManager.building  == true){
            DestroyAllVisitors();
        }
    }
    public void SpawnCleaner(int val,int cost,string name){
        GameObject newCleaner = Instantiate(Cleaner,Spawner.position,transform.rotation);
        Cleaner cleanerScript = newCleaner.GetComponent<Cleaner>();
        cleanerScript.WorkSpeed = val; 
        cleanerScript.Cost = cost;
        cleanerScript.Name = name;
    }
    public void SpawnDigger(int val,int cost,string name){
        GameObject newDigger = Instantiate(Digger,Spawner.position,transform.rotation);
        Digger cleanerScript = newDigger.GetComponent<Digger>();
        cleanerScript.WorkSpeed = val; 
        cleanerScript.Cost = cost;
        cleanerScript.Name = name;
    }
    private IEnumerator UpdateEvery10Seconds()
    {
        while (true)
        {
            int Overall = gameManager.Cleaness + gameManager.Popularity;
            if(buildingManager.CountCageL + buildingManager.CountCageM + buildingManager.CountCageS ==0){
                MaxVisitor=0;
            }
            else{
                if(Overall == 5)MaxVisitor = 15;
                if(Overall == 4)MaxVisitor = 14;
                if(Overall == 3)MaxVisitor = 10;
                if(Overall == 2)MaxVisitor = 7;
                if(Overall == 1)MaxVisitor = 5;
                if(Overall == 0)MaxVisitor = 3;
                Spawn();
            }
            // Your code to be executed every 10 seconds goes here
            ///Debug.Log("This message appears every 10 seconds");
            float timewait = Random.Range(1,10-Overall);
            yield return new WaitForSeconds(timewait); // Wait for 10 seconds
        }
    }
    void Start()
    {
        questMaster = GameObject.Find("QuestManager").GetComponent<QuestMaster>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
        StartCoroutine(UpdateEvery10Seconds());
    }
}
