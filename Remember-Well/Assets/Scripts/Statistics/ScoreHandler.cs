using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler : MonoBehaviour {
    List<ScoreElement> scoreList = new List<ScoreElement> ();
    [SerializeField] string filename;
    [SerializeField] int maxCount = 100;


    public delegate void OnScoreListChanged (List<ScoreElement> list);
    public static event OnScoreListChanged onScoreListChanged;

    private void Start () {
        LoadScores ();
    }

    private void LoadScores () {
        scoreList = FileHandler.ReadListFromJSON<ScoreElement> (filename);

        while (scoreList.Count > maxCount) {
            scoreList.RemoveAt (maxCount);
        }

        if (onScoreListChanged != null) {
            onScoreListChanged.Invoke (scoreList);
        }
    }

    private void SaveScore () {
        FileHandler.SaveToJSON<ScoreElement> (scoreList, filename);
    }

    public void AddScore (ScoreElement element) {
        for (int i = 0; i < maxCount; i++) {
            // add new score
            scoreList.Insert (i, element);

            while (scoreList.Count > maxCount) {
                scoreList.RemoveAt (maxCount);
            }

            SaveScore ();

            if (onScoreListChanged != null) {
                onScoreListChanged.Invoke (scoreList);
            }
        }
    }

}