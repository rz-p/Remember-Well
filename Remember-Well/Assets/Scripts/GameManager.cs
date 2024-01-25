using UnityEngine;
using System.Collections.Generic; // Needed for using List

public class GameManager : MonoBehaviour
{
    public GameObject gridCellPrefab; // Prefab for the grid cells or cards
    public Sprite[] cardFrontSprites; // Array of sprites for the card fronts
    public GameObject cardParent;

    public void InitializeGame()
    {
        // Access the difficulty and grid size from DifficultyController
        DifficultyController.Difficulty difficulty = DifficultyController.selectedDifficulty;
        int gridWidth = DifficultyController.gridWidth;
        int gridHeight = DifficultyController.gridHeight;
        int numPairs = (gridWidth * gridHeight) / 2;
        // Shuffle or arrange the card front sprites before assigning them to cards
        List<Sprite> shuffledSprites = ShuffleSprites(cardFrontSprites, numPairs);

        // Instantiate the grid based on the difficulty
        CreateGrid(gridWidth, gridHeight, shuffledSprites);
    }


    private void CreateGrid(int width, int height, List<Sprite> shuffledSprites)
    {
        float cardSpacing = 2.0f; // Adjust this value to control the space between cards
        int spriteIndex = 0; // Initialize sprite index

        // Calculate the total size of the grid
        float totalWidth = width * (1 + cardSpacing) - cardSpacing;
        float totalHeight = height * (1 + cardSpacing) - cardSpacing;

        // Set the position of the card parent to center the grid
        cardParent.transform.position = new Vector3(-totalWidth / 2, -totalHeight / 2, 0);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Calculate position with spacing
                Vector3 position = new Vector3(x * (1 + cardSpacing), y * (1 + cardSpacing), 0);

                GameObject newCard = Instantiate(gridCellPrefab, position, Quaternion.identity);
                newCard.transform.SetParent(cardParent.transform, false); // Set the new card's parent

                CardManager cardManager = newCard.GetComponent<CardManager>();
                if (cardManager != null && spriteIndex < shuffledSprites.Count)
                {
                    cardManager.SetFrontImage(shuffledSprites[spriteIndex]);
                    spriteIndex++;
                }
            }
        }
    }
    private List<Sprite> ShuffleSprites(Sprite[] sprites, int numPairs)
{
    List<Sprite> selectedSprites = new List<Sprite>();
    HashSet<int> chosenIndexes = new HashSet<int>();
    System.Random rng = new System.Random();

    // Randomly select unique sprites
    while (selectedSprites.Count < numPairs)
    {
        int randomIndex = rng.Next(sprites.Length);
        if (!chosenIndexes.Contains(randomIndex))
        {
            selectedSprites.Add(sprites[randomIndex]);
            chosenIndexes.Add(randomIndex);
        }
    }

    // Duplicate each sprite to create pairs
    List<Sprite> pairedList = new List<Sprite>(selectedSprites);
    pairedList.AddRange(selectedSprites);

    // Shuffle the list
    int n = pairedList.Count;
    while (n > 1)
    {
        n--;
        int k = rng.Next(n + 1);
        Sprite value = pairedList[k];
        pairedList[k] = pairedList[n];
        pairedList[n] = value;
    }

    return pairedList;
}



}
