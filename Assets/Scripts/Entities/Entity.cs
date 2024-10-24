using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private EntitySO entitySO;
    [SerializeField] private EntityHealth entityHealth;
    [SerializeField] private EntityPositioning entityPositioning;
    [Space]
    [SerializeField] protected bool isAlied;

    public EntitySO EntitySO => entitySO;
    public EntityHealth EntityHealth => entityHealth;
    public EntityPositioning EntityPositioning => entityPositioning;
    public bool IsAlied => isAlied;
}
