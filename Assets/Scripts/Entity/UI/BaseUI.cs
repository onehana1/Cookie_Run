using UnityEngine;

// BaseUI 클래스는 모든 UI 클래스에 상속되어 게임 내 모든 UI에 대한 공통 인터페이스 및 구조를 갖습니다.
public abstract class BaseUI : MonoBehaviour
{
    // 다른 UI 및 게임 관리자와 통신하기 위한 UIManager 참조
    protected UIManager uiManager;

    // UI 초기화
    public virtual void Init(UIManager uiManager)
    {
        this.uiManager = uiManager;
    }

    // UI 활성화 상태 설정
    protected abstract UIState GetUIState();

    // UI 활성화 상태에 따라 UI 활성화 여부 설정
    public void SetActive(UIState state)
    {
        this.gameObject.SetActive(GetUIState() == state);
    }
}