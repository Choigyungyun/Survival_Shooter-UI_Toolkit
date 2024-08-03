using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [NonSerialized] public Transform targetTransform;               // 따라다닐 카메라 목표 위치

    [Header("Camera Move Setting")]
    public bool cameraSmooth = true;                                // 부드러움 true / false
    public float cameraDelay = 0.0f;                                // 카메라 이동 딜레이

    [Header("Camera Fixed Transfrom Setting")]
    [SerializeField] private Vector3 setPosition = Vector3.zero;    // 유저 설정 위치
    [SerializeField] private Vector3 setRotation = Vector3.zero;    // 유저 설정 방향

    private Vector3 fixedPosition = Vector3.zero;                   // 이전 프레임 화면 고정 위치

    private void Start()
    {
        Camera.main.transform.rotation = Quaternion.Euler(setRotation);
    }

    /// <summary>
    /// 부드러운 화면 이동 업데이트
    /// </summary>
    /// <remarks>
    /// 카메라가 천천히 따라오기 때문에 캐릭터의 이동 생명주기와 동일하게
    /// 업데이트하여 캐릭터가 움직일 시 화면 흔들림 없이 이동할 수 있습니다.
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
    /// 고정된 화면 이동 업데이트
    /// </summary>
    /// <remarks>
    /// 카메라가 캐릭터의 이동의 생명주기보다 늦게 연산을 하여 끊어짐 없이
    /// 자연스럽게 따라가도록 합니다.
    /// </remarks>
    private void LateUpdate()
    {
        if (targetTransform == null || cameraSmooth) return;

        Camera.main.transform.position = targetTransform.position + setPosition;
    }
}
