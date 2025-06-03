using UnityEngine;

public class UIHoverHandlerArchorSynergy : MonoBehaviour {
    private UIHoverHandler handler;

    private void Start() {
        handler = GetComponent<UIHoverHandler>();
        SetHandler(1);
        PlayerCretureManager.Instance.UnitSlotManager.OnArchorUnitChanged += SetHandler;
    }

    public void SetHandler(int archorAmount) {
        handler.Content = "��� �Ʊ��� ���ݼӵ��� " + (1+ 0.1f * archorAmount) + " �� ��ŭ �����մϴ�";
    }
}