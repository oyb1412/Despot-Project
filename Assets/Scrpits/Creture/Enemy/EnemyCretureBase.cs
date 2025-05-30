using UnityEngine;

public enum enemyCretureType {
    AxeGoblin,
    BurserkerGoblin,
    ShieldRizard,
    CatArhcor,
    CatCrossbow,
    CatGun,
    SkelletonMage,
    VampireMage,
    NecromenserMage,
    Count,
}

public abstract class EnemyCretureBase : CretureBase
{
    public override abstract void Attack(CretureBase my, CretureBase target);

    protected override void Awake() {
        base.Awake();
        IsPlayer = false;
    }
}
