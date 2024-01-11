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

    public GameObject fieldsObject; // Assign this in the inspector

    // Button references
    public Button normalButton;
    public Button hardButton;
    public Button customButton;

    public static Difficulty selectedDifficulty;
    public static int customGridWidth = 4; // Default value
    public static int customGridHeight = 4; // Default value

    // Reference to the TMP InputFields
    public TMP_InputField customWidthInputField;
    public TMP_InputField customHeightInputField;

    public void SetNormalDifficulty()
    {
        selectedDifficulty = Difficulty.Normal;
        UpdateButtonColors();
    }

    public void SetHardDifficulty()
    {
        selectedDifficulty = Difficulty.Hard;
        UpdateButtonColors();
    }

    public void SetCustomDifficulty()
    {
        // Validate and set the custom difficulty
        if (int.TryParse(customWidthInputField.text, out int width) &&
            int.TryParse(customHeightInputField.text, out int height))
        {
            customGridWidth = Mathf.Clamp(width, 1, 6); // Clamping to desired range
            customGridHeight = Mathf.Clamp(height, 1, 6);
            selectedDifficulty = Difficulty.Custom;
            UpdateButtonColors();
        }
        else
        {
            Debug.LogError("Invalid input for custom grid size.");
        }
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
        fieldsObject.SetActive(false);
        Debug.Log("Selected Difficulty: " + selectedDifficulty);
    }

    public void ShowFields()
    {
        fieldsObject.SetActive(true);
    }

    public void UpdateCustomWidth(string widthInput)
    {
        if (int.TryParse(widthInput, out int width))
        {
            customGridWidth = Mathf.Clamp(width, 1, 6);
        }
    }

    public void UpdateCustomHeight(string heightInput)
    {
        if (int.TryParse(heightInput, out int height))
        {
            customGridHeight = Mathf.Clamp(height, 1, 6);
        }
    }

    void StartGame()
    {
        switch (DifficultyController.selectedDifficulty)
        {
            case Difficulty.Normal:
                // Set up a 4x4 grid
                break;
            case Difficulty.Hard:
                // Set up a 6x6 grid
                break;
            case Difficulty.Custom:
                // Set up a grid with customGridWidth x customGridHeight
                break;
        }

        // Additional game setup code
    }


}
