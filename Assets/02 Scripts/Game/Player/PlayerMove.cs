using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 0.0f;      // ĳ���� �̵� �ӵ�

    private bool isMoving = false;      // ������ ����

    private Animator moveAnimator;      // ĳ���� �ִϸ��̼� ��Ʈ��

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
    /// ĳ���� �̵� �Լ�
    /// </summary>
    /// <param name="horizontal">x�� ���� �̵�</param>
    /// <param name="vertical">z�� ���� �̵�</param>
    /// <return>�ӵ� Vector3 ��</return>
    private Vector3 Move(float horizontal, float vertical)
    {
        if (horizontal != 0 || vertical != 0)
        {
            isMoving = true;

            // �Է� ���� ���� ������ ������ ����� ���� Vector������ ����ȭ �մϴ�.
            Vector3 move = new Vector3(horizontal, 0.0f, vertical).normalized;

            // ������ ����Ǵ� ������ �ӵ��� ������� ������ ��� �ӵ��� �����Դϴ�.
            return move * moveSpeed * Time.deltaTime;
        }
        else
        {
            isMoving = false;

            return Vector3.zero;
        }
    }
}
