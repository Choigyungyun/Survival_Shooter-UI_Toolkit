using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanelEvent : MainCanvasManager
{
    [Header("Setting panel UI")]
    [SerializeField] private Button soundSettingButton;
    [SerializeField] private Button keySettingButton;
    [SerializeField] private Button settingBackButton;

    private void Start()
    {
        soundSettingButton.onClick.AddListener(() => ChangePanel((int)MainPanelState.SoundSetting));
        keySettingButton.onClick.AddListener(() => ChangePanel((int)MainPanelState.KeySetting));
        settingBackButton.onClick.AddListener(() => ChangePanel((int)MainPanelState.Start));
    }
}
