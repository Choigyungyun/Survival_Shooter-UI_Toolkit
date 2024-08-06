using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class MainSettingPanelEvent : MainCanvasManager
{
    [Header("Setting panel UI")]
    [SerializeField] private Button soundSettingButton;
    [SerializeField] private Button keySettingButton;
    [SerializeField] private Button settingBackButton;

    private void Awake()
    {
        panelDictionary.Add(1, gameObject);
    }

    private void Start()
    {
        soundSettingButton.onClick.AddListener(() => ChangePanel((int)MainPanelState.SoundSetting));
        keySettingButton.onClick.AddListener(() => ChangePanel((int)MainPanelState.KeySetting));
        settingBackButton.onClick.AddListener(() => ChangePanel((int)MainPanelState.Start));
    }
}
