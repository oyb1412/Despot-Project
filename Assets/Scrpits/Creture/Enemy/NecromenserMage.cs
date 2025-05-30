using UnityEngine;

public class NecromenserMage : EnemyCretureBase {

    protected override void Awake() {
        base.Awake();
        AttackInterface = new NecromenserMagic();
    }
    public override void Attack(CretureBase my, CretureBase target) {
        AttackInterface.Attack(my, target);
    }
}

