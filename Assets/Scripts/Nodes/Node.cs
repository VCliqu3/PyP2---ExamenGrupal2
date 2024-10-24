using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int nodeNumber;

    public int NodeNumber => nodeNumber;
}
