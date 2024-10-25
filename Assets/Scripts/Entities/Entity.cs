using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private EntitySO entitySO;
    [SerializeField] private EntityHealth entityHealth;
    [SerializeField] private EntityPositioning entityPositioning;
    [Space]
    [SerializeField] protected bool isAlied;

    public EntitySO EntitySO => entitySO;
    public EntityHealth EntityHealth => entityHealth;
    public EntityPositioning EntityPositioning => entityPositioning;
    public bool IsAlied => isAlied;

    public static event EventHandler<OnEntityInitializedEventArgs> OnEntityInitialized;

    public class OnEntityInitializedEventArgs : EventArgs
    {
        public Entity entity;
    }

    private void Start()
    {
        InitializeEntity();
    }

    private void InitializeEntity()
    {
        OnEntityInitialized?.Invoke(this, new OnEntityInitializedEventArgs { entity = this });
    }
}
