using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.PackageManager;
using Unity.VisualScripting;

public class EntityPositioning : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Entity entity;

    public NodePosition testPosition;

    public Entity Entity => entity;

    private Node position;
    private NodePosition nodePosition;

    public event EventHandler<OnPositionEventArgs> OnPositionSet;
    public static event EventHandler<OnAnyPositionEventArgs> OnAnyPositionSet;

    private void Start()
    {
        SetPosition(testPosition.Node, testPosition);
    }

    public class OnPositionEventArgs : EventArgs
    {
        public Node previousPosition;
        public Node newPosition;
        public NodePosition previousNodePosition;
        public NodePosition newNodePosition;
    }

    public class OnAnyPositionEventArgs : EventArgs
    {
        public EntityPositioning entityPositioning;
        public Node previousPosition;
        public Node newPosition;
        public NodePosition previousNodePosition;
        public NodePosition newNodePosition;
    }

    public Node GetPosition() => position;
    public NodePosition GetNodePosition() => nodePosition;

    public void SetPosition(Node position, NodePosition nodePosition)
    {
        if (this.nodePosition == nodePosition) return;
        if (this.position == position) return;

        Node previousPosition = this.position;
        NodePosition previousNodePosition = this.nodePosition;

        this.position = position;
        this.nodePosition = nodePosition;

        OnPositionSet?.Invoke(this, new OnPositionEventArgs { previousPosition = previousPosition, newPosition = position , previousNodePosition = previousNodePosition, newNodePosition = nodePosition});
        OnAnyPositionSet?.Invoke(this, new OnAnyPositionEventArgs { entityPositioning = this , previousPosition = previousPosition, newPosition = position, previousNodePosition = previousNodePosition, newNodePosition = nodePosition});
    }

    public Node GetNextNode()
    {
        if (entity.IsAlied) 
        {
            return NodeManager.Instance.GetNextNodeAliedDirection(position);
        }
        
        return NodeManager.Instance.GetNextNodeEnemyDirection(position);
    }

    public NodePosition GetNextNodePosition()
    {
        Node nextNode = GetNextNode();

        if (nextNode == null) return null;

        NodePosition nextNodePosition;

        if (entity.IsAlied)
        {
            switch (entity.EntitySO.entityType)
            {
                case EntityType.Structure:
                    nextNodePosition = nextNode.GetRandomAvailableStructurePosition();
                    break;
                case EntityType.Soldier:
                case EntityType.Tank:
                default:
                    nextNodePosition = nextNode.GetRandomAvailableGroundAliedPosition();
                    break;
                case EntityType.Helicopter:
                    nextNodePosition = nextNode.GetRandomAvailableAerealAliedPosition();
                    break;
            }
        }
        else
        {
            switch (entity.EntitySO.entityType)
            {
                case EntityType.Soldier:
                case EntityType.Tank:
                default:
                    nextNodePosition = nextNode.GetRandomAvailableGroundEnemyPosition();
                    break;
                case EntityType.Helicopter:
                    nextNodePosition = nextNode.GetRandomAvailableAerealEnemyPosition();
                    break;
            }
        }

        return nextNodePosition;
    }

    public bool CheckCurrentNodeHasAliedUnits() => position.HasAliedUnits();

    public bool CheckCurrentNodeHasEnemyUnits() => position.HasEnemyUnits();
}

