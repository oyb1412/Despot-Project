using UnityEngine;

public class FloorManager : MonoSingleton<FloorManager>
{
    void Start()
    {
        MapManager.Instance.GenerateMap();
        MapManager.Instance.tilemap.gameObject.SetActive(true);
        MapManager.Instance.CarveConnections(MapManager.Instance.rooms[MapManager.Instance.currentPlayerPos]);
    }

    public void MoveFloor() {
        MapManager.Instance.GenerateMap();
    }


}
