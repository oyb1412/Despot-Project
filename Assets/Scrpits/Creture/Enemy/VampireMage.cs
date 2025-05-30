using UnityEngine;

public class VampireMage : EnemyCretureBase {
    public GameObject projectilePrefab;

    protected override void Awake() {
        base.Awake();
        AttackInterface = new VampireMagic(projectilePrefab);
    }
    public override void Attack(CretureBase my, CretureBase target) {
        AttackInterface.Attack(my, target);
    }
}
