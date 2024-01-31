using UnityEngine;
using UnityEngine.UI; // For UI components
using TMPro; // Include this for TextMeshPro elements

public class DifficultyController : MonoBehaviour
{
    public enum Difficulty
    {
        Normal,
        Hard,
        Custom
    }

    public enum CardTheme
    {
        Animals,
        Plants
    }

    public static CardTheme selectedTheme;


    public GameObject fieldsObject; // Assign this in the inspector

    public GameObject gameSetupPanel;

    public GameObject playButton;

    // Button references
    public Button normalButton;
    public Button hardButton;
    public Button customButton;
    // Theme button references
    public Button animalsButton;
    public Button plantsButton;

    private bool isDifficultySelected = false;
    private bool isThemeSelected = false;

    public static Difficulty selectedDifficulty;
    public static int gridWidth = 4; // Default value
    public static int gridHeight = 4; // Default value

    // Reference to the TMP Dropdowns
    public TMP_Dropdown widthDropdown;
    public TMP_Dropdown heightDropdown;

    public void SetNormalDifficulty()
    {
        selectedDifficulty = Difficulty.Normal;
        gridHeight = 4;
        gridWidth = 4;
        isDifficultySelected = true;
        UpdateButtonColors();
    }

    public void SetHardDifficulty()
    {
        selectedDifficulty = Difficulty.Hard;
        gridHeight = 6;
        gridWidth = 6;
        isDifficultySelected = true;
        UpdateButtonColors();
    }

    public void SetCustomDifficulty()
    {
        // Assuming the dropdown values correspond to actual grid sizes
        gridWidth = (widthDropdown.value + 1) * 2; // +1 if dropdown index starts at 0
        gridHeight = (heightDropdown.value + 1) * 2; // +1 if dropdown index starts at 0
        selectedDifficulty = Difficulty.Custom;
        isDifficultySelected = true;
        UpdateButtonColors();
    }

    public void SetThemeToAnimals()
    {
        selectedTheme = CardTheme.Animals;
        isThemeSelected = true;
        UpdateThemeButtonColors();
        UpdatePlayButtonStatus();
    }

    public void SetThemeToPlants()
    {
        selectedTheme = CardTheme.Plants;
        isThemeSelected = true;
        UpdateThemeButtonColors();
        UpdatePlayButtonStatus();
    }

    private void UpdateButtonColors()
    {
        // Reset all buttons to default color
        Color defaultColor = Color.white; // Change this to your default color
        normalButton.GetComponent<Image>().color = defaultColor;
        hardButton.GetComponent<Image>().color = defaultColor;
        customButton.GetComponent<Image>().color = defaultColor;

        // Highlight the selected button
        switch (selectedDifficulty)
        {
            case Difficulty.Normal:
                normalButton.GetComponent<Image>().color = Color.green;
                break;
            case Difficulty.Hard:
                hardButton.GetComponent<Image>().color = Color.green;
                break;
            case Difficulty.Custom:
                customButton.GetComponent<Image>().color = Color.green;
                break;
        }
        if (isThemeSelected)
        {
            UpdateThemeButtonColors();
        }

        UpdatePlayButtonStatus();
        fieldsObject.SetActive(false);
        Debug.Log("Selected Difficulty: " + selectedDifficulty);
        Debug.Log("Grid Size: " + gridWidth + " by " + gridHeight);
        if (isThemeSelected && isDifficultySelected)
        {
            playButton.SetActive(true);
        }
    }

    private void UpdateThemeButtonColors()
    {
        Color defaultColor = Color.white; // Your default color
        Color selectedColor = Color.green; // Color for selected button

        animalsButton.GetComponent<Image>().color = (selectedTheme == CardTheme.Animals) ? selectedColor : defaultColor;
        plantsButton.GetComponent<Image>().color = (selectedTheme == CardTheme.Plants) ? selectedColor : defaultColor;
    }

    private void UpdatePlayButtonStatus()
    {
        playButton.SetActive(isDifficultySelected && isThemeSelected);
    }

    public void ShowFields()
    {
        fieldsObject.SetActive(true);
    }

    public void StartGame()
    {
        // Deactivate the game setup UI panel
        if (gameSetupPanel != null)
        {
            gameSetupPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("Game setup panel reference not set.");
        }

        // Initialize the game
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.InitializeGame();
            gameManager.StartStopwatch();
        }
        else
        {
            Debug.LogError("GameManager not found in the scene.");
        }
    }


}
