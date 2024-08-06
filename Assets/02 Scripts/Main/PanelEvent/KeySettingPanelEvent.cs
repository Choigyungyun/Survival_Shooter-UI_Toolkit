using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeySettingPanelEvent : MainCanvasManager
{
    [Header("Key setting panel UI")]
    [SerializeField] private InputField forwardInputField;
    [SerializeField] private InputField backInputField;
    [SerializeField] private InputField leftInputField;
    [SerializeField] private InputField rightInputField;
    [SerializeField] private Button settingBackButton;

    private void Awake()
    {
        panelDictionary.Add(3, gameObject);
    }

    private void Start()
    {
        settingBackButton.onClick.AddListener(() => ChangePanel((int)MainPanelState.Start));
    }
}
