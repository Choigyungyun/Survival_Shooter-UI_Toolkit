using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using GameSettingProperty;

public class PlayerState : MonoBehaviour
{
    [NonSerialized] public bool isDead = false;         // 플레이어 죽음 상태
    [NonSerialized] public int currentHp = 0;           // 플레이어 유동적인 체력

    [NonSerialized] public Slider playerHpSlider;       // 플레이어 체력을 표시하는 슬라이드바 UI

    [Header("Player state settings")]
    [SerializeField] private int playerHp = 0;          // 플레이어 기본 체력 값

    [Header("Player Audioes")]
    [SerializeField] private AudioClip playerDeadAudio; // 플레이어 죽은 소리 클립

    private AudioSource playerAudio;                    // 플레이어 오디오 소스 (기본 소리 값 : 플레이어 다치는 소리 클립)
    private Animator playerAnimator;                    // 플레이어 애니메이션 컨트롤 애니메이터
    private PlayerMove playerMove;                      // 플레이어 움직임 컴포넌트
    private PlayerGaze playerGaze;                      // 플레이어 시선 컴포넌트
    private PlayerGunFire playerGunFire;                // 플레이어 총 발사 컴포넌트

    private void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        playerGaze = GetComponent<PlayerGaze>();
        playerAudio = GetComponentInChildren<AudioSource>();
        playerAnimator = GetComponentInChildren<Animator>();
        playerGunFire = GetComponentInChildren<PlayerGunFire>();

        currentHp = playerHp;
        playerHpSlider.value = currentHp;
    }

    /// <summary>
    /// 플레이어가 데미지를 받으면 호출
    /// </summary>
    /// <param name="damage">적 데미지</param>
    public void PlayerTakeDamage(int damage)
    {
        if (currentHp > 0)
        {
            currentHp -= damage;
            playerHpSlider.value = currentHp;
            playerAudio.Play();
        }
        else
        {
            PlayerDead();
        }
    }

    /// <summary>
    /// 플레이어가 죽으면 호출
    /// </summary>
    public void PlayerDead()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;

        playerMove.enabled = false;
        playerGaze.enabled = false;
        playerGunFire.enabled = false;

        GetComponent<CapsuleCollider>().enabled = false;

        currentHp = 0;

        playerAnimator.SetTrigger("gameOver");
        playerAudio.PlayOneShot(playerDeadAudio);
    }

    /// <summary>
    /// 플레이어가 죽는 애니메이션이 시작할때 호출
    /// </summary>
    /// <remarks>
    /// 플레이어 "Death" 애니메이션에 등록되어 있습니다.
    /// </remarks>
    private void StartDeath()
    {
        GameManager.Instance.AccordingToGameState(GameState.GameOver);
    }

    /// <summary>
    /// 플레이어가 죽는 애니메이션이 끝날때 호출
    /// </summary>
    /// <remarks>
    /// 플레이어 "Death" 애니메이션에 등록되어 있습니다.
    /// </remarks>
    private void RestartLevel()
    {

    }
}
