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

        // Instantiate the grid based on the difficulty
        CreateGrid(gridWidth, gridHeight);
    }

    private void CreateGrid(int width, int height)
    {
        // Shuffle or arrange the card front sprites before assigning them to cards
        List<Sprite> shuffledSprites = ShuffleSprites(cardFrontSprites);

        int spriteIndex = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject newCard = Instantiate(gridCellPrefab, new Vector3(x, y, 0), Quaternion.identity);
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

    private List<Sprite> ShuffleSprites(Sprite[] sprites)
    {
        List<Sprite> shuffledList = new List<Sprite>(sprites);
        System.Random rng = new System.Random();

        int n = shuffledList.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Sprite value = shuffledList[k];
            shuffledList[k] = shuffledList[n];
            shuffledList[n] = value;
        }

        return shuffledList;
    }
}
