using System;
using UnityEngine;

public class PlayerCretureManager : MonoSingleton<PlayerCretureManager>
{
    public GameObject[] PlayerCreturePrefabs;
    public UnitSlotManager UnitSlotManager = new();

   
    private void Start() {
        LoadPrefabs();
        UnitSlotManager.SetDefaultPlayer(PlayerCreturePrefabs);
    }

    

    private void LoadPrefabs() {
        var types = Enum.GetValues(typeof(playerCretureType));
        PlayerCreturePrefabs = new GameObject[types.Length];

        foreach (playerCretureType type in types) {
            string path = $"Prefabs/PlayerCreture/{type}";
            GameObject prefab = Resources.Load<GameObject>(path);

            if (prefab != null) {
                PlayerCreturePrefabs[(int)type] = prefab;
            } else {
                Debug.LogWarning($"[PlayerCretureManager] �������� ã�� �� ����: {path}");
            }
        }

      
    }
}
