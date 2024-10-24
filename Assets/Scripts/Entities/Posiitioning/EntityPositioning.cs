using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.PackageManager;

public abstract class EntityPositioning : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Entity entity;

    public Entity Entity => entity;

    private Node position;
    private NodePosition nodePosition;

    public event EventHandler<OnPositionEventArgs> OnPositionSet;
    public static event EventHandler<OnAnyPositionEventArgs> OnAnyPositionSet;


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
}

