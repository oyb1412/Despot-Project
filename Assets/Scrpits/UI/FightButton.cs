using UnityEngine;
using UnityEngine.UI;

public class FightButton : UI_Button
{
    private Outline outLine;
    public override void Init() {
        outLine = GetComponent<Outline>();  
        seletable = true;
    }

    public override void Select() {
        BattleStateManager.Instance.ChangeType(BattleStateType.Battle);
        UIManager.Instance.ToggleUI(UIType.FightToggle);
    }

    public void Update() {
        if (outLine == null) return;

        float alpha = Mathf.PingPong(Time.time, 1f);

        Color c = outLine.effectColor;
        c.a = alpha;
        outLine.effectColor = c;
    }
}
