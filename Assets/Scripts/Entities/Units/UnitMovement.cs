using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Entity entity;
    [SerializeField] private EntityPositioning entityPositioning;
    [SerializeField] private EntityAttack entityAttack;

    [Header("Unit Specifics")]
    [SerializeField] protected UnitSO unitSO;

    [Header("States")]
    [SerializeField] private State state;

    public UnitSO UnitSO => unitSO;
    public Entity Entity => entity;
    private NodePosition NodePosition => entityPositioning.GetNodePosition();

    public State MovementState => state;
    public enum State { NotMoving, Moving }

    public static event EventHandler<OnUnitEndMovementEventArgs> OnUnitEndMovement; 

    private const float NOT_MOVING_DISTANCE = 0.025f;

    private float timer;
    private float movementCooldown;

    public class OnUnitEndMovementEventArgs : EventArgs
    {
        public UnitMovement unitMovement;
        public NodePosition nodePosition;
    }

    private void Start()
    {
        SetMovementCooldown();
        SetMovementState(State.NotMoving);
        ResetTimer();
    }

    private void Update()
    {
        HandleMovement();
    }

    private void SetMovementState(State state) => this.state = state;

    private void HandleMovement()
    {
        switch (state)
        {
            case State.NotMoving:
            default:
                NotMovingLogic();
                break;
            case State.Moving:
                MovingLogic();
                break;
        }
    }

    private void NotMovingLogic()
    {
        if (NodePosition == null)
        {
            ResetTimer();
            return;
        }

        if (!CanMove())
        {
            ResetTimer();
            return;
        }

        if (MovementOnCooldown())
        {
            timer += Time.deltaTime;
            return;
        }

        TryMove();
    }

    private void TryMove()
    {
        ResetTimer();

        NodePosition nextNodePosition = entityPositioning.GetNextNodePosition();

        if (nextNodePosition == null) return; //No Available Position

        entityPositioning.SetPosition(nextNodePosition.Node, nextNodePosition);

        SetMovementState(State.Moving);
    }

    private void MovingLogic()
    {
        if (NodePosition == null) return;

        transform.position = Vector3.Lerp(transform.position, NodePosition.transform.position, unitSO.smoothMoveFactor* Time.deltaTime);

        float distanceToNodePosition = Vector3.Distance(transform.position, NodePosition.transform.position);

        if (distanceToNodePosition <= NOT_MOVING_DISTANCE)
        {
            SetMovementState(State.NotMoving);
            ResetTimer();

            OnUnitEndMovement?.Invoke(this, new OnUnitEndMovementEventArgs { unitMovement = this, nodePosition = NodePosition });
        }
    }

    private bool CanMove()
    {
        if (entityAttack.AttackState == EntityAttack.State.Attacking) return false;
        if (entity.IsAlied && entityPositioning.CheckCurrentNodeHasEnemyUnits()) return false;
        if (!entity.IsAlied && entityPositioning.CheckCurrentNodeHasAliedUnits()) return false;

        return true;
    }

    private void SetMovementCooldown() => movementCooldown = 1/unitSO.speed;
    private void ResetTimer() => timer = 0f;
    private void MaxTimer() => timer = movementCooldown;
    private bool MovementOnCooldown() => timer < movementCooldown;


}
