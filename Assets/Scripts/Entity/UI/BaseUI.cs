using UnityEngine;

// BaseUI Ŭ������ ��� UI Ŭ������ ��ӵǾ� ���� �� ��� UI�� ���� ���� �������̽� �� ������ �����ϴ�.
public abstract class BaseUI : MonoBehaviour
{
    // �ٸ� UI �� ���� �����ڿ� ����ϱ� ���� UIManager ����
    protected UIManager uiManager;

    // UI �ʱ�ȭ
    public virtual void Init(UIManager uiManager)
    {
        this.uiManager = uiManager;
    }

    // UI Ȱ��ȭ ���� ����
    protected abstract UIState GetUIState();

    // UI Ȱ��ȭ ���¿� ���� UI Ȱ��ȭ ���� ����
    public void SetActive(UIState state)
    {
        this.gameObject.SetActive(GetUIState() == state);
    }
}