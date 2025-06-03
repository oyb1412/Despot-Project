using UnityEngine;

public class UIHoverHandlerMeleeSynergy : MonoBehaviour {
    private UIHoverHandler handler;

    private void Start() {
        handler = GetComponent<UIHoverHandler>();
        SetHandler(1);
        PlayerCretureManager.Instance.UnitSlotManager.OnMeleeUnitChanged += SetHandler;
    }

    public void SetHandler(int meleeAmount) {
        handler.Content = "��� �Ʊ��� ���ݷ��� " + (1 + 0.1f * meleeAmount) + " �� ��ŭ �����մϴ�";
    }
}