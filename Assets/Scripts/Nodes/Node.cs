using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int nodeNumber;

    [Header("Components")]
    [SerializeField] private List<NodePosition> structurePositions;
    [SerializeField] private List<NodePosition> groundPositions;
    [SerializeField] private List<NodePosition> aerealPositions;

    public int NodeNumber => nodeNumber;
}
