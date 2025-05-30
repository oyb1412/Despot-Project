using UnityEngine;

public class PreistMan : PlayerCretureBase
{
    public GameObject projectilePrefab;
    protected override void Awake() {
        base.Awake();
        AttackInterface = new PreistMagic();
    }

    public override void Attack(CretureBase my, CretureBase target) {
        AttackInterface.Attack(my, target);
    }
}
