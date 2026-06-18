using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovements : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 5f;

    [Header("점프 설정")]
    public float jumpForce = 5f;       // 점프 힘
    public Transform groundCheck;     // 바닥을 체크할 피벗 위치 (플레이어 발바닥 부근)
    public float groundDistance = 0.2f; // 바닥 감지 구체의 반지름
    public LayerMask groundMask;      // 어떤 레이어를 바닥으로 인식할 것인가 (Ground 레이어 지정용)

    private Rigidbody rb;
    private float moveX = 0f;
    private float moveZ = 0f;
    private bool isGrounded;          // 현재 바닥에 닿아있는지 여부
    private bool jumpRequested = false; // 점프 입력 신호 변수

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        // 1. WASD 이동 입력 처리
        moveX = 0f;
        if (Keyboard.current.dKey.isPressed) moveX = 1f;
        if (Keyboard.current.aKey.isPressed) moveX = -1f;

        moveZ = 0f;
        if (Keyboard.current.wKey.isPressed) moveZ = 1f;
        if (Keyboard.current.sKey.isPressed) moveZ = -1f;

        // 2. 바닥에 닿아있고, 스페이스바를 누르는 순간 점프 요청 신호를 켬
        if (isGrounded && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            jumpRequested = true;
        }
    }

    void FixedUpdate()
    {
        // 3. 물리 기반 바닥 체크
        // groundCheck 위치를 중심으로 groundDistance 반지름만큼의 가상의 구를 그려 groundMask 레이어가 걸리는지 확인
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        Move();

        // 4. 점프 요청이 있었다면 물리 연산 주기(FixedUpdate)에서 힘을 가함
        if (jumpRequested)
        {
            // Y축 속도만 jumpForce로 순간적으로 치올려줌
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            jumpRequested = false; // 요청 처리 완료 후 리셋
        }
    }

    void Move()
    {
        Vector3 moveDirection = (transform.forward * moveZ) + (transform.right * moveX);
        if (moveDirection.magnitude > 1f)
        {
            moveDirection.Normalize();
        }

        rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed, rb.linearVelocity.y, moveDirection.z * moveSpeed);
    }

    // 에디터 씬 뷰에서 바닥 체크 범위가 잘 잡히는지 빨간색 구체로 시각화
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
        }
    }
}