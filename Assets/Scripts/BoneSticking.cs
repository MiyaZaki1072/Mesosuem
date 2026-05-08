using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BoneSticking : MonoBehaviour
{
    public Button CurrentBone;
    public Button[] AllTheBone;
    public Image[] AllTheHoles;
    public GameObject SuccesFull;
    public int Cur=0;
    public int Count=0;
    private Vector2 touchOffset = Vector2.zero;
    private CollectionSystem collectionSystem;
    public SoundManager soundManager;
    private bool play=false;
    private void Start() {
        collectionSystem = GameObject.Find("CollectionManager").GetComponent<CollectionSystem>();
        soundManager = GameObject.Find("SoungManager").GetComponent<SoundManager>();
    }
    public void Select(int index){
        if(CurrentBone != null){
            AllTheBone[Cur].interactable = true;
        }
        CurrentBone = AllTheBone[index];
        AllTheBone[index].interactable = false;
        Cur =index ;
    }
    private void Update() {
        if(Count == 3){
            SuccesFull.SetActive(true);
            if(!play)soundManager.PlayWorker(3);
            play=true;
        }
        if(CurrentBone != null){
            if (Input.touchCount > 0)
            {
            Touch touch = Input.GetTouch(0);

            // Handle first touch only (assuming single-touch interaction)
            if (touch.phase == TouchPhase.Began)
            {
                touchOffset = touch.position - CurrentBone.image.rectTransform.anchoredPosition;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                // Update image position based on touch delta
                CurrentBone.image.rectTransform.anchoredPosition = touch.position - touchOffset;

                // Check for overlap with any bone image using RectTransformUtility
                Vector2 touchWorldPos = Camera.main.ScreenToWorldPoint(touch.position);
            }
        }
        if (Mathf.Abs(CurrentBone.image.rectTransform.anchoredPosition.x-AllTheHoles[Cur].rectTransform.anchoredPosition.x) <=100 && Mathf.Abs(CurrentBone.image.rectTransform.anchoredPosition.y-AllTheHoles[Cur].rectTransform.anchoredPosition.y) <=100){
            CurrentBone.image.rectTransform.anchoredPosition = AllTheHoles[Cur].rectTransform.anchoredPosition;
            CurrentBone = null;
            AllTheBone[Cur].interactable = false;
            Count++;
            soundManager.PlayWorker(0);
            Debug.Log(Count);
        }
        
        }
    }
}
