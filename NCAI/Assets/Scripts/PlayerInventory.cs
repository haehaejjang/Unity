using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("인벤토리 상태")]
    [Tooltip("현재 플레이어가 문 아이템을 가지고 있는지 여부")]
    [SerializeField] private bool hasDoor = false;

    // 외부(다른 스크립트)에서 문을 가지고 있는지 읽을 수만 있게 안전하게 제공하는 프로퍼티
    public bool HasDoor => hasDoor;

    // 문을 획득했을 때 호출할 함수
    public void AddDoor()
    {
        hasDoor = true;
        Debug.Log("<color=cyan>[인벤토리]</color> 문을 가방에 넣었습니다.");
    }

    // 문을 사용(부착)했을 때 호출할 함수
    public void RemoveDoor()
    {
        hasDoor = false;
        Debug.Log("<color=yellow>[인벤토리]</color> 가방에서 문을 꺼냈습니다.");
    }
}