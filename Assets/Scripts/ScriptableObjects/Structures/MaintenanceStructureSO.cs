using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMaintenanceStructureSO", menuName = "ScriptableObjects/Entities/Structures/MaintenanceStructure")]
public class MaintenanceStructureSO : StructureSO
{
    [SerializeField, Range(0f, 10f)] public int unitsPerStructure;
}
