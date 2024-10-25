using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameSettingsSO gameSettingsSO;

    private void Start()
    {
        CreateInitialMaintenanceStructures();
    }

    private void CreateInitialMaintenanceStructures()
    {
        List<NodePosition> maintenanceStructurePositions = GetInitialMaintenanceStructuresNodePositions();

        foreach(NodePosition position in maintenanceStructurePositions)
        {
            EntitySpawnerUtility.Instance.TryInstantiateMaintenanceStructureInNodePosition(position);
        }
    }

    private List<NodePosition> GetInitialMaintenanceStructuresNodePositions()
    {
        List<NodePosition> positions = new List<NodePosition>();
        List<NodePosition> allPosition = new List<NodePosition>();

        int addedIndex = 0;

        if (gameSettingsSO.startingMaintenanceStructures <= 0) return positions;

        foreach(Node node in NodeManager.Instance.Nodes)
        {
            foreach(NodePosition nodePosition in node.GetAvailableStructurePositions())
            {
                allPosition.Add(nodePosition);
            }
        }

        foreach(NodePosition nodePosition in allPosition)
        {
            positions.Add(nodePosition);
            addedIndex++;

            if (addedIndex >= gameSettingsSO.startingMaintenanceStructures) break;
        }

        return positions;
    }
}
