using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

    public class OptionsMenu : MonoBehaviour
    {
        public void BackMenu(){
            SceneManager.LoadSceneAsync("Main Menu");
        }

        public void Statistics(){
            SceneManager.LoadSceneAsync("Statistics");
        }

    }

