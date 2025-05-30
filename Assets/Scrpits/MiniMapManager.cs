using UnityEngine;
using System.Collections.Generic;

public class MiniMapManager : MonoSingleton<MiniMapManager> {
    public GameObject roomIconPrefab;
    public GameObject linePrefab;
    public Transform iconParent;
    public Transform lineParent;
    public Vector2 iconSpacing = new Vector2(50f, 50f);

    Dictionary<Vector2Int, RectTransform> iconMap = new();

    public void RenderMiniMap(Dictionary<Vector2Int, RoomData> rooms, Vector2Int currentPos) {
        // 기존 방 아이콘 제거
        foreach (Transform child in iconParent) Destroy(child.gameObject);
        foreach (Transform child in lineParent) Destroy(child.gameObject);
        iconMap.Clear();

        // 아이콘 생성
        foreach (var kv in rooms) {
            Vector2Int gridPos = kv.Key;
            RoomData data = kv.Value;

            GameObject icon = Instantiate(roomIconPrefab, iconParent);
            Vector3 pos = new(gridPos.x * iconSpacing.x, gridPos.y * iconSpacing.y, 0);
            icon.transform.localPosition = pos;

            MiniMapRoomIcon roomIcon = icon.GetComponent<MiniMapRoomIcon>();
            roomIcon.SetType(data.type);
            roomIcon.SetHighlight(data.visited);

            iconMap[gridPos] = icon.GetComponent<RectTransform>();
        }

        // 연결선 생성
        foreach (var kv in rooms) {
            Vector2Int from = kv.Key;
            foreach (var to in kv.Value.connections) {
                // 중복 연결 방지 조건 (CompareTo 대체)
                if ((from.x < to.x) || (from.x == to.x && from.y < to.y)) {
                    if (iconMap.ContainsKey(to)) {
                        DrawLine(iconMap[from].localPosition, iconMap[to].localPosition);
                    }
                }
            }
        }
    }

    void DrawLine(Vector2 from, Vector2 to) {
        GameObject line = Instantiate(linePrefab, lineParent);
        RectTransform rt = line.GetComponent<RectTransform>();

        Vector2 dir = to - from;
        float length = dir.magnitude;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        rt.sizeDelta = new Vector2(length / 3, 3f);
        rt.localPosition = (from + to) * 0.5f;
        rt.localRotation = Quaternion.Euler(0, 0, angle);
    }

    public void MoveMapTo(Vector2Int newPlayerPos, MapManager mapManager) {
        Vector2Int curPos = mapManager.currentPlayerPos;

        if (!mapManager.rooms[curPos].connections.Contains(newPlayerPos)) {
            Debug.Log($"이동 불가: {curPos} → {newPlayerPos}는 연결되어 있지 않음");
            return;
        }

        mapManager.MoveMap(false, curPos, iconMap[curPos].GetComponent<MiniMapRoomIcon>());

        // 위치 갱신
        mapManager.currentPlayerPos = newPlayerPos;

        Vector3 offset = new Vector3(
            -newPlayerPos.x * iconSpacing.x,
            -newPlayerPos.y * iconSpacing.y,
            0f
        );

        iconParent.localPosition = offset;
        lineParent.localPosition = offset;

        mapManager.MoveMap(true, newPlayerPos, iconMap[newPlayerPos].GetComponent<MiniMapRoomIcon>());
    }
}
