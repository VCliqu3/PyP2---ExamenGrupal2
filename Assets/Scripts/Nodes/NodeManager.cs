using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private List<Node> nodes;

    public List<Node> Nodes => nodes;
}
