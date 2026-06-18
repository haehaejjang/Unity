using UnityEngine;
using UnityEngine.InputSystem; // ★ 이 줄이 반드시 추가되어야 합니다!

public class MouseLook : MonoBehaviour
{
    [Header("마우스 감도")]
    // 새로운 입력 시스템은 마우스 이동량(픽셀)을 그대로 가져오기 때문에 
    // 예전 방식보다 감도 숫자를 좀 작게(예: 10~20) 설정하는 것이 좋습니다.
    public float mouseSensitivity = 15f; 

    public Transform playerBody;
    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 마우스 장치가 연결되어 있는지 확인 (에러 방지)
        if (Mouse.current == null) return;

        // ★ New Input System 방식으로 마우스 X, Y 이동량 받아오기
        float mouseX = Mouse.current.delta.x.ReadValue() * mouseSensitivity * Time.deltaTime;
        float mouseY = Mouse.current.delta.y.ReadValue() * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}