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
    protected List<GameObject> panelList = new List<GameObject>();

    private void Start()
    {
        ChangePanel((int)MainPanelState.Start);
    }

    protected void ChangePanel(int selector)
    {
        for(int panelIndex = 0; panelIndex < panelList.Count; panelIndex++)
        {
            if (panelList[panelIndex] == panelList[selector])
            {
                panelList[selector].SetActive(true);
                Debug.Log($"Panel name : {panelList[selector]}");
            }
            else
            {
                panelList[panelIndex].SetActive(false);
            }
        }
    }
}
