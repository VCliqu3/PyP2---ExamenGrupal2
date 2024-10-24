using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int nodeNumber;

    [Header("Components")]
    [SerializeField] private List<NodePosition> structurePositions;
    [SerializeField] private List<NodePosition> groundAliedPositions;
    [SerializeField] private List<NodePosition> aerealAliedPositions;
    [Space]
    [SerializeField] private List<NodePosition> groundEnemyPositions;
    [SerializeField] private List<NodePosition> aerealEnemyPositions;

    public int NodeNumber => nodeNumber;
    public List<NodePosition> StructurePositions => structurePositions;
    public List<NodePosition> GroundAliedPositions => groundAliedPositions;
    public List<NodePosition> AerealAliedPositions => aerealAliedPositions;
    public List<NodePosition> GroundEnemyPositions => groundEnemyPositions;
    public List<NodePosition> AerealEnemyPositions => aerealEnemyPositions;

    public List<NodePosition> GetAvailableStructurePositions() => GetAvailablePositionsFromList(structurePositions);
    public List<NodePosition> GetAvailableGroundAliedPositions() => GetAvailablePositionsFromList(groundAliedPositions);
    public List<NodePosition> GetAvailableAerealAliedPositions() => GetAvailablePositionsFromList(aerealAliedPositions);
    public List<NodePosition> GetAvailableGroundEnemyPositions() => GetAvailablePositionsFromList(groundEnemyPositions);
    public List<NodePosition> GetAvailableAerealEnemyPositions() => GetAvailablePositionsFromList(aerealEnemyPositions);

    public bool HasAvailableStructurePositions() => GetAvailableStructurePositions().Count > 0;
    public bool HasAvailableGroundAliedPositions() => GetAvailableGroundAliedPositions().Count > 0;
    public bool HasAvailableAerealAliedPositions() => GetAvailableAerealAliedPositions().Count > 0;
    public bool HasAvailableGroundEnemyPositions() => GetAvailableGroundEnemyPositions().Count > 0;
    public bool HasAvailableAerealEnemyPositions() => GetAvailableAerealEnemyPositions().Count > 0;

    public bool GetRandomStructurePosition() => GetRandomPositionFromList(structurePositions);
    public bool GetRandomGroundAliedPosition() => GetRandomPositionFromList(groundAliedPositions);
    public bool GetRandomAerealAliedPosition() => GetRandomPositionFromList(aerealAliedPositions);
    public bool GetRandomGroundEnemyPosition() => GetRandomPositionFromList(groundEnemyPositions);
    public bool GetRandomAerealEnemyPosition() => GetRandomPositionFromList(aerealEnemyPositions);

    public bool GetFirstStructurePosition() => GetFirstPositionFromList(structurePositions);
    public bool GetFirstGroundAliedPosition() => GetFirstPositionFromList(groundAliedPositions);
    public bool GetFirstAerealAliedPosition() => GetFirstPositionFromList(aerealAliedPositions);
    public bool GetFirstGroundEnemyPosition() => GetFirstPositionFromList(groundEnemyPositions);
    public bool GetFirstAerealEnemyPosition() => GetFirstPositionFromList(aerealEnemyPositions);

    private List<NodePosition> GetAvailablePositionsFromList(List<NodePosition> potentialPositions)
    {
        List<NodePosition> availablePositions = new List<NodePosition>();

        foreach(NodePosition potentialPosition in potentialPositions)
        {
            if(potentialPosition.HasEntity()) continue;
            availablePositions.Add(potentialPosition);
        }

        return availablePositions;
    }

    public NodePosition GetRandomPositionFromList(List<NodePosition> positions)
    {
        if (positions.Count <= 0) return null;

        int randomIndex = UnityEngine.Random.Range(0, positions.Count);

        return positions[randomIndex];
    }

    public NodePosition GetFirstPositionFromList(List<NodePosition> positions)
    {
        if (positions.Count <= 0) return null;

        return positions[0];
    }
}
