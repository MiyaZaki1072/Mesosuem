using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private GameManager gameManager;

    [Header("Music")]
    public AudioClip AC_Music;
    private AudioSource musicSource;

    [Header("Sound Effects")]
    public AudioClip AC_Click;
    public AudioClip AC_BuyWorker; // Array for multiple sound effects
    public AudioClip AC_Laidoff;
    public AudioClip AC_Spin;
    public AudioClip AC_ClickItem;
    public AudioClip AC_Flipping;
    public AudioClip AC_Succesfull;
    private AudioSource soundEffectSource;

    private IEnumerator UpdateVolumeRoutine()
    {
        while (true)
        {
            SetMusicVolume();
            yield return new WaitForSeconds(0.25f); // Wait for 3 seconds
        }
    }
    private void Start()
    {  
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = AC_Music;
        musicSource.loop = true; // Set music to loop continuously
        musicSource.playOnAwake = true; // Play music automatically on start
        musicSource.Play();

        soundEffectSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(UpdateVolumeRoutine());
    }

    public void PlaySound(AudioClip soundEffect)
    {
        if (soundEffect != null) // Check for null reference
        {
            soundEffectSource.PlayOneShot(soundEffect);
        }
    }


     public void SetMusicVolume()
    {
        // Ensure volume is within a valid range (0 to 1)
        float volume = Mathf.Clamp01(gameManager.LevelMusic);
        musicSource.volume = volume;
        volume = Mathf.Clamp01(gameManager.LevelSound);
        soundEffectSource.volume = volume;
    }
    public void PlayClick()
    {
        soundEffectSource.PlayOneShot(AC_Click);
    }
    public void PlayWorker(int index)
    {
        if(index == 0){
        soundEffectSource.PlayOneShot(AC_BuyWorker);
        }
        if(index == 1){
            soundEffectSource.PlayOneShot(AC_Laidoff);
        }
        if(index == 2){
            soundEffectSource.PlayOneShot(AC_Spin);
        }
        if(index == 3){
            soundEffectSource.PlayOneShot(AC_ClickItem);
        }
        if(index==4){
            soundEffectSource.PlayOneShot(AC_Flipping);
        }
        if(index ==5 ){
            soundEffectSource.PlayOneShot(AC_Succesfull);
        }
    }
}
