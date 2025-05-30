using UnityEngine;

public class VampireMagic : AttackInterface
{
    private GameObject projectilePrefab;
    public VampireMagic(GameObject projectilePrefab) {
        this.projectilePrefab = projectilePrefab;
    }
    public void Attack(CretureBase my, CretureBase targe) {
    }

   
}
