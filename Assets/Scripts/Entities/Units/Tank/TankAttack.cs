using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAttack : UnitAttack
{
    protected override bool CanAttackEntity(Entity entityToAttack)
    {
        if (entity.IsAlied == entityToAttack.IsAlied) return false;

        UnitMovement unitMovement = entityToAttack.GetComponent<UnitMovement>();

        if (unitMovement != null)
        {
            if (unitMovement.MovementState == UnitMovement.State.Moving) return false;
        }

        if (entityToAttack.EntitySO.entityType == EntityType.Structure) return true;
        if (entityToAttack.EntitySO.entityType == EntityType.Soldier) return true;

        return false;
    }
}
