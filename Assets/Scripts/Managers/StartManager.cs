using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameSettingsSO gameSettingsSO;
    [SerializeField] private Transform maintenanceStructurePrefab;

    private void Start()
    {
        CreateInitialMaintenanceStructures();
    }

    private void CreateInitialMaintenanceStructures()
    {
        List<NodePosition> maintenanceStructurePositions = GetInitialMaintenanceStructuresNodePositions();

        foreach(NodePosition position in maintenanceStructurePositions)
        {
            InstantiateMaintenanceStructure(position);
        }
    }

    private void InstantiateMaintenanceStructure(NodePosition nodePosition)
    {
        Transform maintenanceStructureTransform = Instantiate(maintenanceStructurePrefab,nodePosition.transform.position, nodePosition.transform.rotation);

        EntityPositioning entityPositioning = maintenanceStructureTransform.GetComponent<EntityPositioning>();

        if(entityPositioning == null)
        {
            Debug.Log("Instantiated Maintenance Structure does not contain an EntityPositioning component");
            return;
        }

        entityPositioning.SetPosition(nodePosition.Node, nodePosition);
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
