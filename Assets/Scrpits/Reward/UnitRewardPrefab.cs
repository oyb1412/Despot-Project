using TMPro;
using UnityEngine;

public class UnitRewardPrefab : MonoBehaviour
{
    public TextMeshProUGUI levelTMP;
    public int RewardLevel;
    public playerCretureType CretureType;
    private float baseY;

    public void Init(int level, playerCretureType type, Vector3 pos) {
        RewardLevel = level;
        CretureType = type;
        levelTMP.text = "Lv." + level.ToString();
        transform.position = pos;
        baseY = transform.position.y;
    }

    void Update() {
        float offset = Mathf.PingPong(Time.time * 0.5f, 0.2f);
        transform.position = new Vector3(transform.position.x, baseY + offset, transform.position.z);
    }
}
