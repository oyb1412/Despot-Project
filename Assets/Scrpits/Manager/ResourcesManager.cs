using System;
using UnityEngine;

public class ResourcesManager : MonoSingleton<ResourcesManager>
{
    private int MaxFood = 999;
    private int MaxGold = 999999;
    private int _currentGold;
    public int CurrentGold {
        get => _currentGold;
        set {
            if (_currentGold != value) {
                _currentGold = value;
                OnGoldChanged?.Invoke(_currentGold);
            }
        }
    }

    private int _currentFood;
    public int CurrentFood {
        get => _currentFood;
        set {
            if (_currentFood != value) {
                _currentFood = value;
                OnFoodChanged?.Invoke(_currentFood);
            }
        }
    }

    public Action<int> OnGoldChanged;
    public Action<int> OnFoodChanged;

    private void Start() {
        CurrentFood = 20;
        CurrentGold = 50;
    }

    public void UseFood(int amount) {
        CurrentFood -= amount;
        if(CurrentFood <= 0) {
            BattleStateManager.Instance.ChangeType(BattleStateType.Lose);
        }
    }

    public void GetFood(int amount) {
        if (CurrentFood + amount > MaxFood) {
            CurrentFood = MaxFood;
        }
        else {
            CurrentFood += amount;
        }
    }

    public void UseGold(int amount) {
        if(CurrentGold < amount) {
            //°ñµå ºÎÁ· ¿¡·¯
            Debug.Log("°ñµå ºÎÁ·");
        }
        else {
            CurrentGold -= amount;
        }
    }

    public void GetGold(int amount) {
        if (CurrentGold + amount > MaxGold) {
            CurrentGold = MaxGold;
        } else {
            CurrentGold += amount;
        }
    }
}
