using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using GameSettingProperty;

public class PlayerState : MonoBehaviour
{
    [NonSerialized] public bool isDead = false;         // �÷��̾� ���� ����
    [NonSerialized] public int currentHp = 0;           // �÷��̾� �������� ü��

    [NonSerialized] public Slider playerHpSlider;       // �÷��̾� ü���� ǥ���ϴ� �����̵�� UI

    [Header("Player state settings")]
    [SerializeField] private int playerHp = 0;          // �÷��̾� �⺻ ü�� ��

    [Header("Player Audioes")]
    [SerializeField] private AudioClip playerDeadAudio; // �÷��̾� ���� �Ҹ� Ŭ��

    private AudioSource playerAudio;                    // �÷��̾� ����� �ҽ� (�⺻ �Ҹ� �� : �÷��̾� ��ġ�� �Ҹ� Ŭ��)
    private Animator playerAnimator;                    // �÷��̾� �ִϸ��̼� ��Ʈ�� �ִϸ�����
    private PlayerMove playerMove;                      // �÷��̾� ������ ������Ʈ
    private PlayerGaze playerGaze;                      // �÷��̾� �ü� ������Ʈ
    private PlayerGunFire playerGunFire;                // �÷��̾� �� �߻� ������Ʈ

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
    /// �÷��̾ �������� ������ ȣ��
    /// </summary>
    /// <param name="damage">�� ������</param>
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
    /// �÷��̾ ������ ȣ��
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
    /// �÷��̾ �״� �ִϸ��̼��� �����Ҷ� ȣ��
    /// </summary>
    /// <remarks>
    /// �÷��̾� "Death" �ִϸ��̼ǿ� ��ϵǾ� �ֽ��ϴ�.
    /// </remarks>
    private void StartDeath()
    {
        GameManager.Instance.AccordingToGameState(GameState.GameOver);
    }

    /// <summary>
    /// �÷��̾ �״� �ִϸ��̼��� ������ ȣ��
    /// </summary>
    /// <remarks>
    /// �÷��̾� "Death" �ִϸ��̼ǿ� ��ϵǾ� �ֽ��ϴ�.
    /// </remarks>
    private void RestartLevel()
    {

    }
}
