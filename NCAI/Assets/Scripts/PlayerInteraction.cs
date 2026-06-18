using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [Header("상호작용 범위 설정")]
    [SerializeField] private float interactRadius = 2.5f;

    // 플레이어 본인의 인벤토리 컴포넌트 저장용 변수
    private PlayerInventory playerInventory;

    void Start()
    {
        // 시작할 때 내 몸(Player)에 붙어있는 인벤토리 스크립트를 찾아옵니다.
        playerInventory = GetComponent<PlayerInventory>();
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        // 1. [E] 키 입력 시: 문 여닫기 시도
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            TryAction(isOpenDoorAction: true);
        }

        // 2. [F] 키 입력 시: 문 탈부착(스왑) 시도
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            TryAction(isOpenDoorAction: false);
        }
    }

    private void TryAction(bool isOpenDoorAction)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactRadius);
        
        foreach (var col in hitColliders)
        {
            DoorAttachmentSystem doorSystem = col.GetComponentInParent<DoorAttachmentSystem>();
            
            if (doorSystem != null)
            {
                if (isOpenDoorAction)
                {
                    doorSystem.ToggleDoor(); 
                }
                else
                {
                    // ★ F키를 누르면 내 인벤토리 정보(playerInventory)를 벽에 넘겨줍니다.
                    doorSystem.SwapSlot(playerInventory);   
                }
                break; 
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}