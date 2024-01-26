using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreviousScene : MonoBehaviour
{
    public static string SceneName { get; private set; }
    private void OnDestroy()
    {
        SceneName = gameObject.scene.name;
    }
    
    private void Start()
    {
        Debug.Log(PreviousScene.SceneName);
    }
}
