using UnityEngine;

public enum UIType {
    FightToggle,
}
public class UIManager : MonoSingleton<UIManager>
{
    public GameObject FightButton;
    public GameLog gameLog;
    public HoverUi hoverUI;
    public Canvas WorldCanvas;

    public void SetHoverUI(bool isHover, string content, Vector3 pos) {
        hoverUI.SetHoverUI(isHover, content, pos);
    }

    public void SetGameLog(string content) {
        gameLog.SetLog(content);
    }

    public void ToggleUI(UIType type) {
        switch(type) {
            case UIType.FightToggle:
                bool currentState = FightButton.activeSelf;
                FightButton.SetActive(!currentState);

                break;
        }
    }
}
