using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SituationPanelEvent : GameUserInterfaceManager
{
    [SerializeField] private Text situationText;

    private float countDownTime = 3.0f;

    private Image panelImage;

    private void Awake()
    {
        panelImage = GetComponent<Image>();
    }

    public override void InterfaceStateControl(GameState gameState)
    {
        base.InterfaceStateControl(gameState);

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

    private IEnumerator GameReady()
    {
        situationText.gameObject.SetActive(true);

        StartCoroutine(PanelFadeControl(panelImage, 1.0f, 0.0f, 2.5f));
        yield return new WaitForSeconds(3f + fadeTime);

        StartCoroutine(CountDown(countDownTime));
        yield return new WaitForSeconds(4.0f);

        GameManager.Instance.AccordingToGameState(GameState.Play);
    }

    private IEnumerator GameRoundEnd()
    {
        StartCoroutine(PanelFadeControl(panelImage, 0.0f, 1.0f, 1f));
        yield return new WaitForSeconds(3f + fadeTime);

        GameManager.Instance.AccordingToGameState(GameState.Ready);
    }

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
}
