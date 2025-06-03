using TMPro;
using UnityEngine;

public class GoldPrefab : MonoBehaviour
{
    public int RewardGold;
    public TextMeshProUGUI rewardTMP;
    private float baseY;

    public void Init(int amount, Vector2Int pos) {
        RewardGold = amount;
        rewardTMP.text = amount.ToString();
        transform.position = new Vector3(pos.x, pos.y, 0);
        baseY = transform.position.y;
    }
    void Update()
    {
        float offset = Mathf.PingPong(Time.time * 0.5f, 0.2f);
        transform.position = new Vector3(transform.position.x, baseY + offset, transform.position.z);
    }
}
