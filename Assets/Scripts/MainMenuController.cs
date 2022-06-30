using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour{
    
    public void PlayGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void QuitGame(){
        Debug.Log("Quit");
        Application.Quit();
    }

    public void ToggleMusic(){
        Sound[] sounds = Array.FindAll(AudioManager.instance.sounds,item => item.loop == true);
        foreach (Sound s in sounds){
            s.source.mute = !s.source.mute;
        }
    }

    public void ToggleEffects(){
        Sound[] sounds = Array.FindAll(AudioManager.instance.sounds,item => item.loop == false);
        foreach (Sound s in sounds){
            s.source.mute = !s.source.mute;
        }
    }
}
