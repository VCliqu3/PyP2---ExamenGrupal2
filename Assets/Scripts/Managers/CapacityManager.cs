using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapacityManager : MonoBehaviour
{
    public static CapacityManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private int capacity;

    private int Capacity => capacity;

    public static event EventHandler<OnCapacityEventArgs> OnCapacityInitialized;
    public static event EventHandler<OnCapacityEventArgs> OnCapacityIncreased;
    public static event EventHandler<OnCapacityEventArgs> OnCapacityDecreased;

    public class OnCapacityEventArgs : EventArgs
    {
        public int capacity;
    }

    private void OnEnable()
    {
        MaintenanceStructureMaintenance.OnMaintenanceStructureInitialized += MaintenanceStructureMaintenance_OnMaintenanceStructureInitialized;
        MaintenanceStructureMaintenance.OnMaintenanceStructureDestroyed += MaintenanceStructureMaintenance_OnMaintenanceStructureDestroyed;
    }

    private void OnDisable()
    {
        MaintenanceStructureMaintenance.OnMaintenanceStructureInitialized -= MaintenanceStructureMaintenance_OnMaintenanceStructureInitialized;
        MaintenanceStructureMaintenance.OnMaintenanceStructureDestroyed -= MaintenanceStructureMaintenance_OnMaintenanceStructureDestroyed;
    }

    private void Awake()
    {
        SetSingleton();
        ResetCapacity();
    }

    private void Start()
    {
        InitializeCapacity();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("There is more than one CapacityManager Instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private void InitializeCapacity()
    {
        OnCapacityInitialized?.Invoke(this, new OnCapacityEventArgs { capacity = capacity });
    }

    private void IncreaseCapacity(int quantity)
    {
        capacity += quantity;
        OnCapacityIncreased?.Invoke(this, new OnCapacityEventArgs { capacity = capacity });
    }

    private void DecreaseCapacity(int quantity)
    {
        capacity = capacity - quantity < 0 ? 0 : capacity - quantity;
        OnCapacityDecreased?.Invoke(this, new OnCapacityEventArgs { capacity = capacity });
    }

    private void ResetCapacity() => capacity = 0;

    #region MaintenanceStructure Subscriptions
    private void MaintenanceStructureMaintenance_OnMaintenanceStructureInitialized(object sender, MaintenanceStructureMaintenance.OnMaintenanceStructureEventArgs e)
    {
        IncreaseCapacity(e.capacity);
    }
    private void MaintenanceStructureMaintenance_OnMaintenanceStructureDestroyed(object sender, MaintenanceStructureMaintenance.OnMaintenanceStructureEventArgs e)
    {
        DecreaseCapacity(e.capacity);
    }
    #endregion
}
