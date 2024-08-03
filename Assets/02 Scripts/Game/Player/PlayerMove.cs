using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 0.0f;      // 캐릭터 이동 속도

    private bool isMoving = false;      // 움직임 여부

    private Animator moveAnimator;      // 캐릭터 애니메이션 컨트롤

    private void Awake()
    {
        moveAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.gameState != GameState.Play)
        {
            return;
        }
        transform.position += Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void Update()
    {
        if (GameManager.Instance.gameState != GameState.Play)
        {
            return;
        }
        moveAnimator.SetBool("IsWalking", isMoving);
    }

    /// <summary>
    /// 캐릭터 이동 함수
    /// </summary>
    /// <param name="horizontal">x값 수평 이동</param>
    /// <param name="vertical">z값 수직 이동</param>
    /// <return>속도 Vector3 값</return>
    private Vector3 Move(float horizontal, float vertical)
    {
        if (horizontal != 0 || vertical != 0)
        {
            isMoving = true;

            // 입력 받은 값을 일정한 움직임 계산을 위해 Vector값으로 정규화 합니다.
            Vector3 move = new Vector3(horizontal, 0.0f, vertical).normalized;

            // 게임이 실행되는 프레임 속도에 관계없이 일정한 평균 속도로 움직입니다.
            return move * moveSpeed * Time.deltaTime;
        }
        else
        {
            isMoving = false;

            return Vector3.zero;
        }
    }
}
