using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawnerUtility : MonoBehaviour
{
    public static EntitySpawnerUtility Instance {  get; private set; }

    [Header("Allied Units")]
    [SerializeField] private Transform aliedSoldierPrefab;
    [SerializeField] private Transform aliedTankPrefab;
    [SerializeField] private Transform aliedHelicopterPrefab;

    [Header("Allied Structures")]
    [SerializeField] private Transform collectorStructurePrefab;
    [SerializeField] private Transform maintenanceStructurePrefab;
    [SerializeField] private Transform defenseStructurePrefab;

    [Header("Enemy Units")]
    [SerializeField] private Transform enemySoldierPrefab;
    [SerializeField] private Transform enemyTankPrefab;
    [SerializeField] private Transform enemyHelicopterPrefab;

    public static event EventHandler<OnEntitySpawnedEventArgs> OnEntitySpawned;

    public class OnEntitySpawnedEventArgs : EventArgs
    {
        public Entity entity;
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
            Debug.LogWarning("There is more than one EntitySpawnerUtility Instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    #region Alied Units
    public bool TryInstantiateAlliedSoldierInNode(Node node)
    {
        NodePosition spawnPosition = node.GetRandomAvailableGroundAliedPosition();
        bool spawned = TryInstantiateEntityInNodePosition(aliedSoldierPrefab, spawnPosition);

        return spawned;
    }

    public bool TryInstantiateAlliedTankInNode(Node node)
    {
        NodePosition spawnPosition = node.GetRandomAvailableGroundAliedPosition();
        bool spawned = TryInstantiateEntityInNodePosition(aliedTankPrefab, spawnPosition);

        return spawned;
    }

    public bool TryInstantiateAlliedHelicopterInNode(Node node)
    {
        NodePosition spawnPosition = node.GetRandomAvailableAerealAliedPosition();
        bool spawned = TryInstantiateEntityInNodePosition(aliedHelicopterPrefab, spawnPosition);

        return spawned;
    }
    #endregion

    #region Alied Structures
    public bool TryInstantiateCollectorStructureInNode(Node node)
    {
        NodePosition spawnPosition = node.GetRandomAvailableStructurePosition();
        bool spawned = TryInstantiateEntityInNodePosition(collectorStructurePrefab, spawnPosition);

        return spawned;
    }

    public bool TryInstantiateMaintenanceStructureInNode(Node node)
    {
        NodePosition spawnPosition = node.GetRandomAvailableStructurePosition();
        bool spawned = TryInstantiateEntityInNodePosition(maintenanceStructurePrefab, spawnPosition);

        return spawned;
    }

    public bool TryInstantiateDefenseStructureInNode(Node node)
    {
        NodePosition spawnPosition = node.GetRandomAvailableStructurePosition();
        bool spawned = TryInstantiateEntityInNodePosition(defenseStructurePrefab, spawnPosition);

        return spawned;
    }
    #endregion

    #region Enemy Units
    public bool TryInstantiateEnemySoldierInNode(Node node)
    {
        NodePosition spawnPosition = node.GetRandomAvailableGroundEnemyPosition();
        bool spawned = TryInstantiateEntityInNodePosition(enemySoldierPrefab, spawnPosition);

        return spawned;
    }

    public bool TryInstantiateEnemyTankInNode(Node node)
    {
        NodePosition spawnPosition = node.GetRandomAvailableGroundEnemyPosition();
        bool spawned = TryInstantiateEntityInNodePosition(enemyTankPrefab, spawnPosition);

        return spawned;
    }

    public bool TryInstantiateEnemyHelicopterInNode(Node node)
    {
        NodePosition spawnPosition = node.GetRandomAvailableAerealEnemyPosition();
        bool spawned = TryInstantiateEntityInNodePosition(enemyHelicopterPrefab, spawnPosition);

        return spawned;
    }
    #endregion

    public bool TryInstantiateAliedSoldierInNodePosition(NodePosition nodePosition) => TryInstantiateEntityInNodePosition(aliedSoldierPrefab, nodePosition);
    public bool TryInstantiateAliedTankInNodePosition(NodePosition nodePosition) => TryInstantiateEntityInNodePosition(aliedTankPrefab, nodePosition);
    public bool TryInstantiateAliedHelicopterInNodePosition(NodePosition nodePosition) => TryInstantiateEntityInNodePosition(aliedHelicopterPrefab, nodePosition);
    public bool TryInstantiateCollectorStructureInNodePosition(NodePosition nodePosition) => TryInstantiateEntityInNodePosition(collectorStructurePrefab, nodePosition);
    public bool TryInstantiateMaintenanceStructureInNodePosition(NodePosition nodePosition) => TryInstantiateEntityInNodePosition(maintenanceStructurePrefab, nodePosition);
    public bool TryInstantiateDefenseStructureInNodePosition(NodePosition nodePosition) => TryInstantiateEntityInNodePosition(defenseStructurePrefab, nodePosition);
    public bool TryInstantiateEnemySoldierInNodePosition(NodePosition nodePosition) => TryInstantiateEntityInNodePosition(enemySoldierPrefab, nodePosition);
    public bool TryInstantiateEnemyTankInNodePosition(NodePosition nodePosition) => TryInstantiateEntityInNodePosition(enemyTankPrefab, nodePosition);
    public bool TryInstantiateEnemyHelicopterInNodePosition(NodePosition nodePosition) => TryInstantiateEntityInNodePosition(enemyHelicopterPrefab, nodePosition);


    public bool TryInstantiateEntityInNodePosition(Transform entityPrefab, NodePosition nodePosition)
    {
        if(nodePosition == null) return false;
        if (nodePosition.HasEntity()) return false;

        Transform entityTransform = Instantiate(entityPrefab, nodePosition.transform.position, nodePosition.transform.rotation);

        EntityPositioning entityPositioning = entityTransform.GetComponent<EntityPositioning>();

        if (entityPositioning == null)
        {
            Debug.Log("Instantiated Entity does not contain an EntityPositioning component");
            return true;
        }

        OnEntitySpawned?.Invoke(this, new OnEntitySpawnedEventArgs { entity = entityPositioning.Entity });

        entityPositioning.SetPosition(nodePosition.Node, nodePosition);

        return true;
    }
}
