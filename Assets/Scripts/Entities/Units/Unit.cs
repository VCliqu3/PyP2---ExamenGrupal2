using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : Entity
{
    [Header("Unit Specifics")]
    [SerializeField] protected UnitSO unitSO;
    [SerializeField] protected Entity currentTarget;

    public UnitSO UnitSO => unitSO;
    public Entity CurrentTarget => currentTarget;

    private void Update()
    {
        HandleMovement();
        HandleAttack();
    }

    private void HandleAttack()
    {

    }

    private void HandleMovement()
    {

    }


    protected abstract bool CanAttackEntity(Entity entity, EntitySO entitySO);
}
