using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.PackageManager;

public abstract class EntityPositioning : MonoBehaviour
{
    private Node position;

    public event EventHandler<OnPositionEventArgs> OnPositionSet;
    public static event EventHandler<OnAnyPositionEventArgs> OnAnyPositionSet;

    public class OnPositionEventArgs : EventArgs
    {
        public Node previousPosition;
        public Node newPosition;
    }

    public class OnAnyPositionEventArgs : EventArgs
    {
        public Node previousPosition;
        public Node newPosition;
        public EntityPositioning entity;

    }

    public Node GetPosition() => position;
    public void SetPosition(Node position)
    {
        if(this.position == position) return;

        Node previousPosition = this.position;
        this.position = position;

        OnPositionSet?.Invoke(this, new OnPositionEventArgs { previousPosition = previousPosition, newPosition = position });
        OnAnyPositionSet?.Invoke(this, new OnAnyPositionEventArgs { previousPosition = previousPosition, newPosition = position, entity = this });
    }
}

