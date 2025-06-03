using UnityEngine;

public enum RewardType {
    Gold,
    Food,
    Unit,
    Relic,
}
public class RewardManager : MonoSingleton<RewardManager>
{
    public GameObject GoldRewardPrefab;
    public GameObject FoodRewardPrefab;
    public GameObject[] UnitRewardPrefabs;
    public GameObject[] RelicRewardPrefabs;

    public int RelicChance;
    public void SetReward() {
        int ran = Random.Range(0, 100);
        if(ran < RelicChance) {
            //∑º∏Ø µÓ¿Â
            var ranRelic = Random.Range(0, RelicRewardPrefabs.Length);
            var relic = Instantiate(RelicRewardPrefabs[ranRelic]).GetComponent<RelicRewardPrefab>();
            relic.Init( (RelicType)ranRelic, new Vector2Int(5, -1));
        }
        else
        {
            //¿Ø¥÷ µÓ¿Â
            var ranUnit = Random.Range(0, UnitRewardPrefabs.Length);
            var unit = Instantiate(UnitRewardPrefabs[ranUnit]).GetComponent<UnitRewardPrefab>();
            unit.Init(FloorManager.Instance.CurrentFloor,(playerCretureType)ranUnit,  new Vector3(5, -1.4f, 0));
        }
        var gold = Instantiate(GoldRewardPrefab).GetComponent<GoldPrefab>();
        gold.Init(30 * FloorManager.Instance.CurrentFloor, new Vector2Int(3, -1));
        var food = Instantiate(FoodRewardPrefab).GetComponent<FoodPrefab>();
        food.Init(10 * FloorManager.Instance.CurrentFloor, new Vector2Int(4, -1));
        //¡¬«• 3,-1  4,-1  5,-1
    }

    public void GetReward() {

    }
}
