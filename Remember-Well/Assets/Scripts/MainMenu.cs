using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame(){
        SceneManager.LoadSceneAsync("GameScene");
    }

    public void Options(){
        SceneManager.LoadSceneAsync("Options");
    }

    public void QuitGame(){
        Application.Quit();
    }
}
