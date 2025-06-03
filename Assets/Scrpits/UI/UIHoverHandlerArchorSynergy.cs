using UnityEngine;

public class UIHoverHandlerArchorSynergy : MonoBehaviour {
    private UIHoverHandler handler;

    private void Start() {
        handler = GetComponent<UIHoverHandler>();
        SetHandler(1);
        PlayerCretureManager.Instance.UnitSlotManager.OnArchorUnitChanged += SetHandler;
    }

    public void SetHandler(int archorAmount) {
        handler.Content = "모든 아군의 공격속도가 " + (1+ 0.1f * archorAmount) + " 배 만큼 증가합니다";
    }
}