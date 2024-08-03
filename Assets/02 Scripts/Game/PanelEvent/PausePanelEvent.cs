using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanelEvent : GameUserInterfaceManager
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button lobbyButton;

    private void Start()
    {
        playButton.onClick.AddListener(OnClickPlay);
        settingButton.onClick.AddListener(OnClickSetting);
        lobbyButton.onClick.AddListener(OnClickLobby);
    }

    private void OnClickPlay()
    {
        gameObject.SetActive(false);
        GameManager.Instance.AccordingToGameState(GameState.Play);
    }

    private void OnClickSetting()
    {

    }

    private void OnClickLobby()
    {

    }
}
