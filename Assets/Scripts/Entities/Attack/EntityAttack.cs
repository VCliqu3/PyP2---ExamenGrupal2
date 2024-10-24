using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityAttack : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] protected Entity entity;
    [SerializeField] protected Entity currentTarget;
    [Space]
    [SerializeField] protected Transform projectilePrefab;
    [Space] 
    [SerializeField] protected Transform firePoint;

    [Header("States")]
    [SerializeField] protected State state;

    public State AttackState => state;
    public enum State { NotAttacking, Attacking }

    public Entity Entity => entity;
    public Entity CurrentTarget => currentTarget;

    protected float timer;
    protected float attackCooldown;

    protected void Start()
    {
        MaxTimer();
    }

    protected void Update()
    {
        HandleAttack();
    }

    private void SetAttackState(State state) => this.state = state;

    protected void HandleAttack()
    {
        switch (state)
        {
            case State.NotAttacking:
            default:
                NotAttackingLogic();
                break;
            case State.Attacking:
                AttackingLogic();
                break;
        }
    }

    protected void NotAttackingLogic()
    {
        Entity target = FindTarget();

        if (target == null) return;

        SetCurrentTarget(target);

        SetAttackState(State.Attacking);
        ResetTimer();
    }

    protected void AttackingLogic()
    {
        if (AttackOnCooldown())
        {
            timer += Time.deltaTime;
            return;
        }

        Attack(currentTarget);
        ResetTimer();
    }

    protected abstract bool CanAttackEntity(Entity entity);

    protected abstract Entity FindTarget();

    protected abstract void Attack(Entity entity);


    protected void SetCurrentTarget(Entity target) => currentTarget = target;  
    protected void ClearCurrentTarget() => currentTarget = null;

    protected abstract void SetAttackCooldown();
    protected void ResetTimer() => timer = 0;
    protected void MaxTimer() => timer = attackCooldown;
    protected bool AttackOnCooldown() => timer < attackCooldown;
}
