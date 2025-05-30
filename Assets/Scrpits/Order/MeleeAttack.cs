using UnityEngine;

public class MeleeAttack : AttackInterface
{
    public void Attack(CretureBase my, CretureBase target) {
        target.TakeDamage(my.AttackPower);
    }
}
