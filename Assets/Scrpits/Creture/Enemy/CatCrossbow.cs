using UnityEngine;

public class CatCrossbow : EnemyCretureBase {
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
