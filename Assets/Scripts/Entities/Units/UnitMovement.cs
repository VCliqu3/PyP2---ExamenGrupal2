using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private EntityPositioning entityPositioning;
    [SerializeField] private EntityAttack entityAttack;

    [Header("Unit Specifics")]
    [SerializeField] protected UnitSO unitSO;

    [Header("States")]
    [SerializeField] private State state;

    public UnitSO UnitSO => unitSO;
    private NodePosition NodePosition => entityPositioning.GetNodePosition();

    public State MovementState => state;
    public enum State { NotMoving, Moving }

    private const float SMOOTH_POSITION_FACTOR = 10f;
    private const float NOT_MOVING_DISTANCE = 0.1f;

    private void Start()
    {
        SetPositioningState(State.NotMoving);
    }

    private void Update()
    {
        HandleMovement();
    }

    private void SetPositioningState(State state) => this.state = state;

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
        if (NodePosition == null) return;

        float distanceToNodePosition = Vector3.Distance(transform.position, NodePosition.transform.position);

        if (distanceToNodePosition > NOT_MOVING_DISTANCE)
        {
            SetPositioningState(State.Moving);
        }
    }

    private void MovingLogic()
    {
        if (NodePosition == null) return;

        transform.position = Vector3.Lerp(transform.position, NodePosition.transform.position, SMOOTH_POSITION_FACTOR * Time.deltaTime);

        float distanceToNodePosition = Vector3.Distance(transform.position, NodePosition.transform.position);

        if (distanceToNodePosition <= NOT_MOVING_DISTANCE)
        {
            SetPositioningState(State.NotMoving);
        }
    }

    private bool CanMove()
    {
        if (entityAttack.AttackState == EntityAttack.State.Attacking) return false;
        return true;
    }

    private bool CheckCurrentNodeHasAliedUnits()
    {
        return false;
    }

    private bool CheckCurrentNodeHasEnemyUnits()
    {
        return false;
    }
}
