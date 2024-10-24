using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitiesManager : MonoBehaviour
{
    public static EntitiesManager Instance { get; private set; }

    [Header("Lists")]
    [SerializeField] private List<Entity> allyEntities;
    [SerializeField] private List<Entity> enemyEntities;

    public List<Entity> AllyEntities => allyEntities;
    public List<Entity> EnemyEntities => enemyEntities;

    public static event EventHandler<OnEntityEventArgs> OnAnyAllyEntityAdded;
    public static event EventHandler<OnEntityEventArgs> OnAnyEnemyEntityAdded;
    public static event EventHandler<OnEntityEventArgs> OnAnyAllyEntityRemoved;
    public static event EventHandler<OnEntityEventArgs> OnAnyEnemyEntityRemoved;

    public class OnEntityEventArgs : EventArgs
    {
        public Entity entity;
    }

    private void OnEnable()
    {
        Entity.OnEntityInitialized += Entity_OnEntityInitialized;
        EntityHealth.OnAnyEntityDeath += EntityHealth_OnAnyEntityDeath;
    }

    private void OnDisable()
    {
        Entity.OnEntityInitialized -= Entity_OnEntityInitialized;
        EntityHealth.OnAnyEntityDeath -= EntityHealth_OnAnyEntityDeath;
    }

    private void Awake()
    {
        SetSingleton();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("There is more than one EntitiesManager Instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private int GetAllyEntitiesCount() => allyEntities.Count;
    private int GetEnemyEntitiesCount() => enemyEntities.Count;

    public bool HasAllyEntities() => allyEntities.Count > 0;
    public bool HasEnemyEntities() => enemyEntities.Count > 0;

    private void CheckAddToAllyEntitiesList(Entity entity)
    {
        if (allyEntities.Contains(entity)) return;
        if (!entity.IsAlied) return;

        allyEntities.Add(entity);

        OnAnyAllyEntityAdded?.Invoke(this, new OnEntityEventArgs { entity = entity });
    }

    private void CheckAddToEnemyEntitiesList(Entity entity)
    {
        if (enemyEntities.Contains(entity)) return;
        if (entity.IsAlied) return;

        enemyEntities.Add(entity);

        OnAnyEnemyEntityAdded?.Invoke(this, new OnEntityEventArgs { entity = entity });
    }

    private void CheckRemoveFromAllyEntitiesList(Entity entity)
    {
        if (!allyEntities.Contains(entity)) return;
        if (!entity.IsAlied) return;

        allyEntities.Remove(entity);

        OnAnyAllyEntityRemoved?.Invoke(this, new OnEntityEventArgs { entity = entity });
    }

    private void CheckRemoveFromEnemyEntitiesList(Entity entity)
    {
        if (!enemyEntities.Contains(entity)) return;
        if (entity.IsAlied) return;

        enemyEntities.Remove(entity);

        OnAnyEnemyEntityRemoved?.Invoke(this, new OnEntityEventArgs { entity = entity });
    }

    #region Subscriptions
    private void Entity_OnEntityInitialized(object sender, Entity.OnEntityInitializedEventArgs e)
    {
        CheckAddToAllyEntitiesList(e.entity);
        CheckAddToEnemyEntitiesList(e.entity);
    }
    private void EntityHealth_OnAnyEntityDeath(object sender, EntityHealth.OnAnyEntityDeathEventArgs e)
    {
        CheckRemoveFromAllyEntitiesList(e.entityHealth.Entity);
        CheckRemoveFromEnemyEntitiesList(e.entityHealth.Entity);
    }

    #endregion
}

