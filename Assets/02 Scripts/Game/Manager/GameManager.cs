using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���� ���� ����
/// </summary>
public enum GameState
{
    Nothing = 0,
    Ready,
    Play,
    Pause,
    RoundEnd,
    GameOver,
    EndGame
}

/// <summary>
/// ���� ���̵� ����
/// </summary>
public enum GameModeDifficulty
{
    None = 0,
    Easy,
    Nomal,
    Hard
}

public class GameManager : GenericSingleton<GameManager>
{
    [HideInInspector] public GameState gameState = GameState.Nothing;                           // ���� ����
    [HideInInspector] public GameModeDifficulty gameModeDifficult = GameModeDifficulty.None;    // ���� ���̵�

    private delegate void GameRoundDelegate(int rount);                                         // ���� ���� �븮��
    private delegate void ScoreDelegate(int score);                                             // ���� ���� �븮��
    private delegate void RoundTimeDelegate(float roundTime);                                   // ���� ���� �ð� �븮��
    private delegate void GameStateDelegate(GameState gameState);                               // ���� ���� �븮��

    [SerializeField] private GameObject spawnManagerObject;                                     // ���� �Ŵ��� ������Ʈ
    [SerializeField] private GameObject gameUiManagerObject;                                    // ���� UI �Ŵ��� ������Ʈ

    // �븮��
    private GameRoundDelegate gameRoundDelegate;                                                // ���� ���� ���� ��
    private ScoreDelegate scoreDelegate;                                                        // ���� ���� ���� ��
    private RoundTimeDelegate roundTimeDelegate;                                                // ���� ���� �ð� ���� ��
    private GameStateDelegate gameStateDelegate;                                                // ���� ���� ���� ��

    // �Ŵ���
    private PlayerSpawnManager playerSpawnManager;                                              // �÷��̾� ���� ����
    private EnemySpawnManager enemySpawnManager;                                                // �� ���� ����
    private GameUserInterfaceManager gameUserInterfaceManager;                                  // ���� UI ����

    private int gameScore = 0;                                                                  // ���� ���ھ�
    private int gameRound = 1;                                                                  // ���� ����
    private float startTime = 60.9f;

    private Color fadeColor;

    private void Awake()
    {
        playerSpawnManager = spawnManagerObject.GetComponent<PlayerSpawnManager>();
        enemySpawnManager = spawnManagerObject.GetComponent<EnemySpawnManager>();
        gameUserInterfaceManager = gameUiManagerObject.GetComponent<GameUserInterfaceManager>();
    }

    private void Start()
    {
        gameModeDifficult = GameModeDifficulty.Easy;
        AccordingToGameState(GameState.Ready);

        // �븮�� �̺�Ʈ ����
        gameStateDelegate += gameUserInterfaceManager.InterfaceStateControl;
    }

    public void AddScore(int score)
    {
        gameScore += score;

        scoreDelegate?.Invoke(gameScore);
    }

    public void AccordingToGameState(GameState state)
    {
        gameState = state;

        switch (state)
        {
            case GameState.Nothing:
                break;
            case GameState.Ready:
                gameRoundDelegate?.Invoke(gameRound);
                scoreDelegate?.Invoke(gameScore);
                roundTimeDelegate?.Invoke(startTime);
                break;
            case GameState.Play:
                enemySpawnManager.InitializeSpawnDifficult(gameModeDifficult);
                StartCoroutine(RoundTime(startTime));
                break;
            case GameState.Pause:
                break;
            case GameState.RoundEnd:
                gameRoundDelegate?.Invoke(gameRound);
                scoreDelegate?.Invoke(gameScore);
                break;
            case GameState.GameOver:
                break;
        }

        gameStateDelegate?.Invoke(state);
    }

    private IEnumerator RoundTime(float maxTime)
    {
        while (maxTime > 0.0f)
        {
            maxTime -= Time.deltaTime;

            if (maxTime > 0.0f)
            {
                roundTimeDelegate?.Invoke(maxTime);
            }
            else
            {
                gameRound += 1;
                AccordingToGameState(GameState.RoundEnd);
                roundTimeDelegate?.Invoke(0.0f);
            }
            yield return null;
        }
    }
}
