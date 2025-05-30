using UnityEngine;

public class BurserkerGoblin : EnemyCretureBase {
    protected override void Awake() {
        base.Awake();
        AttackInterface = new MeleeAttack();
    }
    public override void Attack(CretureBase my, CretureBase target) {
        AttackInterface.Attack(my, target);
    }
}
