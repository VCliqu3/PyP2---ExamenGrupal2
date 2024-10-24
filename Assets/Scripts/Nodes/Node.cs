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
}
