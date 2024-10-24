using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Unit
{
    protected override bool CanAttackEntity(Entity entity, EntitySO entitySO)
    {
        if (isAlied == entity.IsAlied) return false;

        if (entitySO.entityType == EntityType.Structure) return true;
        if (entitySO.entityType == EntityType.Soldier) return true;

        return false;
    }
}
