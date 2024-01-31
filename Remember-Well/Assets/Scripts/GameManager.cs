using UnityEngine;
using System.Collections.Generic;
using System.Collections; // Needed for using List

public class GameManager : MonoBehaviour
{
    public GameObject gridCellPrefab; // Prefab for the grid cells or cards
    public Sprite[] animalCardSprites; // Assign in Inspector
    public Sprite[] plantCardSprites;
    public GameObject cardParent;

    public GameObject audioController;
    public GameObject endGamePanel;
    private int matchedPairsCount = 0;
    private int totalPairs;
    private List<CardManager> selectedCards = new List<CardManager>();

    public void InitializeGame()
    {
        Sprite[] cardFrontSprites;
        if (DifficultyController.selectedTheme == DifficultyController.CardTheme.Animals)
        {
            cardFrontSprites = animalCardSprites;
        }
        else
        {
            cardFrontSprites = plantCardSprites;
        }
        // Access the difficulty and grid size from DifficultyController
        DifficultyController.Difficulty difficulty = DifficultyController.selectedDifficulty;
        int gridWidth = DifficultyController.gridWidth;
        int gridHeight = DifficultyController.gridHeight;
        totalPairs = (gridHeight * gridWidth / 2);
        List<Sprite> shuffledSprites = ShuffleAndPairSprites(cardFrontSprites, gridWidth, gridHeight);
        CreateGrid(gridWidth, gridHeight, shuffledSprites);
    }


    private void CreateGrid(int width, int height, List<Sprite> shuffledSprites)
    {
        float cardWidth = 1.8f; // Width of a card
        float cardHeight = 2.4f; // Height of a card
        float spacing = 0.2f; // Spacing between cards

        // Calculate total grid size
        float totalGridWidth = width * cardWidth + (width - 1) * spacing;
        float totalGridHeight = height * cardHeight + (height - 1) * spacing;

        // Calculate the start position
        Vector3 startPos = new Vector3(-totalGridWidth / 2 + cardWidth / 2, totalGridHeight / 2 - cardHeight / 2, 0);

        int spriteIndex = 0;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Calculate position for each card
                Vector3 position = new Vector3(startPos.x + x * (cardWidth + spacing), startPos.y - y * (cardHeight + spacing), 0);
                GameObject newCard = Instantiate(gridCellPrefab, position, Quaternion.identity, cardParent.transform);

                // Set up the card
                CardManager cardManager = newCard.GetComponent<CardManager>();
                if (cardManager != null && spriteIndex < shuffledSprites.Count)
                {
                    cardManager.SetFrontImage(shuffledSprites[spriteIndex]);
                    spriteIndex++;
                }
            }
        }
    }

    private List<Sprite> ShuffleAndPairSprites(Sprite[] sprites, int width, int height)
    {
        int numPairs = (width * height) / 2;
        List<Sprite> pairedList = new List<Sprite>();
        System.Random rng = new System.Random();

        // Add each sprite twice
        for (int i = 0; i < numPairs; i++)
        {
            int spriteIndex = rng.Next(sprites.Length);
            pairedList.Add(sprites[spriteIndex]);
            pairedList.Add(sprites[spriteIndex]);
        }

        // Shuffle
        for (int i = 0; i < pairedList.Count; i++)
        {
            int swapIndex = rng.Next(pairedList.Count);
            Sprite temp = pairedList[i];
            pairedList[i] = pairedList[swapIndex];
            pairedList[swapIndex] = temp;
        }

        return pairedList;
    }

    public void CardSelected(CardManager card)
    {
        // Prevent selecting more cards if two are already selected
        if (selectedCards.Count == 2)
        {
            return;
        }

        // Toggle selection if the same card is clicked again
        if (selectedCards.Contains(card))
        {
            selectedCards.Remove(card);
            return;
        }

        // Add new selection
        selectedCards.Add(card);

        // Check if two cards are selected
        if (selectedCards.Count == 2)
        {
            SetAllCardsClickable(false);
            StartCoroutine(WaitAndCheckMatch());
        }
    }

    private IEnumerator WaitAndCheckMatch()
    {
        // Wait for seconds
        yield return new WaitForSeconds(0.5f);

        // Check for a match
        CheckForMatch();
        SetAllCardsClickable(true);
    }

    private void CheckForMatch()
    {
        if (selectedCards[0].frontSprite == selectedCards[1].frontSprite)
        {
            Debug.Log("Match found!");
            matchedPairsCount++;
            Debug.Log("Matched Pairs: " + matchedPairsCount + ", Total Pairs: " + totalPairs);
            // Check for endgame condition
            if (matchedPairsCount >= totalPairs)
            {
                EndGame();
            }
            foreach (CardManager card in selectedCards)
            {
                card.DisableCard();
                audioController.GetComponent<SFXController>().PlayCorrect();
            }

        }
        else
        {
            Debug.Log("No match!");
            // Handle no match (e.g., flip cards back)
            foreach (CardManager card in selectedCards)
            {
                card.FlipCard();
                audioController.GetComponent<SFXController>().PlayFalse();
            }
        }

        // Clear the list of selected cards
        selectedCards.Clear();
    }

    private void SetAllCardsClickable(bool clickable)
    {
        foreach (Transform child in cardParent.transform)
        {
            CardManager cardManager = child.GetComponent<CardManager>();
            if (cardManager != null)
            {
                cardManager.SetClickable(clickable);
            }
        }

    }

    private void EndGame()
    {
        if (endGamePanel != null)
        {
            endGamePanel.SetActive(true);
        }
    }


}
