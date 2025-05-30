using UnityEngine;
public enum playerCretureType {
    KnightMan,
    AxeMan,
    ShieldMan,
    ArchorMan,
    CrossbowMan,
    GunMan,
    FiremageMan,
    IcemageMan,
    PreistMan,
    Count,
}



public abstract class PlayerCretureBase : CretureBase
{
    public override abstract void Attack(CretureBase my, CretureBase targe);

    protected override void Awake() {
        base.Awake();
        IsPlayer = true;
    }
}
