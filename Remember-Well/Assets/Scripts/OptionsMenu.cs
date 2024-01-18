using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static PreviousScene;

public class OptionsMenu : MonoBehaviour
{
    public void Back(){
        SceneManager.LoadSceneAsync(PreviousScene.SceneName);
    }

    public void Statistics(){
        SceneManager.LoadSceneAsync("Statistics");
    }

}
