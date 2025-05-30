using UnityEngine;

public class AxeMan : PlayerCretureBase
{
    protected override void Awake() {
        base.Awake();
        AttackInterface = new MeleeAttack();
    }

    public override void Attack(CretureBase my, CretureBase target) {
        AttackInterface.Attack(my, target);
    }
}
