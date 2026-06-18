using UnityEngine;

public interface IInteractable
{
    string GetInteractPrompt(); // 플레이어 화면에 띄울 안내 문구 (예: "[E] 문 부착하기")
    void Interact();            // 상호작용 실행 함수
}
