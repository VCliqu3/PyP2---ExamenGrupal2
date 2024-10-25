using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintenanceStructureMaintenance : MonoBehaviour
{
    [Header("Maintenance Structure Specifics")]
    [SerializeField] private MaintenanceStructureSO maintenanceStructureSO;
    [SerializeField] private EntityHealth entityHealth;

    public MaintenanceStructureSO MaintenanceStructureSO => maintenanceStructureSO;

    public static event EventHandler<OnMaintenanceStructureEventArgs> OnMaintenanceStructureInitialized;
    public static event EventHandler<OnMaintenanceStructureEventArgs> OnMaintenanceStructureDestroyed;

    public class OnMaintenanceStructureEventArgs : EventArgs
    {
        public MaintenanceStructureMaintenance maintenanceStructure;
        public int capacity;
    }

    private void OnEnable()
    {
        entityHealth.OnEntityDeath += EntityHealth_OnEntityDeath;
    }

    private void OnDisable()
    {
        entityHealth.OnEntityDeath -= EntityHealth_OnEntityDeath;
    }

    public void Start()
    {
        InitializeMaintenanceStructure();
    }

    private void InitializeMaintenanceStructure()
    {
        OnMaintenanceStructureInitialized?.Invoke(this, new OnMaintenanceStructureEventArgs { maintenanceStructure = this, capacity = maintenanceStructureSO.capacity });
    }

    #region EntityHealth Subscriptions
    private void EntityHealth_OnEntityDeath(object sender, EventArgs e)
    {
        OnMaintenanceStructureDestroyed?.Invoke(this, new OnMaintenanceStructureEventArgs { maintenanceStructure = this, capacity = maintenanceStructureSO.capacity });
    }
    #endregion
}
