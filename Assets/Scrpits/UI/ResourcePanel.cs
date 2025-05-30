using TMPro;
using UnityEngine;

public class ResourcePanel : MonoBehaviour
{
    public TextMeshProUGUI goldTmp;
    public TextMeshProUGUI peopleTmp;
    public TextMeshProUGUI foodTmp;
    public TextMeshProUGUI meleeTmp;
    public TextMeshProUGUI acrhorTmp;
    public TextMeshProUGUI mageTmp;


    private void Awake() {
        ResourcesManager.Instance.OnGoldChanged += ((gold)=> goldTmp.text = gold.ToString("D6"));
        ResourcesManager.Instance.OnFoodChanged += ((food)=> foodTmp.text = food.ToString("D3"));
        PlayerCretureManager.Instance.UnitSlotManager.OnTotalUnitChanged += ((unit) => peopleTmp.text = unit.ToString("D2"));
        PlayerCretureManager.Instance.UnitSlotManager.OnMeleeUnitChanged += ((melee) => meleeTmp.text = melee.ToString() + "/4");
        PlayerCretureManager.Instance.UnitSlotManager.OnArchorUnitChanged += ((archor) => acrhorTmp.text = archor.ToString() + "/4");
        PlayerCretureManager.Instance.UnitSlotManager.OnMageUnitChanged += ((mage) => mageTmp.text = mage.ToString() + "/4");
    }
}
