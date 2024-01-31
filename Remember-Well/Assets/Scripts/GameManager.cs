using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    #region Variables
    public GameObject gridCellPrefab;
    public Sprite[] animalCardSprites;
    public Sprite[] plantCardSprites;
    public GameObject cardParent;
    public GameObject audioController;
    public GameObject endGamePanel;

    private int matchedPairsCount = 0;
    private int totalPairs;
    private List<CardManager> selectedCards = new List<CardManager>();
    private float startTime;
    private float elapsedTime;
    private bool stopwatchStarted = false;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;

    int score;
    #endregion

    #region Initialization
    void Start()
    {
        score = 0;
        UpdateScoreText();
    }

    private void Update()
    {
        UpdateTimer();
    }

    public void InitializeGame()
    {
        Sprite[] cardFrontSprites = (DifficultyController.selectedTheme == DifficultyController.CardTheme.Animals)
            ? animalCardSprites
            : plantCardSprites;

        DifficultyController.Difficulty difficulty = DifficultyController.selectedDifficulty;
        int gridWidth = DifficultyController.gridWidth;
        int gridHeight = DifficultyController.gridHeight;
        totalPairs = (gridHeight * gridWidth / 2);
        List<Sprite> shuffledSprites = ShuffleAndPairSprites(cardFrontSprites, gridWidth, gridHeight);
        CreateGrid(gridWidth, gridHeight, shuffledSprites);

        ResetStopwatch();

        timerText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
    }
    #endregion

    #region Grid Creation
    private void CreateGrid(int width, int height, List<Sprite> shuffledSprites)
    {
        float cardWidth = 1.8f;
        float cardHeight = 2.4f;
        float spacing = 0.2f;

        float totalGridWidth = width * cardWidth + (width - 1) * spacing;
        float totalGridHeight = height * cardHeight + (height - 1) * spacing;

        Vector3 startPos = new Vector3(-totalGridWidth / 2 + cardWidth / 2, totalGridHeight / 2 - cardHeight / 2, 0);

        int spriteIndex = 0;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 position = new Vector3(startPos.x + x * (cardWidth + spacing), startPos.y - y * (cardHeight + spacing), 0);
                GameObject newCard = Instantiate(gridCellPrefab, position, Quaternion.identity, cardParent.transform);

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

        for (int i = 0; i < numPairs; i++)
        {
            int spriteIndex = rng.Next(sprites.Length);
            pairedList.Add(sprites[spriteIndex]);
            pairedList.Add(sprites[spriteIndex]);
        }

        for (int i = 0; i < pairedList.Count; i++)
        {
            int swapIndex = rng.Next(pairedList.Count);
            Sprite temp = pairedList[i];
            pairedList[i] = pairedList[swapIndex];
            pairedList[swapIndex] = temp;
        }

        return pairedList;
    }
    #endregion

    #region Card Selection
    public void CardSelected(CardManager card)
    {
        if (selectedCards.Count == 2)
        {
            return;
        }

        if (selectedCards.Contains(card))
        {
            selectedCards.Remove(card);
            return;
        }

        selectedCards.Add(card);

        if (selectedCards.Count == 2)
        {
            SetAllCardsClickable(false);
            StartCoroutine(WaitAndCheckMatch());
        }
    }

    private IEnumerator WaitAndCheckMatch()
    {
        yield return new WaitForSeconds(0.5f);

        CheckForMatch();
        SetAllCardsClickable(true);
    }

    private void CheckForMatch()
    {
        if (selectedCards[0].frontSprite == selectedCards[1].frontSprite)
        {
            Debug.Log("Match found!");
            matchedPairsCount++;
            Debug.Log($"Matched Pairs: {matchedPairsCount}, Total Pairs: {totalPairs}");

            if (matchedPairsCount >= totalPairs)
            {
                EndGame();
            }

            foreach (CardManager card in selectedCards)
            {
                card.DisableCard();
                audioController.GetComponent<SFXController>().PlayCorrect();
            }

            score++;
            UpdateScoreText();
        }
        else
        {
            Debug.Log("No match!");

            foreach (CardManager card in selectedCards)
            {
                card.FlipCard();
                audioController.GetComponent<SFXController>().PlayFalse();
            }
        }

        selectedCards.Clear();
    }

    private void SetAllCardsClickable(bool clickable)
    {
        foreach (Transform child in cardParent.transform)
        {
            CardManager cardManager = child.GetComponent<CardManager>();
            cardManager?.SetClickable(clickable);
        }
    }
    #endregion

    #region Game Logic
    private void EndGame()
    {
        if (endGamePanel != null)
        {
            stopwatchStarted = false;
            StopStopwatch();

            endGamePanel.SetActive(true);
            timerText.text = $"Time: {FormatTime(elapsedTime)}";
            scoreText.text = $"Score: {score}";
        }
    }
    #endregion

    #region Stopwatch
    private String FormatTime(float timeInSeconds) => TimeSpan.FromSeconds(timeInSeconds).ToString("mm':'ss");

    public void StartStopwatch()
    {
        stopwatchStarted = true;
        startTime = Time.time;
    }

    private void StopStopwatch()
    {
        elapsedTime = Time.time - startTime;
    }

    private void ResetStopwatch()
    {
        startTime = 0f;
        elapsedTime = 0f;
    }
    #endregion

    #region Score Calculation
    int CalculateScore(float elapsedTime)
    {
        int baseScore = 1000;
        return Mathf.Max(0, baseScore - Mathf.RoundToInt(elapsedTime * 10));
    }
    #endregion

    #region Timer
    private void UpdateTimer()
    {
        if (stopwatchStarted)
        {
            elapsedTime = Time.time - startTime;
            timerText.text = $"Time: {TimeSpan.FromSeconds(elapsedTime):mm':'ss}";
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";

        }
    }
    #endregion
}
