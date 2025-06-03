using UnityEngine;

public class CrossbowMan : PlayerCretureBase
{
    public GameObject projectilePrefab;
    protected override void Awake() {
        base.Awake();
        Bundle = CretureBundle.Archor;
        AttackInterface = new RangeAttack(projectilePrefab);
    }

    public override void Attack(CretureBase my, CretureBase target) {
        AttackInterface.Attack(my, target);
    }
}
