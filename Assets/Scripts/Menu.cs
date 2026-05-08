using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadMainGame(){
       ///Debug.Log("1");s
        SceneManager.LoadScene("MainGame");
    }
    public void LoadMenu(){
        SceneManager.LoadScene("MainMenu");
    }
    public void ExitGame()
    {
        // Apply platform-specific exit methods
        #if UNITY_EDITOR
            // Exit play mode in the editor
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Quit the application on other platforms
            Application.Quit();
        #endif
    }
}
