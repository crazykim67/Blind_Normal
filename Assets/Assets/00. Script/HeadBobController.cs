using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobController : MonoBehaviour
{
    [SerializeField]
    private PlayerController controller;
    [SerializeField]
    private StepController stepController;

    [SerializeField]
    private bool enable = true;

    // 진폭
    [SerializeField, Range(0, 0.1f)]
    private float amplitude = 0.015f;
    // 빈도
    [SerializeField, Range(0, 30)]
    private float frequency = 10f;

    [SerializeField]
    private Transform camTr = null;
    [SerializeField]
    private Transform camHolder = null;

    private CharacterController ch;

    [SerializeField]
    private float toggleSpeed = 3.0f;
    [SerializeField]
    private Vector3 startPos;
    [SerializeField]
    private float tolerance = 0.001f;

    private void Awake()
    {
        ch = GetComponent<CharacterController>();
        startPos = camTr.localPosition;
    }

    private void Update()
    {
        if (!enable)
            return;

        CheckMotion();
        ResetPosition();
        camTr.LookAt(FocusTarget());
        // 임시 주석
    }


    // 걷는 동작에 따른 헤드 밥 효과를 계산하는 메서드
    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        // 크기 제어
        pos.y += Mathf.Sin(Time.time * frequency) * amplitude;
        // 속도 제어
        pos.x += Mathf.Cos(Time.time * frequency / 2) * amplitude * 2;

        return pos;
    }

    // 플레이어의 움직임에 따라 카메라 위치를 조절하는 메서드
    private void PlayerMotion(Vector3 motion)
    {
        camTr.localPosition += motion;

        if(Mathf.Abs(camTr.localPosition.x - startPos.x) < tolerance)
            stepController.OnStep();
    }

    // 플레이어의 움직임을 확인하고, 일정 속도 이상일 때만 헤드 밥 효과를 적용하는 메서드
    public void CheckMotion()
    {
        float speed = new Vector3(controller.moveDir.x, 0, controller.moveDir.z).magnitude;

        if (speed < toggleSpeed)
            return;

        if (!ch.isGrounded)
            return;

        PlayerMotion(FootStepMotion());
    }

    // 헤드 밥 효과의 위치를 초기 위치로 부드럽게 되돌리는 메서드
    private void ResetPosition()
    {
        if (camTr.localPosition == startPos)
            return;

        camTr.localPosition = Vector3.Lerp(camTr.localPosition, startPos, 1 * Time.deltaTime);
    }

    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + camHolder.localPosition.y, transform.position.z);
        pos += camHolder.forward * 15.0f;
        return pos;
    }
}
