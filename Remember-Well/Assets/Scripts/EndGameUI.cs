using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameUI : MonoBehaviour
{
    public void PlayAgain()
    {
        // Reload the current game scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene("Main Menu"); // Replace "MainMenu" with your main menu scene name
    }
}
