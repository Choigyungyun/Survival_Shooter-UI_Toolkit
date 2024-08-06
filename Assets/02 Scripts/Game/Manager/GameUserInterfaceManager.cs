using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SituationUiAbstract
{
    public abstract void ViewRound(int round);
}

public class GameUserInterfaceManager : MonoBehaviour
{
    protected float fadeTime = 2.0f;

    public virtual void GetRound(int round)
    {

    }

    public virtual void GetRoundTime(float time)
    {

    }

    public virtual void GetScore(int score) { }

    public virtual void InterfaceStateControl(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Nothing:
                break;
            case GameState.Ready:
                break;
            case GameState.Play:
                break;
            case GameState.Pause:
                break;
            case GameState.RoundEnd:
                break;
            case GameState.GameOver:
                break;
            case GameState.EndGame:
                break;
        }
    }

    #region Fade control Functions
    protected IEnumerator PanelFadeControl(Image panelImage, float startAlpha, float endAlpha, float waitTime)
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

    protected IEnumerator TextFadeControl(Text text, float startAlpha, float endAlpha, float waitTime)
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
}
