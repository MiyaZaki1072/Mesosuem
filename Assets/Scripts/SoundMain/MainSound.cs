using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSound : MonoBehaviour
{
    public static MainSound instance;
    private DontDestroy gameManager;

    [Header("Music")]
    public AudioClip AC_Music;
    private AudioSource musicSource;

    [Header("Sound Effects")]
    public AudioClip AC_Click;
    public AudioClip[] otherSoundEffects; // Array for multiple sound effects
    private AudioSource soundEffectSource;
    private IEnumerator UpdateVolumeRoutine()
    {
        while (true)
        {
            SetMusicVolume();
            yield return new WaitForSeconds(1f); // Wait for 3 seconds
        }
    }
    private void Start()
    {
        gameManager = GameObject.Find("SettingVal").GetComponent<DontDestroy>();  
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
    public void PlayClick()
    {
        soundEffectSource.PlayOneShot(AC_Click);
    }
    public void PlaySoundEffect(int index)
    {
        if (index >= 0 && index < otherSoundEffects.Length) // Check for valid index
        {
            soundEffectSource.PlayOneShot(otherSoundEffects[index]);
        }
        else
        {
            Debug.LogError("SoundManager: Invalid sound effect index provided!");
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
}
