using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 게임 상태 열거
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
/// 게임 난이도 열거
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
    [HideInInspector] public GameState gameState = GameState.Nothing;                           // 게임 상태
    [HideInInspector] public GameModeDifficulty gameModeDifficult = GameModeDifficulty.None;    // 게임 난이도

    private delegate void GameRoundDelegate(int rount);                                         // 게임 라운드 대리자
    private delegate void ScoreDelegate(int score);                                             // 게임 보드 대리자
    private delegate void RoundTimeDelegate(float roundTime);                                   // 게임 라운드 시간 대리자
    private delegate void GameStateDelegate(GameState gameState);                               // 게임 상태 대리자

    [SerializeField] private GameObject spawnManagerObject;                                     // 스폰 매니저 오브젝트
    [SerializeField] private GameObject gameUiManagerObject;                                    // 게임 UI 매니저 오브젝트

    // 대리자
    private GameRoundDelegate gameRoundDelegate;                                                // 게임 라운드 전달 값
    private ScoreDelegate scoreDelegate;                                                        // 게임 보드 전달 값
    private RoundTimeDelegate roundTimeDelegate;                                                // 게임 라운드 시간 전달 값
    private GameStateDelegate gameStateDelegate;                                                // 게임 상태 전달 값

    // 매니저
    private PlayerSpawnManager playerSpawnManager;                                              // 플레이어 스폰 관리
    private EnemySpawnManager enemySpawnManager;                                                // 적 스폰 관리
    private GameUserInterfaceManager gameUserInterfaceManager;                                  // 게임 UI 관리

    private int gameScore = 0;                                                                  // 게임 스코어
    private int gameRound = 1;                                                                  // 게임 라운드
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

        // 대리자 이벤트 전달
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
