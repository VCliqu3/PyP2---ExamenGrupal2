using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePosition : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform entityTransform;

    public Transform EntityTransform => entityTransform;

    public bool IsEmpty() => entityTransform == null;
}
