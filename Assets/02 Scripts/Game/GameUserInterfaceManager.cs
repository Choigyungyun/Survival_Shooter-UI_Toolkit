using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUserInterfaceManager : MonoBehaviour
{
    [SerializeField] private GameObject situationPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Text scoreBoardText;
    [SerializeField] private Text roundTimeText;
    [SerializeField] private Text situationText;

    private float fadeTime = 2.0f;
    private float countDownTime = 3.0f;

    public void InterfaceStateControl(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Nothing:
                break;
            case GameState.Ready:
                StartCoroutine(GameReady());
                break;
            case GameState.Play:
                situationText.gameObject.SetActive(false);
                break;
            case GameState.Pause:
                break;
            case GameState.RoundEnd:
                StartCoroutine(GameRoundEnd());
                break;
            case GameState.GameOver:
                break;
            case GameState.EndGame:
                break;
        }
    }
    #region Delegate Functions
    public void GetRound(int round)
    {
        situationText.text = $"Day {round}";
    }

    public void GetScoreBoard(int score)
    {
        scoreBoardText.text = $"Score : {score}";
    }

    public void GetRoundTime(float time)
    {
        roundTimeText.text = $"Time : {(int)time}";
    }
    #endregion

    #region Fade control Functions
    private IEnumerator PanelFadeControl(Image panelImage, float startAlpha, float endAlpha, float waitTime)
    {
        Color color = panelImage.color;
        color.a = startAlpha;
        panelImage.color = color;

        yield return new WaitForSeconds(waitTime);

        float timer = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            timer += Time.deltaTime;
            percent = timer / fadeTime;

            color.a = Mathf.Lerp(startAlpha, endAlpha, percent);

            panelImage.color = color;

            yield return null;
        }
    }

    private IEnumerator TextFadeControl(Text text, float startAlpha, float endAlpha, float waitTime)
    {
        Color color = text.color;
        color.a = startAlpha;
        text.color = color;

        yield return new WaitForSeconds(waitTime);

        float timer = 0.0f;
        float percent = 0.0f;

        while(percent < 1)
        {
            timer += Time.deltaTime;
            percent = timer / fadeTime;

            color.a = Mathf.Lerp(startAlpha, endAlpha, percent);

            text.color = color;

            yield return null;
        }
    }
    #endregion

    private IEnumerator CountDown(float countTime)
    {

        while (countTime > 0)
        {
            situationText.text = countTime.ToString();
            yield return new WaitForSeconds(1f);

            countTime--;
        }

        situationText.text = "Start!";
    }

    private IEnumerator GameReady()
    {
        situationText.gameObject.SetActive(true);

        StartCoroutine(PanelFadeControl(situationPanel.GetComponent<Image>(), 1.0f, 0.0f, 2.5f));
        yield return new WaitForSeconds(3f + fadeTime);

        StartCoroutine(CountDown(countDownTime));
        yield return new WaitForSeconds(4.0f);

        GameManager.Instance.AccordingToGameState(GameState.Play);
    }

    private IEnumerator GameRoundEnd()
    {
        StartCoroutine(PanelFadeControl(situationPanel.GetComponent<Image>(), 0.0f, 1.0f, 1f));
        yield return new WaitForSeconds(3f + fadeTime);

        GameManager.Instance.AccordingToGameState(GameState.Ready);
    }

    private void GamePause()
    {

    }
}
