using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using GameSettingProperty;

public class PlayerSpawnManager : MonoBehaviour
{
    [Header("Player Prefabs")]
    [SerializeField] private GameObject playerObject;

    [Header("Set Player UI")]
    [SerializeField] private Slider hpSlider;

    private GameObject currentPlayer;

    private void Start()
    {
        if (currentPlayer != null)
        {
            return;
        }
        currentPlayer = Instantiate(playerObject);

        Camera.main.GetComponent<CameraMove>().targetTransform = currentPlayer.transform;
        currentPlayer.GetComponent<PlayerState>().playerHpSlider = hpSlider;

        hpSlider.gameObject.SetActive(true);
    }
}
