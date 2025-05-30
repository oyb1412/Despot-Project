using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public class UnitSlotManager {
    public Dictionary<CretureBundle, GameObject[]> unitSlots = new();
    public Dictionary<CretureBundle, Vector2Int[]> unitPositions = new();

    public Action<int> OnTotalUnitChanged;
    public Action<int> OnMeleeUnitChanged;
    public Action<int> OnArchorUnitChanged;
    public Action<int> OnMageUnitChanged;

    public void Init(bool player) {
        unitSlots[CretureBundle.Melee] = new GameObject[4];
        unitSlots[CretureBundle.Archor] = new GameObject[4];
        unitSlots[CretureBundle.Mage] = new GameObject[4];

        if(player) {
            unitPositions[CretureBundle.Melee] = new Vector2Int[] {
            new(-2, 0), new(-2, -1), new(-2, -2), new(-2, -3)
        };

            unitPositions[CretureBundle.Archor] = new Vector2Int[] {
            new(-3, 0), new(-3, -1), new(-3, -2), new(-3, -3)
        };

            unitPositions[CretureBundle.Mage] = new Vector2Int[] {
            new(-4, 0), new(-4, -1), new(-4, -2), new(-4, -3)
        };
        }
        else {
            unitPositions[CretureBundle.Melee] = new Vector2Int[] {
            new(2, 0), new(2, -1), new(2, -2), new(2, -3)
        };

            unitPositions[CretureBundle.Archor] = new Vector2Int[] {
            new(3, 0), new(3, -1), new(3, -2), new(3, -3)
        };

            unitPositions[CretureBundle.Mage] = new Vector2Int[] {
            new(4, 0), new(4, -1), new(4, -2), new(4, -3)
        };
        }
    }

    public bool TryAddUnit(CretureBundle type, GameObject prefab, out GameObject createdUnit) {
        createdUnit = null;
        for (int i = 0; i < unitSlots[type].Length; i++) {
            if (unitSlots[type][i] == null) {
                createdUnit = GameObject.Instantiate(prefab);
                createdUnit.transform.position = new Vector3(unitPositions[type][i].x, unitPositions[type][i].y, 0);
                unitSlots[type][i] = createdUnit;
                OnTotalUnitChanged?.Invoke(GetTotalUnitCount());
                OnMeleeUnitChanged?.Invoke(GetUnitCount(CretureBundle.Melee));
                OnArchorUnitChanged?.Invoke(GetUnitCount(CretureBundle.Archor));
                OnMageUnitChanged?.Invoke(GetUnitCount(CretureBundle.Mage));
               
                return true;
            }
        }

        Debug.LogWarning($"{type} ½½·ÔÀÌ °¡µæ Ã¡½À´Ï´Ù.");
        return false;
    }

    public bool RemoveUnit(CretureBundle type, GameObject unit) {
        for (int i = 0; i < unitSlots[type].Length; i++) {
            if (unitSlots[type][i] == unit) {
                GameObject.Destroy(unit);
                unitSlots[type][i] = null;
                OnTotalUnitChanged?.Invoke(GetTotalUnitCount());
                OnMeleeUnitChanged?.Invoke(GetUnitCount(CretureBundle.Melee));
                OnArchorUnitChanged?.Invoke(GetUnitCount(CretureBundle.Archor));
                OnMageUnitChanged?.Invoke(GetUnitCount(CretureBundle.Mage));
                return true;
            }
        }

        Debug.LogWarning("ÇØ´ç À¯´ÖÀ» Ã£À» ¼ö ¾øÀ½");
        return false;
    }

    public int GetUnitCount(CretureBundle type) {
        return unitSlots[type].Count(slot => slot != null);
    }

    public GameObject[] GetAllUnit() {
        var allUnits = unitSlots.Values.SelectMany(arr => arr).Where(go => go != null);
        return allUnits.ToArray();
    }

    public int GetTotalUnitCount() {
        int count = 0;
        foreach (var kvp in unitSlots) {
            foreach (var unit in kvp.Value) {
                if (unit != null)
                    count++;
            }
        }
        return count;
    }

    public void SetDefaultPlayer(GameObject[] allPrefabs) {
        Init(true);

        foreach (CretureBundle type in Enum.GetValues(typeof(CretureBundle))) {
            if (type == CretureBundle.Count) continue;

            int baseIndex = (int)type * 3;
            int rand = UnityEngine.Random.Range(0, 3);
            GameObject prefab = allPrefabs[baseIndex + rand];

            TryAddUnit(type, prefab, out _);
        }
    }
}
