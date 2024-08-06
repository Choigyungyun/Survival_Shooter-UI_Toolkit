using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MainPanelState
{
    Start = 0,
    Setting,
    SoundSetting,
    KeySetting,
    Creator
}

public class MainCanvasManager : MonoBehaviour
{
    protected Dictionary<int, GameObject> panelDictionary;

    private void Awake()
    {
        panelDictionary = new Dictionary<int, GameObject>();
    }

    private void Start()
    {

        ChangePanel((int)MainPanelState.Start);

        for (int panelIndex = 0; panelIndex < panelDictionary.Count; panelIndex++)
        {
            Debug.Log(panelDictionary[panelIndex].name);
        }
    }

    protected void ChangePanel(int selector)
    {
        for(int panelIndex = 0; panelIndex < panelDictionary.Count; panelIndex++)
        {
            if (panelDictionary[panelIndex] == panelDictionary[selector])
            {
                panelDictionary[selector].SetActive(true);
                Debug.Log($"Panel name : {panelDictionary[selector]}");
            }
            else
            {
                panelDictionary[panelIndex].SetActive(false);
            }
        }
    }
}
