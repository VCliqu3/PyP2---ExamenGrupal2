using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasHealth
{
    public int GetHealth();
    public int GetMaxHealth();
    public void TakeDamage(int quantity);
    public void IncreaseHealth(int quantity);
    public bool IsAlive();
    public Transform GetTransform();
}
