using System;
using UnityEngine;

public class EnemyCretureManager : MonoSingleton<EnemyCretureManager>
{
    public GameObject[] EnemyCreturePrefabs;
    public UnitSlotManager UnitSlotManager = new();

    private void Start() {
        LoadPrefabs();
        UnitSlotManager.Init(false);
        UnitSlotManager.TryAddUnit(CretureBundle.Melee, EnemyCreturePrefabs[0], out var unit);
    }

    private void LoadPrefabs() {
        var types = Enum.GetValues(typeof(enemyCretureType));
        EnemyCreturePrefabs = new GameObject[types.Length];

        foreach (enemyCretureType type in types) {
            string path = $"Prefabs/EnemyCreture/{type}";
            GameObject prefab = Resources.Load<GameObject>(path);

            if (prefab != null) {
                EnemyCreturePrefabs[(int)type] = prefab;
            } else {
                Debug.LogWarning($"[EnemyCretureManager] 프리팹을 찾을 수 없음: {path}");
            }
        }
    }
}
