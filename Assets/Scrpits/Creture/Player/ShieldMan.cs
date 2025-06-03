using UnityEngine;

public class ShieldMan : PlayerCretureBase
{
    protected override void Awake() {
        base.Awake();
        Bundle = CretureBundle.Melee;
        AttackInterface = new MeleeAttack();
    }

    public override void Attack(CretureBase my, CretureBase target) {
        AttackInterface.Attack(my, target);
    }
}
