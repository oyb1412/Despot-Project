using UnityEngine;

public class IcemageMan : PlayerCretureBase
{
    public GameObject projectilePrefab;
    protected override void Awake() {
        base.Awake();
        Bundle = CretureBundle.Mage;
        AttackInterface = new RangeAttack(projectilePrefab);
    }

    public override void Attack(CretureBase my, CretureBase target) {
        AttackInterface.Attack(my, target);
    }
}
