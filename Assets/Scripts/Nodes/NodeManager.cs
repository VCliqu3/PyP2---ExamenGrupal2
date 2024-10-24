using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    public static NodeManager Instance {get; private set;}

    [Header("Components")]
    [SerializeField] private List<Node> nodes;

    public List<Node> Nodes => nodes;

    private void Awake()
    {
        SetSingleton();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("There is more than one NodeManager Instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    public Node GetAliedBase() => nodes[0];
    public Node GetEnemyBase() => nodes[^1];

    public Node GetNextNodeAliedDirection(Node previousNode)
    {
        if (previousNode == GetEnemyBase()) return null;

        int previousNodeIndex = GetNodeIndex(previousNode);

        return nodes[previousNodeIndex + 1];
    }

    public Node GetNextNodeEnemyDirection(Node previousNode)
    {
        if (previousNode == GetAliedBase()) return null;

        int previousNodeIndex = GetNodeIndex(previousNode);

        return nodes[previousNodeIndex - 1];
    }

    public int GetNodeIndex(Node indexedNode)
    {
        int index = 0;

        foreach (Node node in nodes)
        {
            index++;
            if (node == indexedNode) return index;
        }

        return index;
    }
}
