using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintenanceStructureMaintenance : MonoBehaviour
{
    [Header("Maintenance Structure Specifics")]
    [SerializeField] private MaintenanceStructureSO maintenanceStructureSO;

    public MaintenanceStructureSO MaintenanceStructureSO => maintenanceStructureSO;
}
