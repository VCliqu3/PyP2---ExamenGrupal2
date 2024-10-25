using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePosition : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Node node;
    [SerializeField] private Entity entity;

    public Node Node => node;
    public Entity Entity => entity;

    private void OnEnable()
    {
        EntityPositioning.OnAnyPositionSet += EntityPositioning_OnAnyPositionSet;
        EntityHealth.OnAnyEntityDeath += EntityHealth_OnAnyEntityDeath;
    }

    private void OnDisable()
    {
        EntityPositioning.OnAnyPositionSet -= EntityPositioning_OnAnyPositionSet;
        EntityHealth.OnAnyEntityDeath -= EntityHealth_OnAnyEntityDeath;
    }

    public bool HasEntity() => entity != null;
    public void SetEntity(Entity entity)
    {
        this.entity = entity;
        entity.transform.SetParent(transform);
    }

    public void ClearEntity()
    {
        if (entity == null) return;

        if(entity.transform.parent == this) entity.transform.SetParent(null);

        entity = null;
    }

    private void CheckLeavePosition(EntityPositioning entityPositioning, NodePosition nodePosition)
    {
        if (nodePosition != this) return;
        if (entityPositioning.Entity != entity) return;

        ClearEntity();
    }

    private void CheckArrivePosition(EntityPositioning entityPositioning, NodePosition nodePosition)
    {
        if (nodePosition != this) return;
        if (HasEntity()) return;

        SetEntity(entityPositioning.Entity);
    }

    protected void CheckEntityDeath(EntityHealth entityHealth)
    {
        if (entityHealth.Entity != entity) return;

        ClearEntity();
    }

    private void EntityPositioning_OnAnyPositionSet(object sender, EntityPositioning.OnAnyPositionEventArgs e)
    {
        CheckLeavePosition(e.entityPositioning, e.previousNodePosition);
        CheckArrivePosition(e.entityPositioning, e.newNodePosition);
    }

    private void EntityHealth_OnAnyEntityDeath(object sender, EntityHealth.OnAnyEntityDeathEventArgs e)
    {
        CheckEntityDeath(e.entityHealth);
    }
}
