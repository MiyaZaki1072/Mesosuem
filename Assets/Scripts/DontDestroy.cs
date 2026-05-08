using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DontDestroy : MonoBehaviour
{
    public Slider Sound;
    public Slider Music;
    public GameObject gameObject;
    public float LevelMusic;
    public float LevelSound;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);   
    }
    void Update(){
        LevelMusic = Music.value;
        LevelSound = Sound.value;
    }
}
