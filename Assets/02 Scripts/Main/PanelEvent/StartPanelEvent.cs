using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartPanelEvent : MainCanvasManager
{
    [Header("Start panel UI")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button creatorButton;

    private void Start()
    {
        panelDictionary.Add(0, gameObject);

        startButton.onClick.AddListener(GameStart);
        settingButton.onClick.AddListener(() => ChangePanel((int)MainPanelState.Setting));
        quitButton.onClick.AddListener(QuitGame);

        creatorButton.onClick.AddListener(() => ChangePanel((int)MainPanelState.Creator));
    }

    private void GameStart()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
