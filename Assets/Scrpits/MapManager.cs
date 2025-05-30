using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum RoomType {
    Combat,
    Shop,
    Boss,
    Reward,
}

public class RoomData {
    public Vector2Int position;
    public RoomType type;
    public bool visited;
    public List<Vector2Int> connections = new();

    public RoomData(Vector2Int pos) {
        position = pos;
        visited = false;
        type = RoomType.Combat;
    }

    public override string ToString() {
        return $"Room ({type}) at {position} → [{string.Join(", ", connections)}]";
    }
}

public class MapManager : MonoSingleton<MapManager>
{
    public Tilemap tilemap;
    public Dictionary<Vector2Int, RoomData> rooms = new();
    public Vector2Int currentPlayerPos = Vector2Int.zero;
    Dictionary<Vector3Int, TileBase> defaultTileCache = new();
    public Tile groundTile;

    void Update() {
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            MiniMapManager.Instance.MoveMapTo(currentPlayerPos + Vector2Int.right, this);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            MiniMapManager.Instance.MoveMapTo(currentPlayerPos + Vector2Int.left, this);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            MiniMapManager.Instance.MoveMapTo(currentPlayerPos + Vector2Int.up, this);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            MiniMapManager.Instance.MoveMapTo(currentPlayerPos + Vector2Int.down, this);
        }
    }

    public void GenerateMap() {
        rooms.Clear();
        HashSet<Vector2Int> used = new();

        int roomCount = Random.Range(5, 9); // 5~8개 방
        Vector2Int startPos = Vector2Int.zero;
        rooms[startPos] = new RoomData(startPos);
        rooms[startPos].type = RoomType.Reward; // 시작방 = 보상방
        rooms[startPos].visited = true;
        used.Add(startPos);

        List<Vector2Int> candidates = new() { startPos };
        Vector2Int[] directions = { Vector2Int.right, Vector2Int.up, Vector2Int.down };

        while (rooms.Count < roomCount) {
            var basePos = candidates[Random.Range(0, candidates.Count)];
            var dir = directions[Random.Range(0, directions.Length)];
            var newPos = basePos + dir;

            if (!used.Contains(newPos)) {
                RoomData newRoom = new(newPos);
                rooms[newPos] = newRoom;
                rooms[basePos].connections.Add(newPos);
                newRoom.connections.Add(basePos);
                used.Add(newPos);
                candidates.Add(newPos);
            }
        }

        var roomList = new List<Vector2Int>(rooms.Keys);

        Vector2Int bossPos = GetFarthestRoom(startPos);
        rooms[bossPos].type = RoomType.Boss;

        List<Vector2Int> availableForShop = new(roomList);
        availableForShop.Remove(bossPos);
        availableForShop.Remove(startPos);

        Vector2Int shopPos = availableForShop[Random.Range(0, availableForShop.Count)];
        rooms[shopPos].type = RoomType.Shop;

        MiniMapManager.Instance.RenderMiniMap(rooms, currentPlayerPos);

        CacheDefaultTiles(startPos);
    }

    public void CarveConnections(RoomData room) {
        RestoreDefaultTiles();

        if (room.type == RoomType.Boss)
            return;

        foreach (var connected in room.connections) {
            Vector2Int dir = connected - room.position;


            if (dir == Vector2Int.right) {
                // 오른쪽 벽 지우기 (대충 예시 좌표)
                tilemap.SetTile(new Vector3Int(7, -1, 0), groundTile);
                tilemap.SetTile(new Vector3Int(7, -2, 0), groundTile);
                tilemap.SetTile(new Vector3Int(8, -1, 0), groundTile);
                tilemap.SetTile(new Vector3Int(8, -2, 0), groundTile);
            } else if (dir == Vector2Int.left) {
                tilemap.SetTile(new Vector3Int(-8, -1, 0), groundTile);
                tilemap.SetTile(new Vector3Int(-8, -2, 0), groundTile);
                tilemap.SetTile(new Vector3Int(-9, -1, 0), groundTile);
                tilemap.SetTile(new Vector3Int(-9, -2, 0), groundTile);
            } else if (dir == Vector2Int.up) {
                tilemap.SetTile(new Vector3Int(-1, 2, 0), groundTile);
                tilemap.SetTile(new Vector3Int(0, 2, 0), groundTile);
                tilemap.SetTile(new Vector3Int(-1, 3, 0), groundTile);
                tilemap.SetTile(new Vector3Int(0, 3, 0), groundTile);               
                tilemap.SetTile(new Vector3Int(-1, 4, 0), groundTile);
                tilemap.SetTile(new Vector3Int(0, 4, 0), groundTile);
            } else if (dir == Vector2Int.down) {
                tilemap.SetTile(new Vector3Int(-1, -5, 0), groundTile);
                tilemap.SetTile(new Vector3Int(0, -5, 0), groundTile);
            }
        }
    }

    Vector2Int GetFarthestRoom(Vector2Int start) {
        Queue<(Vector2Int, int)> q = new();
        HashSet<Vector2Int> visited = new();
        q.Enqueue((start, 0));
        visited.Add(start);

        Vector2Int farthest = start;
        int maxDist = 0;

        while (q.Count > 0) {
            var (pos, dist) = q.Dequeue();
            if (dist > maxDist) {
                maxDist = dist;
                farthest = pos;
            }

            foreach (var neighbor in rooms[pos].connections) {
                if (!visited.Contains(neighbor)) {
                    visited.Add(neighbor);
                    q.Enqueue((neighbor, dist + 1));
                }
            }
        }

        return farthest;
    }

    public void MoveMap(bool vis, Vector2Int pos, MiniMapRoomIcon miniMap) {
        if (rooms.ContainsKey(pos))
            rooms[pos].visited = vis;

         CarveConnections(rooms[pos]);

        miniMap.SetHighlight(vis);
    }

    void PrintMapLog() {
        Debug.Log("=== 생성된 맵 ===");
        foreach (var kv in rooms) {
            Debug.Log(kv.Value);
        }
    }

    void CacheDefaultTiles(Vector2Int origin) {
        for (int x = tilemap.cellBounds.xMin; x < tilemap.cellBounds.xMax; x++) {
            for (int y = tilemap.cellBounds.yMin; y < tilemap.cellBounds.yMax; y++) {
                Vector3Int pos =  new Vector3Int(x + origin.x, y + origin.y, 0);
                var tile = tilemap.GetTile(pos);
                defaultTileCache[pos] = tile;
            }
        }
    }

    void RestoreDefaultTiles() {
        Tilemap defaultMap = tilemap;

        foreach (var kvp in defaultTileCache) {
            defaultMap.SetTile(kvp.Key, kvp.Value);
        }
    }
}
