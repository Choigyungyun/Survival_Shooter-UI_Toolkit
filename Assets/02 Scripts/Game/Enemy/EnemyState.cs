using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

using GameSettingProperty;

public class EnemyState : MonoBehaviour
{
    [SerializeField] private int enemyDamage = 0;           // �� ������
    [SerializeField] private int enemyHp = 0;               // �� ü��
    [SerializeField] private int enemyScore = 0;            // �� ���ھ�
    [SerializeField] private float attackInterval = 0.0f;   // �� ���� ����

    [SerializeField] private AudioClip enemyDeadClip;       // �� ���� �Ҹ� Ŭ��

    private bool rangeInPlayer = false;                     // �÷��̾� �浹 ����
    private float timer = 0.0f;                             // �� Ÿ�̸�

    private Animator enemyAnimator;                         // �� �ִϸ��̼� ��Ʈ�� �ִϸ�����
    private AudioSource enemyAudio;                         // �� ����� �ҽ� (�Ҹ� �⺻ �� : �� ��ġ�� �Ҹ� Ŭ��)
    private NavMeshAgent enemyNavMeshAgent;                 // �� AI (�÷��̾� ����)
    private ParticleSystem enemyHitParticle;                // �� Ÿ�� ��ƼŬ

    private GameObject playerObject;                        // �÷��̾� ������Ʈ
    private PlayerState playerState;                        // �÷��̾� ����

    private void Awake()
    {
        // �� �� �ʱ�ȭ
        enemyAnimator = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();
        enemyHitParticle = GetComponentInChildren<ParticleSystem>();

        // �÷��̾� �� �ʱ�ȭ
        playerObject = GameObject.FindGameObjectWithTag("Player");

        playerState = playerObject.GetComponent<PlayerState>();
    }

    /// <summary>
    /// �ݸ��� �浹�� ȣ��
    /// </summary>
    /// <remarks>
    /// ���� �÷��̾�� �浹��
    /// <paramref name="rangeInPlayer"/>�� ���� true�� �˴ϴ�.
    /// </remarks>
    /// <param name="collision">�浹�� ������Ʈ�� �ݸ���</param>
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        rangeInPlayer = true;
    }

    /// <summary>
    /// �ݸ��� �浹���� ����� ȣ��
    /// </summary>
    /// <remarks>
    /// ���� �÷��̾�� �浹���� �����
    /// <paramref name="rangeInPlayer"/>�� ���� false�� �˴ϴ�.
    /// </remarks>
    /// <param name="collision"></param>
    private void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        rangeInPlayer = false;
    }

    private void Update()
    {
        if (GameManager.Instance.gameState == GameState.Play)
        {
            timer += Time.deltaTime;

            if (timer >= attackInterval && rangeInPlayer)
            {
                playerState.PlayerTakeDamage(enemyDamage);
                timer = 0.0f;
            }

            if (playerState.currentHp <= 0 && !playerState.isDead)
            {
                enemyAnimator.SetTrigger("gameOver");
            }

            if (playerObject == null || playerState.isDead)
            {
                return;
            }

            enemyNavMeshAgent.SetDestination(playerObject.transform.position);
        }
        else if (GameManager.Instance.gameState == GameState.RoundEnd)
        {
            enemyAnimator.SetBool("isDie", true);
            enemyNavMeshAgent.isStopped = true;
        }
        else
        {
            enemyNavMeshAgent.isStopped = true;
        }
    }

    /// <summary>
    /// ���� �������� ������ ȣ��
    /// </summary>
    /// <param name="damage">�÷��̾� �� ������</param>
    /// <param name="hitDistance">���� ���� ���� ��ġ</param>
    public void EnemyTakeDamage(int damage, Vector3 hitDistance)
    {
        if (enemyHp > 0)
        {
            enemyHp -= damage;
            enemyAudio.Play();

            enemyHitParticle.gameObject.transform.position = hitDistance;
            enemyHitParticle.Play();
        }
        else
        {
            GameManager.Instance.AddScore(enemyScore);
            enemyAnimator.SetBool("isDie", true);
            enemyNavMeshAgent.isStopped = true;
        }
    }

    /// <summary>
    /// ���� �״� �ִϸ��̼��� �����Ҷ� ȣ��
    /// </summary>
    /// <remarks>
    /// �� "Death" �ִϸ��̼ǿ� ��ϵǾ��ֽ��ϴ�.
    /// </remarks>
    private void StartDeath()
    {
        GetComponent<CapsuleCollider>().enabled = false;

        enemyAudio.PlayOneShot(enemyDeadClip);
    }

    /// <summary>
    /// ���� �״� �ִϸ��̼��� ������ ȣ��
    /// </summary>
    /// <remarks>
    /// �� "Death" �ִϸ��̼ǿ� ��ϵǾ��ֽ��ϴ�.
    /// </remarks>
    private void EndSinking()
    {
        if (gameObject == null)
        {
            return;
        }
        Destroy(gameObject);
    }
}
