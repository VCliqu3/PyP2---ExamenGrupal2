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

    public NodePosition GetRandomAvailableStructurePosition() => GetRandomPositionFromList(GetAvailablePositionsFromList(structurePositions));
    public NodePosition GetRandomAvailableGroundAliedPosition() => GetRandomPositionFromList(GetAvailablePositionsFromList(groundAliedPositions));
    public NodePosition GetRandomAvailableAerealAliedPosition() => GetRandomPositionFromList(GetAvailablePositionsFromList(aerealAliedPositions));
    public NodePosition GetRandomAvailableGroundEnemyPosition() => GetRandomPositionFromList(GetAvailablePositionsFromList(groundEnemyPositions));
    public NodePosition GetRandomAvailableAerealEnemyPosition() => GetRandomPositionFromList(GetAvailablePositionsFromList(aerealEnemyPositions));

    public NodePosition GetFirstAvailableStructurePosition() => GetFirstPositionFromList(GetAvailablePositionsFromList(structurePositions));
    public NodePosition GetFirstAvailableGroundAliedPosition() => GetFirstPositionFromList(GetAvailablePositionsFromList(groundAliedPositions));
    public NodePosition GetFirstAvailableAerealAliedPosition() => GetFirstPositionFromList(GetAvailablePositionsFromList(aerealAliedPositions));
    public NodePosition GetFirstAvailableGroundEnemyPosition() => GetFirstPositionFromList(GetAvailablePositionsFromList(groundEnemyPositions));
    public NodePosition GetFirstAvailableAerealEnemyPosition() => GetFirstPositionFromList(GetAvailablePositionsFromList(aerealEnemyPositions));

    public bool HasStructures() => ListHasUnits(structurePositions);
    public bool HasAliedGroundUnits() => ListHasUnits(groundAliedPositions);
    public bool HasAliedAerealUnits() => ListHasUnits(aerealAliedPositions);
    public bool HasEnemyGroundUnits() => ListHasUnits(groundEnemyPositions);
    public bool HasEnemyAerealUnits() => ListHasUnits(aerealEnemyPositions);

    public List<Entity> GetAllEntitiesInNode()
    {
        List<Entity> entities = new List<Entity>();

        entities.AddRange(GetAllStructures());
        entities.AddRange(GetAllGroundAlliedUnits());
        entities.AddRange(GetAllAerealAliedUnits());
        entities.AddRange(GetAllGroundEnemyUnits());
        entities.AddRange(GetAllAerealEnemyUnits());

        return entities;
    }

    public List<Entity> GetAllStructures() => GetAllEntitiesInList(structurePositions);
    public List<Entity> GetAllGroundAlliedUnits() => GetAllEntitiesInList(groundAliedPositions);
    public List<Entity> GetAllAerealAliedUnits() => GetAllEntitiesInList(aerealAliedPositions);
    public List<Entity> GetAllGroundEnemyUnits() => GetAllEntitiesInList(groundEnemyPositions);
    public List<Entity> GetAllAerealEnemyUnits() => GetAllEntitiesInList(aerealEnemyPositions);

    public List<Entity> GetAllEntitiesInList(List<NodePosition> nodePositions)
    {
        List<Entity> entities = new List<Entity>();

        foreach (NodePosition nodePosition in nodePositions)
        {
            if(nodePosition.HasEntity()) entities.Add(nodePosition.Entity);
        }

        return entities;
    }

    public bool HasAliedUnits()
    {
        if (HasStructures()) return true;
        if(HasAliedGroundUnits()) return true;
        if(HasAliedAerealUnits()) return true;

        return false;
    }

    public bool HasEnemyUnits()
    {
        if (HasEnemyGroundUnits()) return true;
        if (HasEnemyAerealUnits()) return true;

        return false;
    }

    public bool ListHasUnits(List<NodePosition> nodePositions)
    {
        foreach(NodePosition nodePosition in nodePositions)
        {
            if (nodePosition.HasEntity()) return true;
        }

        return false;
    }


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
