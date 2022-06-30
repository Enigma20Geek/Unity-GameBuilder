using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayUIController : MonoBehaviour{
    
    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void HomeScreen(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }
}
