using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIRoot : MonoBehaviour
{
    [SerializeField]
    TMP_Text scoreUI; 
    [SerializeField]
    TMP_Text resultUI;

    void Start()
    {
        scoreUI.gameObject.SetActive(false);
        resultUI.gameObject.SetActive(false);
    }

    public void OnGameStarted()
    {
        scoreUI.gameObject.SetActive(true);
        scoreUI.text = string.Format("Score: {0}", 0);
    }

    public void OnGameEnded(bool isVictory, int buildingCount)
    {
        resultUI.gameObject.SetActive(true);
        resultUI.text = isVictory ? "You Win!" : "You Lose";
    }

    public void OnScoreChanged(int score)
    {
        scoreUI.text = string.Format("Score: {0}", score);
    }

}
