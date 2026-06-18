using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private bool isOpen = false;
    private bool isRotating = false;

    [SerializeField] private float openAngle = 120f;   // 목표 회전 각도
    [SerializeField] private float rotationSpeed = 2f; // 회전 속도

    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Awake()
    {
        // 시작할 때의 각도를 닫힌 상태(0도)로 기억
        closedRotation = transform.localRotation;
        // 닫힌 상태에서 Y축으로 120도 회전한 상태를 열린 상태로 계산
        openRotation = Quaternion.Euler(transform.localEulerAngles + new Vector3(0, openAngle, 0));
    }

    // ★ 이 함수가 추가되어 DoorAttachmentSystem과의 연동 에러가 해결됩니다.
    public void ToggleDoor()
    {
        if (isRotating) return; // 이미 회전 중이면 조작 불가

        isOpen = !isOpen;
        StartCoroutine(AnimateDoor(isOpen ? openRotation : closedRotation));
    }

    // 부드러운 회전을 위한 코루틴 함수
    private IEnumerator AnimateDoor(Quaternion targetRotation)
    {
        isRotating = true;
        float time = 0f;
        Quaternion startRotation = transform.localRotation;

        while (time < 1f)
        {
            time += Time.deltaTime * rotationSpeed;
            transform.localRotation = Quaternion.Slerp(startRotation, targetRotation, time);
            yield return null;
        }

        transform.localRotation = targetRotation; // 오차 보정
        isRotating = false;
    }

    // 탈착될 때 문이 열려있었다면 상태를 초기화해주는 안전장치
    public void ResetDoorState()
    {
        StopAllCoroutines();
        transform.localRotation = closedRotation;
        isOpen = false;
        isRotating = false;
    }
}