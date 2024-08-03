using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [NonSerialized] public Transform targetTransform;               // ����ٴ� ī�޶� ��ǥ ��ġ

    [Header("Camera Move Setting")]
    public bool cameraSmooth = true;                                // �ε巯�� true / false
    public float cameraDelay = 0.0f;                                // ī�޶� �̵� ������

    [Header("Camera Fixed Transfrom Setting")]
    [SerializeField] private Vector3 setPosition = Vector3.zero;    // ���� ���� ��ġ
    [SerializeField] private Vector3 setRotation = Vector3.zero;    // ���� ���� ����

    private Vector3 fixedPosition = Vector3.zero;                   // ���� ������ ȭ�� ���� ��ġ

    private void Start()
    {
        Camera.main.transform.rotation = Quaternion.Euler(setRotation);
    }

    /// <summary>
    /// �ε巯�� ȭ�� �̵� ������Ʈ
    /// </summary>
    /// <remarks>
    /// ī�޶� õõ�� ������� ������ ĳ������ �̵� �����ֱ�� �����ϰ�
    /// ������Ʈ�Ͽ� ĳ���Ͱ� ������ �� ȭ�� ��鸲 ���� �̵��� �� �ֽ��ϴ�.
    /// </remarks>
    private void FixedUpdate()
    {
        if (targetTransform == null || !cameraSmooth) return;

        fixedPosition = new Vector3(
                   targetTransform.position.x + setPosition.x,
                   targetTransform.position.y + setPosition.y,
                   targetTransform.position.z + setPosition.z);
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, fixedPosition, Time.deltaTime * cameraDelay);
    }

    /// <summary>
    /// ������ ȭ�� �̵� ������Ʈ
    /// </summary>
    /// <remarks>
    /// ī�޶� ĳ������ �̵��� �����ֱ⺸�� �ʰ� ������ �Ͽ� ������ ����
    /// �ڿ������� ���󰡵��� �մϴ�.
    /// </remarks>
    private void LateUpdate()
    {
        if (targetTransform == null || cameraSmooth) return;

        Camera.main.transform.position = targetTransform.position + setPosition;
    }
}
