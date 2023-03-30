using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InLevelUIManager : MonoBehaviour
{
    public TMP_Text centreText;
    public CanvasGroup resultGroup;
    public TMP_Text resultTitle;

    float fadeRate = 0.005f;

    public void UpdateUI()
    {
        if (LevelManager.instance.currentState == LevelManager.GameStates.Start)
        {
            centreText.text = "Time Remaining: \n" + LevelManager.instance.timer.displayTime;
        }
        else if (LevelManager.instance.currentState == LevelManager.GameStates.GameOver)
        {
            centreText.text = "GAMEOVER!";
        }
    }

    public void EndGameUI()
    {
        StartCoroutine(DisplayCanvas(fadeRate));
    }

    IEnumerator DisplayCanvas(float rate)
    {
        if (LevelManager.instance.currentState == LevelManager.GameStates.Won)
        {
            resultTitle.text = ("Congrats Player 1, You've Won!");
        }
        else if (LevelManager.instance.currentState == LevelManager.GameStates.Lost)
        {
            resultTitle.text = ("Congrats Player 2, You've Won!");
        }

        while (resultGroup.alpha < 0.9)
        {
            resultGroup.alpha = Mathf.Lerp(resultGroup.alpha, 1, rate);
            yield return new WaitForEndOfFrame();
        }

        resultGroup.alpha = 1f;
        yield return null;
    }
}
