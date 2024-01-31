using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour {
    [SerializeField] GameObject panel;
    [SerializeField] GameObject scoreUIElementPrefab;
    [SerializeField] Transform elementWrapper;

    List<GameObject> uiElements = new List<GameObject> ();

    private void OnEnable () {
        ScoreHandler.onScoreListChanged += UpdateUI;
    }

    private void OnDisable () {
        ScoreHandler.onScoreListChanged -= UpdateUI;
    }

/*     public void ShowPanel () {
        panel.SetActive (true);
    }

    public void ClosePanel () {
        panel.SetActive (false);
    } */

    private void UpdateUI (List<ScoreElement> list) {
        for (int i = 0; i < list.Count; i++) {
            ScoreElement el = list[i];

            if (el != null) {
                if (i >= uiElements.Count) {
                    // instantiate new entry
                    var inst = Instantiate (scoreUIElementPrefab, Vector3.zero, Quaternion.identity);
                    inst.transform.SetParent (elementWrapper, false);

                    uiElements.Add (inst);
                }

                // write or overwrite list
                var texts = uiElements[i].GetComponentsInChildren<Text> ();
                texts[0].text = el.playerName;
                texts[1].text = el.points.ToString ();
                texts[2].text = el.time.ToString();
                texts[3].text = el.mistakes.ToString();
            }
        }
    }

}