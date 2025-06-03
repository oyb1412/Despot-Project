using UnityEngine;

public class UIHoverHandlerMageSynergy : MonoBehaviour {
    private UIHoverHandler handler;

    private void Start() {
        handler = GetComponent<UIHoverHandler>();
        SetHandler(1);
        PlayerCretureManager.Instance.UnitSlotManager.OnMageUnitChanged += SetHandler;
    }

    public void SetHandler(int mageAmount) {
        handler.Content = "��� �Ʊ��� ġ��Ÿ Ȯ���� " + (1+ 0.1f * mageAmount) + " �� ��ŭ �����մϴ�";
    }
}