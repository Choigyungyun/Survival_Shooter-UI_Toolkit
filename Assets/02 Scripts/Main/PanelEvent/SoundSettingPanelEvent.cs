using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettingPanelEvent : MainCanvasManager
{
    [Header("Sound setting panel UI")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider effectVolumeSlider;
    [SerializeField] private Button soundSettingBackButton;

    private void Awake()
    {
        panelDictionary.Add(2, gameObject);
    }
}
