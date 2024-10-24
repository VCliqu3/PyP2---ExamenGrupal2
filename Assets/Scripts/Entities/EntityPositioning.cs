using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.PackageManager;

public abstract class EntityPositioning : MonoBehaviour
{
    private Node position;
    private NodePosition nodePosition;

    public event EventHandler<OnPositionEventArgs> OnPositionSet;
    public static event EventHandler<OnAnyPositionEventArgs> OnAnyPositionSet;

    private const float smoothPositionTarget = 10f;

    public class OnPositionEventArgs : EventArgs
    {
        public Node previousPosition;
        public Node newPosition;
        public NodePosition previousNodePosition;
        public NodePosition newNodePosition;
    }

    public class OnAnyPositionEventArgs : EventArgs
    {
        public Node previousPosition;
        public Node newPosition;
        public NodePosition previousNodePosition;
        public NodePosition newNodePosition;
        public EntityPositioning entity;
    }

    private void Update()
    {
        HandlePositionMovement();
    }

    private void HandlePositionMovement()
    {
        if (nodePosition == null) return;
        transform.position = Vector3.Lerp(transform.position, nodePosition.transform.position, smoothPositionTarget * Time.deltaTime);
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
        OnAnyPositionSet?.Invoke(this, new OnAnyPositionEventArgs { previousPosition = previousPosition, newPosition = position, previousNodePosition = previousNodePosition, newNodePosition = nodePosition, entity = this });
    }
}

