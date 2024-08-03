using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

using GameSettingProperty;

public class EnemyState : MonoBehaviour
{
    [SerializeField] private int enemyDamage = 0;           // 적 데미지
    [SerializeField] private int enemyHp = 0;               // 적 체력
    [SerializeField] private int enemyScore = 0;            // 적 스코어
    [SerializeField] private float attackInterval = 0.0f;   // 적 공격 간격

    [SerializeField] private AudioClip enemyDeadClip;       // 적 죽은 소리 클립

    private bool rangeInPlayer = false;                     // 플레이어 충돌 유부
    private float timer = 0.0f;                             // 적 타이머

    private Animator enemyAnimator;                         // 적 애니메이션 컨트롤 애니메이터
    private AudioSource enemyAudio;                         // 적 오디오 소스 (소리 기본 값 : 적 다치는 소리 클립)
    private NavMeshAgent enemyNavMeshAgent;                 // 적 AI (플레이어 추적)
    private ParticleSystem enemyHitParticle;                // 적 타격 파티클

    private GameObject playerObject;                        // 플레이어 오브젝트
    private PlayerState playerState;                        // 플레이어 상태

    private void Awake()
    {
        // 적 값 초기화
        enemyAnimator = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();
        enemyHitParticle = GetComponentInChildren<ParticleSystem>();

        // 플레이어 값 초기화
        playerObject = GameObject.FindGameObjectWithTag("Player");

        playerState = playerObject.GetComponent<PlayerState>();
    }

    /// <summary>
    /// 콜리전 충돌시 호출
    /// </summary>
    /// <remarks>
    /// 만약 플레이어와 충돌시
    /// <paramref name="rangeInPlayer"/>의 값이 true가 됩니다.
    /// </remarks>
    /// <param name="collision">충돌한 오브젝트의 콜리전</param>
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        rangeInPlayer = true;
    }

    /// <summary>
    /// 콜리전 충돌에서 벗어나면 호출
    /// </summary>
    /// <remarks>
    /// 만약 플레이어와 충돌에서 벗어나면
    /// <paramref name="rangeInPlayer"/>의 값이 false가 됩니다.
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
    /// 적이 데미지를 받으면 호출
    /// </summary>
    /// <param name="damage">플레이어 총 데미지</param>
    /// <param name="hitDistance">적이 총을 맞은 위치</param>
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
    /// 적이 죽는 애니메이션이 시작할때 호출
    /// </summary>
    /// <remarks>
    /// 적 "Death" 애니메이션에 등록되어있습니다.
    /// </remarks>
    private void StartDeath()
    {
        GetComponent<CapsuleCollider>().enabled = false;

        enemyAudio.PlayOneShot(enemyDeadClip);
    }

    /// <summary>
    /// 적이 죽는 애니메이션이 끝날때 호출
    /// </summary>
    /// <remarks>
    /// 적 "Death" 애니메이션에 등록되어있습니다.
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
