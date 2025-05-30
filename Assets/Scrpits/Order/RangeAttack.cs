using UnityEngine;

public class RangeAttack : AttackInterface
{
    private GameObject projectilePrefab;
    public RangeAttack(GameObject projectilePrefab) {
        if (projectilePrefab == null)
            return;

        this.projectilePrefab = projectilePrefab;
    }

    public void Attack(CretureBase my, CretureBase targe) {
        //프로젝타일 발사
    }
}
