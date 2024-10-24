using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePosition : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Node node;
    [SerializeField] private Transform entityTransform;

    public Node Node => node;
    public Transform EntityTransform => entityTransform;

    private void OnEnable()
    {
        EntityPositioning.OnAnyPositionSet += EntityPositioning_OnAnyPositionSet;
    }

    private void OnDisable()
    {
        EntityPositioning.OnAnyPositionSet -= EntityPositioning_OnAnyPositionSet;
    }

    public bool HasEntity() => entityTransform != null;
    public void SetEntityTransform(Transform entityTransform)
    {
        this.entityTransform = entityTransform;
        entityTransform.SetParent(transform);
    }

    public void ClearEntityTransform()
    {
        if (entityTransform == null) return;

        entityTransform.SetParent(null);
        entityTransform = null;
    }

    private void CheckLeavePosition(EntityPositioning entityPositioning, NodePosition nodePosition)
    {
        if (nodePosition != this) return;
        if (entityPositioning.transform != entityTransform) return;

        ClearEntityTransform();
    }

    private void CheckArrivePosition(EntityPositioning entityPositioning, NodePosition nodePosition)
    {
        if (nodePosition != this) return;
        if (HasEntity()) return;

        SetEntityTransform(entityPositioning.transform);
    }

    private void EntityPositioning_OnAnyPositionSet(object sender, EntityPositioning.OnAnyPositionEventArgs e)
    {
        CheckLeavePosition(e.entityPositioning, e.previousNodePosition);
        CheckArrivePosition(e.entityPositioning, e.newNodePosition);
    }
}
