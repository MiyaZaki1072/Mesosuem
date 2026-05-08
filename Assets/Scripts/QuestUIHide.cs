using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUIHide : MonoBehaviour
{
    public bool current =false;
    public GameObject questmenu;
    private Vector3 hiding = new Vector3(1232f,50.164f,0);
    private Vector3 showing = new Vector3(620.3276f,48.2556f,0);
    public SoundManager soundManager;
    private void Awake() {
        soundManager = GameObject.Find("SoungManager").GetComponent<SoundManager>();
    }
    public void OnClick(){
        current = !current;
        soundManager.PlayWorker(3);
        if(current){
            Debug.Log("asd");
            questmenu.transform.localPosition = hiding;
        }
        else{
            Debug.Log("wed");
            questmenu.transform.localPosition = showing;
        }
    }
}
