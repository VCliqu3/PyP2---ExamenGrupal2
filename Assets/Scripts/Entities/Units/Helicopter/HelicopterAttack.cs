using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterAttack : UnitAttack
{
    protected override bool CanAttackEntity(Entity entityToAttack)
    {
        if (entity.IsAlied == entityToAttack.IsAlied) return false;

        UnitMovement unitMovement = entity.GetComponent<UnitMovement>();

        if (unitMovement != null)
        {
            if (unitMovement.MovementState == UnitMovement.State.Moving) return false;
        }

        if (entity.EntitySO.entityType == EntityType.Structure) return true;
        if (entity.EntitySO.entityType == EntityType.Tank) return true;

        return false;
    }
}
