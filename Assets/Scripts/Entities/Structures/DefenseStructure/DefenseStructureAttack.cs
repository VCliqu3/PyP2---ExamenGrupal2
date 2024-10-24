using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseStructureAttack : EntityAttack
{
    [Header("Defense Structure Specifics")]
    [SerializeField] private DefenseStructureSO defenseStructureSO;

    public DefenseStructureSO DefenseStructureSO => defenseStructureSO;

    protected override void SetAttackCooldown() => attackCooldown = 1 / defenseStructureSO.attackSpeed;

    protected override bool CanAttackEntity(Entity entityToAttack)
    {
        if (entity.IsAlied == entityToAttack.IsAlied) return false;

        return true;
    }

    protected override Entity FindTarget()
    {
        //CheckForEntities in any node
        return null;
    }

    protected override void Attack(Entity entity)
    {
        Transform projectileTransform = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        ProjectileHandler projectileHandler = projectileTransform.GetComponent<ProjectileHandler>();

        if (projectileHandler == null)
        {
            Debug.Log("Instantiated Projectile does not contain a ProjectileHandler component");
            return;
        }

        projectileHandler.SetTarget(entity.transform);
        projectileHandler.SetDamage(defenseStructureSO.damage);
        projectileHandler.SetSpeed(defenseStructureSO.projectileSpeed);
    }
}
