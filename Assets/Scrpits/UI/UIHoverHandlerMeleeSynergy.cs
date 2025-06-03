using UnityEngine;

public class UIHoverHandlerMeleeSynergy : MonoBehaviour {
    private UIHoverHandler handler;

    private void Start() {
        handler = GetComponent<UIHoverHandler>();
        SetHandler(1);
        PlayerCretureManager.Instance.UnitSlotManager.OnMeleeUnitChanged += SetHandler;
    }

    public void SetHandler(int meleeAmount) {
        handler.Content = "모든 아군의 공격력이 " + (1 + 0.1f * meleeAmount) + " 배 만큼 증가합니다";
    }
}