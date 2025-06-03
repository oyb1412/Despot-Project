using UnityEngine;

public enum RelicType {
    Water,
    Count,
}
public class RelicRewardPrefab : MonoBehaviour
{
    private float baseY;
    public RelicType RelicType;
    public void Init(RelicType type, Vector2Int pos) {
        RelicType = type;
        transform.position = new Vector3(pos.x, pos.y, 0);
        baseY = transform.position.y;
    }

    void Update() {
        float offset = Mathf.PingPong(Time.time * 0.5f, 0.2f);
        transform.position = new Vector3(transform.position.x, baseY + offset, transform.position.z);
    }
}
