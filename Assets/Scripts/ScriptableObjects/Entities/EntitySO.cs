using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntitySO : ScriptableObject
{
    [Header("Settings")]
    public EntityType entityType;
    public string entityName;
    [Range(10, 200)] public int health;
    [Range(10, 100)] public int price;
}
