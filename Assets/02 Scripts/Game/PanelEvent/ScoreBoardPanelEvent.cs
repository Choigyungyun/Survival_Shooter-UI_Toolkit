using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoardPanelEvent : GameUserInterfaceManager
{
    [SerializeField] private Text scoreBoardText;
    [SerializeField] private Text roundTimeText;

    public override void InterfaceStateControl(GameState gameState)
    {
        base.InterfaceStateControl(gameState);

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
}
