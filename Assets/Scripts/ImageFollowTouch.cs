using UnityEngine;
using UnityEngine.UI;

public class ImageFollowTouchSimple : MonoBehaviour
{
    public Image CaptureCamera;
    private Vector2 touchOffset = Vector2.zero; // Stores the offset between touch position and initial image position
    public Image[] bones; // Array of bone images
    public int SuccesfullPercentage =0;
    public Research_Delve research_Delve;
    public GameObject SucessFull;
    public SoundManager soundManager;
    public Slider currentbar;
    private bool play=false;
    void Start()
    {
         soundManager = GameObject.Find("SoungManager").GetComponent<SoundManager>();
    research_Delve = GameObject.Find("ResearchManager").GetComponent<Research_Delve>();
        // Ensure CaptureCamera reference is set in the Inspector
        if (CaptureCamera == null)
        {
            Debug.LogError("CaptureCamera reference not set in Inspector!");
        }

        // Ensure bones array is assigned in the Inspector
        if (bones == null || bones.Length == 0)
        {
            Debug.LogError("bones array is empty or not assigned!");
        }
    }
    private void DisplayValueSlider(){
        ///currentbar.value = SuccesfullPercentage;
    }
    void Update()
    {
        if(SuccesfullPercentage == 100){
            if(!play)soundManager.PlayWorker(3);
            play=true;
            SucessFull.SetActive(true);
            if(research_Delve.DoneResearch){
            SucessFull.SetActive(false);
            string curisland = research_Delve.CurrentIsland;
            string curtype = research_Delve.CurrentTypeResearch;
            int curindex = research_Delve.index;
            if(curisland == "Grass"){
                research_Delve.ResearchGrassDone[curindex] = true;
                research_Delve.BackToMainMenu(); 
                research_Delve.GrassDino[curindex].SetActive(false);
                research_Delve.SelectPinImage.gameObject.SetActive(false);
            }
            if(curisland == "Desert"){
                research_Delve.ResearchDesertDone[curindex] = true;
                research_Delve.BackToMainMenu(); 
                research_Delve.DesDino[curindex].SetActive(false);
                research_Delve.SelectPinImage.gameObject.SetActive(false);
            }
            }
            return;
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Handle first touch only (assuming single-touch interaction)
            if (touch.phase == TouchPhase.Began)
            {
                touchOffset = touch.position - CaptureCamera.rectTransform.anchoredPosition;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                // Update image position based on touch delta
                CaptureCamera.rectTransform.anchoredPosition = touch.position - touchOffset;

                // Check for overlap with any bone image using RectTransformUtility
                Vector2 touchWorldPos = Camera.main.ScreenToWorldPoint(touch.position);
                bool collided = false;

                foreach (Image bone in bones)
                {
                    if (bone != null) // Check for null references
                    {
                        ///Debug.Log(CaptureCamera.rectTransform.anchoredPosition + "-"+ bone.rectTransform.anchoredPosition);
                        Rect boneRect = bone.GetComponent<RectTransform>().rect;
                        if (Mathf.Abs(CaptureCamera.rectTransform.anchoredPosition.x-bone.rectTransform.anchoredPosition.x) <=100 && Mathf.Abs(CaptureCamera.rectTransform.anchoredPosition.y-bone.rectTransform.anchoredPosition.y) <=100)
                        {
                            soundManager.PlayWorker(0);
                            SuccesfullPercentage+=20;
                            DisplayValueSlider();
                            Destroy(bone.gameObject);
                            collided = true;
                            break; // Exit loop after first collision (optional)
                        }
                    }
                }
                if (collided)
                {
                    Debug.Log(SuccesfullPercentage);
                }
            }
        }
    }
}