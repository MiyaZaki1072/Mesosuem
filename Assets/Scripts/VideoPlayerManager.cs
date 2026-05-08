using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
public class VideoPlayerManager : MonoBehaviour
{
    public VideoClip[] videoClips; // Array of video clips for cutscenes
    private int currentClipIndex = 0; // Index of the currently playing clip
    private VideoPlayer videoPlayer; // Reference to the Video Player component
    public bool isPlaying = false; // Flag to track playback state
    public GameObject Screen;
    public Image RawIMG;
    public GameManager gameManager;
    public float SaveMusic;
    public float SaveFPX;
    public int cnt;
    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }
    private void Start(){
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        PlayCutscene(0);
    }
    IEnumerator DelayedPlayCutscene()
    {
        yield return new WaitForSeconds(2f);
        videoPlayer.clip = null;
        videoPlayer.Stop();
        isPlaying = false;
        videoPlayer.loopPointReached -= OnVideoEnd; // Unregister event
    }
    public void PlayCutscene(int clipIndex)
    {
        cnt++;
        gameManager.ChangeDuring(true);
        SaveMusic = gameManager.LevelMusic;
        SaveFPX = gameManager.LevelSound;
        gameManager.ChangeLVLSOUND(0,0);
        Screen.gameObject.SetActive(true);
        if (videoClips != null && clipIndex >= 0 && clipIndex < videoClips.Length && !isPlaying)
        {
            currentClipIndex = clipIndex;
            videoPlayer.clip = videoClips[currentClipIndex];
            videoPlayer.Prepare(); // Prepare the video for playback
            ///DelayedPlayCutscene();
            videoPlayer.loopPointReached += OnVideoEnd; // Register for loop point event
            videoPlayer.Play();
            ///DelayedPlayCutscene();
            ///RawIMG.image.gameObject.SetActive(true);
            isPlaying = true;
            // Optional: Disable player input and manage UI elements during cutscene
            //...
        }
        else
        {
            Debug.LogError("CutsceneManager: Invalid clip index or already playing!");
        }
    }
    void ResetVid(){
            cnt--;
            videoPlayer.clip = videoClips[9];
            videoPlayer.Prepare(); // Prepare the video for playback
            ///DelayedPlayCutscene();
            videoPlayer.loopPointReached += OnVideoEnd; // Register for loop point event
            videoPlayer.Play();
            isPlaying=true;
            ///DelayedPlayCutscene();
            ///RawIMG.image.gameObject.SetActive(true);
            //isPlaying = true;
            // Optional: Disable player input and manage UI elements during cutscene
            //...
    }
    private void Update() {
        StopCutscene();
    }
    public void StopCutscene()
    {
        if (videoPlayer != null && !isPlaying)
        {
            videoPlayer.clip = null;
            videoPlayer.Stop();
            isPlaying = false;
            videoPlayer.loopPointReached -= OnVideoEnd; // Unregister event
            Screen.gameObject.SetActive(false);
            gameManager.ChangeDuring(false);
            if(cnt >0)ResetVid();
            ///RawIMG.gameObject.SetActive(false);
            ///RawIMG.color = Color.black;
            // Optional: Re-enable player input and UI elements after cutscene
            //...
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        // Handle end of playback (optional: advance to next clip, loop, etc.)
        isPlaying = false;
        // You can implement logic here to play the next clip in sequence, loop, etc.
    }
}