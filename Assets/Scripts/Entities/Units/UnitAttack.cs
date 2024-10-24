using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitAttack : EntityAttack
{
    [Header("Unit Specifics")]
    [SerializeField] protected UnitSO unitSO;
    [SerializeField] protected UnitMovement unitMovement;

    public UnitSO UnitSO => unitSO;
    public UnitMovement UnitMovement => unitMovement;

    protected override void SetAttackCooldown() => attackCooldown = 1 / unitSO.attackSpeed;

    protected override bool CanAttack()
    {
        if (unitMovement.MovementState == UnitMovement.State.Moving) return false;

        return true;
    }

    protected override Entity FindTarget()
    {
        //CheckForEntities in current node
        return null;
    }

    protected override void Attack(Entity entity)
    {
        Transform projectileTransform = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        ProjectileHandler projectileHandler = projectileTransform.GetComponent<ProjectileHandler>();

        if(projectileHandler == null)
        {
            Debug.Log("Instantiated Projectile does not contain a ProjectileHandler component");
            return;
        }

        projectileHandler.SetTarget(entity.transform);
        projectileHandler.SetDamage(unitSO.damage);
        projectileHandler.SetSpeed(unitSO.projectileSpeed);
    }
}
