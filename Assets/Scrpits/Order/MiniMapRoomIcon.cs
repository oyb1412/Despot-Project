using UnityEngine;
using UnityEngine.UI;

public class MiniMapRoomIcon : MonoBehaviour {
    public Image iconImage;
    public Outline outlineEffect;
    public Color combatColor = Color.gray;
    public Color shopColor = Color.blue;
    public Color bossColor = Color.red;
    public Color rewardColor = Color.yellow;

    public void SetType(RoomType type) {
        switch (type) {
            case RoomType.Combat: iconImage.color = combatColor; break;
            case RoomType.Shop: iconImage.color = shopColor; break;
            case RoomType.Boss: iconImage.color = bossColor; break;
            case RoomType.Reward: iconImage.color = rewardColor; break; 
        }
    }

    public void SetHighlight(bool active) {
        outlineEffect.enabled = active;
    }
}
