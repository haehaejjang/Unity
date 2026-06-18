using UnityEngine;

public class DoorAttachmentSystem : MonoBehaviour
{
    [Header("기본 상태 설정")]
    public bool isDoorActiveInitially = true;

    [Header("연결 설정")]
    public GameObject wallFiller;
    public GameObject doorPivot;
    public DoorController doorController;

    private bool isDoorActive;

    void Start()
    {
        isDoorActive = isDoorActiveInitially;
        UpdateState();
    }

    // ★ 플레이어의 인벤토리 컴포넌트를 매개변수로 받아서 조건을 검사합니다.
    public void SwapSlot(PlayerInventory inventory)
    {
        if (inventory == null) return;

        if (isDoorActive)
        {
            // [상황 A] 현재 문이 붙어있음 -> 문을 떼서 인벤토리에 넣어야 함
            if (inventory.HasDoor)
            {
                Debug.Log("<color=red>[경고]</color> 이미 문을 소지하고 있어서 더 뗄 수 없습니다!");
                return;
            }

            isDoorActive = false;
            UpdateState();
            inventory.AddDoor(); // 플레이어 가방에 문 넣어주기
            Debug.Log("성공: 문을 탈착하여 보관했습니다.");
        }
        else
        {
            // [상황 B] 현재 벽으로 막혀있음 -> 가방에서 문을 꺼내서 붙여야 함
            if (!inventory.HasDoor)
            {
                Debug.Log("<color=red>[경고]</color> 소지한 문이 없어서 벽에 문을 부착할 수 없습니다!");
                return;
            }

            isDoorActive = true;
            UpdateState();
            inventory.RemoveDoor(); // 플레이어 가방에서 문 빼기
            Debug.Log("성공: 소지한 문을 벽에 부착했습니다.");
        }
    }

    public void ToggleDoor()
    {
        if (isDoorActive && doorController != null)
        {
            doorController.ToggleDoor();
        }
    }

    private void UpdateState()
    {
        doorPivot.SetActive(isDoorActive);
        wallFiller.SetActive(!isDoorActive);

        if (!isDoorActive && doorController != null)
        {
            doorController.ResetDoorState();
        }
    }
}